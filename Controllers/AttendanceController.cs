using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyDoctor]
    public class AttendanceController : Controller {

        private readonly ISchedulingServices _schedulingServices;
        private readonly ITimeServices _timeServices;
        private readonly IAttendanceServices _attendanceServices;
        private readonly IMedicineService _medicineService;
        private readonly IPatientServices _patientServices;
        private readonly IDoctorServices _doctorServices;
        private readonly ICidServices _cidServices;


        public AttendanceController(ISchedulingServices schedulingServices, ITimeServices timeServices, IAttendanceServices attendanceServices, IMedicineService medicineSerioce, IPatientServices patientServices, IDoctorServices doctorServices, ICidServices cidServices) {
            _schedulingServices = schedulingServices;
            _timeServices = timeServices;
            _attendanceServices = attendanceServices;
            _medicineService = medicineSerioce;
            _patientServices = patientServices;
            _doctorServices = doctorServices;
            _cidServices = cidServices;
        }

        public async Task<IActionResult> Index() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                IEnumerable<Time> listTime = await _timeServices.FindScheduleByIdAsync(person.Id, DateTime.Now);

                var timeJob = listTime.Where(x => x.InitialDay <= DateTime.Now && x.FinalDay >= DateTime.Now).ToList();

                attendance.Hour = timeJob.Count() == 0 ? await _timeServices.FindScheduleByIdAndFinalDateNullAsync(person.Id) : timeJob[0];

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);


                var newList = list.Where(x => x.StatusAttendence == StatusAttendence.Aguardando_atendimento || x.StatusAttendence == StatusAttendence.Em_atendimento);

                attendance.Schedulings = newList;

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Attended() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                IEnumerable<Time> listTime = await _timeServices.FindScheduleByIdAsync(person.Id, DateTime.Now);

                var timeJob = listTime.Where(x => x.InitialDay <= DateTime.Now && x.FinalDay >= DateTime.Now).ToList();

                attendance.Hour = timeJob.Count() == 0 ? await _timeServices.FindScheduleByIdAndFinalDateNullAsync(person.Id) : timeJob[0];

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);


                var newList = list.Where(x => x.StatusAttendence == StatusAttendence.Finalizado);

                attendance.Schedulings = newList;

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Scheduling() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                IEnumerable<Time> listTime = await _timeServices.FindScheduleByIdAsync(person.Id, DateTime.Now);

                var timeJob = listTime.Where(x => x.InitialDay <= DateTime.Now && x.FinalDay >= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)).ToList();

                attendance.Hour = timeJob.Count() == 0 ? await _timeServices.FindScheduleByIdAndFinalDateNullAsync(person.Id) : timeJob[0];

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);
                Scheduling[] array = new Scheduling[attendance.Hour.HourPerDay];
                Scheduling[] listArray = list.ToArray();

                for (int i = 0; i < attendance.Hour.HourPerDay; i++) {
                    Scheduling sc = new Scheduling();
                    sc.HourAttendence = attendance.Hour.InitialHour.AddMinutes(minutes * i);
                    sc.StatusAttendence = StatusAttendence.Livre;
                    array[i] = sc;
                }

                for (int i = 0; i < attendance.Hour.HourPerDay; i++) {
                    for (int j = 0; j < listArray.Length; j++) {
                        if (array[i].HourAttendence.ToShortTimeString() == listArray[j].HourAttendence.ToShortTimeString()) {
                            array[i] = listArray[j];
                        }
                    }
                }


                attendance.Schedulings = array.ToList();

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> Attend(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            Scheduling scheduling = await _schedulingServices.FindByIdAsync(id.Value);
            if (scheduling == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            if (person.Id != scheduling.Doctor.Id) {
                TempData["ErrorMessage"] = $"Atendimento invalido";
                return RedirectToAction("Index");
            }

            //if (DateTime.Now.Month > scheduling.DayAttendence.Month) {
            //    TempData["ErrorMessage"] = $"Data de atendimento invalido";
            //    return RedirectToAction(nameof(Index));
            //}

            //if (DateTime.Now.Day > scheduling.DayAttendence.Day) {
            //    TempData["ErrorMessage"] = $"Data de atendimento invalido";
            //    return RedirectToAction(nameof(Index));
            //}

            //if (DateTime.Now.Day == scheduling.DayAttendence.Day) {
            //    if (DateTime.Now.Hour > scheduling.HourAttendence.Hour) {
            //        TempData["ErrorMessage"] = $"Horario de atendimento invalido";
            //        return RedirectToAction(nameof(Index));
            //    }
            //    if (DateTime.Now.Hour < scheduling.HourAttendence.Hour) {
            //        TempData["ErrorMessage"] = $"Horario de atendimento futuro";
            //        return RedirectToAction(nameof(Index));
            //    }
            //}


            if (scheduling.StatusAttendence != StatusAttendence.Aguardando_atendimento && scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            Attendance attendance1 = await _attendanceServices.FindBySchedulingId(scheduling.Id);
            Attendance attendance = new Attendance { Doctor = scheduling.Doctor, Patient = scheduling.Patient, Scheduling = scheduling };
            if (attendance1 != null) {
                attendance.Report = attendance1.Report;
            }

            return View(attendance);
        }

        public async Task<IActionResult> Finishing(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Scheduling scheduling = await _schedulingServices.FindByIdAsync(id.Value);
            if (scheduling == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            if (scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Attendance attendance = await _attendanceServices.FindBySchedulingId(id.Value);
            List<Prescription> prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(attendance.Id);
            PrescriptionViewModel prescription = new PrescriptionViewModel { Attendance = attendance, Prescriptions = prescriptions, MedicalCertificate = await _attendanceServices.FindMedicalCertificateByAttendanceId(attendance.Id) };
            return View(prescription);
        }

        public async Task<IActionResult> Prescription(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Scheduling scheduling = await _schedulingServices.FindByIdAsync(id.Value);
            if (scheduling == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }
            if (scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }

            Attendance attendance = await _attendanceServices.FindBySchedulingId(scheduling.Id);
            List<Medicine> medicines = await _medicineService.FindAllAsync();
            Person patient = await _patientServices.FindByIdAsync(attendance.Patient.Id);
            PrescriptionViewModel prescription = new PrescriptionViewModel { AttendanceId = attendance.Id, Medicines = medicines, Patient = patient, Prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(attendance.Id) };
            return View(prescription);
        }


        public async Task<IActionResult> Remove(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            Prescription prescription = await _attendanceServices.FindPrescriptionById(id.Value);

            if (prescription == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            if (prescription.Attendance.Scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            await _attendanceServices.DeletePrescriptionAsync(id.Value);

            PrescriptionViewModel prescriptionViewModel = new PrescriptionViewModel { AttendanceId = prescription.Attendance.Id, Medicines = await _medicineService.FindAllAsync(), Patient = await _patientServices.FindByIdAsync(prescription.Attendance.Patient.Id), Prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(prescription.Attendance.Id) };

            return View("Prescription", prescriptionViewModel);
        }

        public async Task<IActionResult> Close(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Scheduling scheduling = await _schedulingServices.FindByIdAsync(id.Value);
            if (scheduling == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            if (scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }

            scheduling.StatusAttendence = StatusAttendence.Finalizado;
            await _schedulingServices.UpdateAsync(scheduling);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MedicalCertificate(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Attendance attendance = await _attendanceServices.FindBySchedulingId(id.Value);

            if (attendance == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            MedicalCertificate medical = await _attendanceServices.FindMedicalCertificateByAttendanceId(attendance.Id);
            MedicalCertificateViewModel certificate = new MedicalCertificateViewModel { Attendance = attendance, ListCid = await _cidServices.FindAllAsync() };
            if (medical != null) {
                certificate.Days = medical.Days;
                certificate.Cid = medical.Cid.Id;
            }
            return View(certificate);
        }

        public async Task<IActionResult> PrintMedicalCertificate(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Attendance attendance = await _attendanceServices.FindAttendanceByIdAsync(id.Value);

            if (attendance == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            if (attendance.Scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            MedicalCertificate certificate = await _attendanceServices.FindMedicalCertificateByAttendanceId(id.Value);
            Document document = new Document();

            MemoryStream stream = new MemoryStream();


            try {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\PresMed\\wwwroot\\images\\logo_clinica.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic,
                    $"Rua das Ambrosias - Nº 486 - Goianésia GO\n\n\n"
                };

                document.Add(paragraph1);
                Paragraph paragraph2 = new Paragraph($"Atestado Medico de {attendance.Patient.Name}\n\n");
                paragraph2.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph2);

                document.Add(new Paragraph($"Atesto para os devidos fins, a pedido, que o(a) Sr(a). {attendance.Patient.Name}, inscrito(a) no CPF sob o nº {attendance.Patient.Cpf}, paciente sob meus cuidados, foi atendido(a) no dia {DateTime.Now.ToShortDateString()} às {DateTime.Now.ToShortTimeString()}, cid {certificate.Cid.Cod} necessitando de {certificate.Days} dias de repouso.\r\n\r\n Goianésia, {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}.\r\n\r\n \r\n\r\n{attendance.Doctor.Name}\r\n\r\nCRM {attendance.Doctor.Crm}"));

            }
            catch (DocumentException de) {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe) {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required

            return File(stream, "application/pdf", $"Atestado - {attendance.Patient.Name}.pdf");
        }


        public async Task<IActionResult> PrintPrescription(int? id) {
            if (id == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            Attendance attendance = await _attendanceServices.FindAttendanceByIdAsync(id.Value);

            if (attendance == null) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            if (attendance.Scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }
            List<Prescription> prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(id.Value);

            if (prescriptions.Count() == 0) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                RedirectToAction("Index");
            }

            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {
                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\PresMed\\wwwroot\\images\\logo_clinica.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic,
                    $"Rua das Ambrosias - Nº 486 - Goianésia GO\n\n\n"
                };

                document.Add(paragraph1);
                Paragraph paragraph2 = new Paragraph($"Receita Medica de {attendance.Patient.Name}\n\n");
                paragraph2.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph2);

                foreach (var item in prescriptions) {
                    document.Add(new Paragraph($"Tomar {item.Dosage} de {item.Medicine.Name} a cada {item.Time.ToShortTimeString()}h, observação: {item.Observation}\n----------------------------------------------------------------------------------------------------------------------------------"));
                }
                document.Add(new Paragraph($"\r\n\r\n Goianésia, {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}.\r\n\r\n \r\n\r\n{attendance.Doctor.Name}\r\n\r\nCRM {attendance.Doctor.Crm}"));
            }
            catch (DocumentException de) {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe) {
                Console.Error.WriteLine(ioe.Message);
            }

            document.Close();

            stream.Flush(); //Always catches me out
            stream.Position = 0; //Not sure if this is required

            return File(stream, "application/pdf", $"Receita Medica - {attendance.Patient.Name}.pdf");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attend(Attendance attendance) {
            attendance.Patient = await _patientServices.FindByIdAsync(attendance.Patient.Id);
            attendance.Doctor = await _patientServices.FindByIdAsync(attendance.Doctor.Id);
            attendance.Scheduling = await _schedulingServices.FindByIdAsync(attendance.Scheduling.Id);
            if (string.IsNullOrEmpty(attendance.Report)) {
                if (!ModelState.IsValid) {
                    return View("Attend", attendance);
                }
            }
            if (attendance.Scheduling.StatusAttendence != StatusAttendence.Aguardando_atendimento && attendance.Scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }

            Attendance attendanceDB = await _attendanceServices.FindBySchedulingId(attendance.Scheduling.Id);

            if (attendanceDB == null) {
                attendance.Scheduling.StatusAttendence = StatusAttendence.Em_atendimento;
                await _attendanceServices.InsertAttendanceAsync(attendance);
                return RedirectToAction("Index");
            }
            attendanceDB.Report = attendance.Report;
            await _attendanceServices.UpdateAsync(attendanceDB);
            return RedirectToAction("Index");


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(PrescriptionViewModel list) {

            list.Patient = await _patientServices.FindByIdAsync(list.Patient.Id);
            list.Medicines = await _medicineService.FindAllAsync();
            list.Prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(list.AttendanceId);

            if (list.Prescription.Days == 0 || list.Prescription.Time == null || list.Prescription.Dosage == null || list.Prescription.Observation == null) {
                if (!ModelState.IsValid) {
                    return View("Prescription", list);
                }
            }
            Attendance attendance = await _attendanceServices.FindAttendanceByIdAsync(list.AttendanceId);

            if (attendance.Scheduling.StatusAttendence != StatusAttendence.Em_atendimento) {
                TempData["ErrorMessage"] = $"ID não encontrado";
                return RedirectToAction("Index");
            }


            Medicine medicine = await _medicineService.FindByIdAsync(list.Prescription.Medicine.Id);
            Prescription prescription = new Prescription { Attendance = attendance, Days = list.Prescription.Days, Dosage = list.Prescription.Dosage, Medicine = medicine, Time = list.Prescription.Time, Observation = list.Prescription.Observation };

            await _attendanceServices.InsertPrescriptionAsync(prescription);
            list.Prescriptions = await _attendanceServices.FindPrescriptionByAttendanceId(list.AttendanceId);
            return View("Prescription", list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MedicalCertificate(MedicalCertificateViewModel medicalCertificateViewModel) {

            Attendance attendance = await _attendanceServices.FindAttendanceByIdAsync(medicalCertificateViewModel.Id);
            IEnumerable<Cid> listCid = await _cidServices.FindAllAsync();
            medicalCertificateViewModel.Attendance = attendance;
            medicalCertificateViewModel.ListCid = listCid;
            if (!ModelState.IsValid) {
                TempData["ErrorMessage"] = "Test";
                return View(medicalCertificateViewModel);
            }

            MedicalCertificate medical = await _attendanceServices.FindMedicalCertificateByAttendanceId(attendance.Id);
            Cid cid = await _cidServices.FindByIdAsync(medicalCertificateViewModel.Cid);
            if (medical != null) {
                medical.Attendance = attendance;
                medical.Cid = cid;
                medical.Days = medicalCertificateViewModel.Days;
                await _attendanceServices.UpdateMedicalCertificateAsync(medical);
            }
            else {
                await _attendanceServices.InsertMedicalCertificateAsync(new MedicalCertificate { Attendance = attendance, Cid = cid, Days = medicalCertificateViewModel.Days });
            }
            return RedirectToAction("Index");
        }
    }
}
