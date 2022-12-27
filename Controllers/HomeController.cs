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
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    public class HomeController : Controller {

        private readonly ILoginService _loginService;
        private readonly ITimeServices _timeServices;
        private readonly IDoctorServices _doctorServices;
        private readonly IClinicSetingsServices _clinicOpeningServices;
        private readonly IAttendanceServices _attendanceServices;

        public HomeController(ILoginService loginService, ITimeServices timeServices, IDoctorServices doctorServices, IClinicSetingsServices clinicOpeningServices, IAttendanceServices attendanceServices) {
            _loginService = loginService;
            _timeServices = timeServices;
            _doctorServices = doctorServices;
            _clinicOpeningServices = clinicOpeningServices;
            _attendanceServices = attendanceServices;
        }

        public IActionResult Index() {

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            if (person.PersonType == PersonType.Doctor) {
                return RedirectToAction("Dash", "Attendance");

            }

            return View(person);
        }


        public IActionResult ChangePassword() {

            return View();
        }

        public async Task<IActionResult> Report() {
            ReportViewModel report = new ReportViewModel { ListDoctors = await _doctorServices.FindAllActiveAsync(), Initial = DateTime.Now.AddDays(-1), Final = DateTime.Now };
            return View(report);
        }

        public async Task<IActionResult> PrintDoctorsTime() {
            List<Person> doctors = await _doctorServices.FindAllActiveAsync();
            List<TimeViewModel> listTime = new List<TimeViewModel>();

            foreach (var item in doctors) {
                var dbTime = await _timeServices.FindAllByPersonId(item.Id);
                TimeViewModel model = new TimeViewModel();
                model.ListTime = dbTime;
                model.Person = item;
                listTime.Add(model);
            }
            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {

                ClinicSetings clinicOpening = await _clinicOpeningServices.ListAsync();

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\PresMed\\wwwroot\\images\\logo_clinica.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic,
                    $"Rua {clinicOpening.Street} - Nº {clinicOpening.Number} - {clinicOpening.City} {clinicOpening.State}\n\n\n"
                };

                document.Add(paragraph1);

                foreach (var item in listTime) {
                    document.Add(new Paragraph($"Lista de horario de atendimento do médico: {item.Person.Name}\n\n"));
                    PdfPTable table = new PdfPTable(5);

                    Paragraph p1 = new Paragraph("Dia Inicial");
                    PdfPCell H1 = new PdfPCell(p1);
                    H1.BackgroundColor = new BaseColor(189, 189, 189);
                    H1.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p2 = new Paragraph("Dia Final");
                    PdfPCell H2 = new PdfPCell(new Paragraph(p2));
                    H2.BackgroundColor = new BaseColor(189, 189, 189);
                    H2.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p3 = new Paragraph("Hora Inicial");
                    PdfPCell H3 = new PdfPCell(p3);
                    H3.BackgroundColor = new BaseColor(189, 189, 189);
                    H3.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p4 = new Paragraph("Hora Final");
                    PdfPCell H4 = new PdfPCell(p4);
                    H4.BackgroundColor = new BaseColor(189, 189, 189);
                    H4.HorizontalAlignment = Element.ALIGN_CENTER;

                    Paragraph p5 = new Paragraph("Tempo de atendimento");
                    PdfPCell H5 = new PdfPCell(p5);
                    H5.BackgroundColor = new BaseColor(189, 189, 189);
                    H5.HorizontalAlignment = Element.ALIGN_CENTER;

                    table.AddCell(H1);
                    table.AddCell(H2);
                    table.AddCell(H3);
                    table.AddCell(H4);
                    table.AddCell(H5);

                    foreach (var itens in item.ListTime) {
                        PdfPCell cell1 = new PdfPCell(new Paragraph($"{itens.InitialDay.ToShortDateString()}"));
                        cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell2 = new PdfPCell(new Paragraph($"{itens.FinalDay?.ToShortDateString()}"));
                        cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell3 = new PdfPCell(new Paragraph($"{itens.InitialHour.ToShortTimeString()}"));
                        cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell4 = new PdfPCell(new Paragraph($"{itens.FinalHour.ToShortTimeString()}"));
                        cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                        PdfPCell cell5 = new PdfPCell(new Paragraph($"{itens.ServiceTime.ToShortTimeString()}"));
                        cell5.HorizontalAlignment = Element.ALIGN_CENTER;

                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        table.AddCell(cell3);
                        table.AddCell(cell4);
                        table.AddCell(cell5);

                    }
                    table.SpacingAfter = 10f;
                    document.Add(table);
                }

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

            return File(stream, "application/pdf", $"Receita Medica.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel pwd) {

            if (!ModelState.IsValid) {
                return View(pwd);
            }


            if (pwd.Password != pwd.ConfirmPassword) {
                TempData["ErrorMessage"] = $"As senhas não conferem";
                return View(pwd);
            }


            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            person.Password = pwd.Password;

            person.SetPasswordHash();

            await _loginService.ChangePasswordAsync(person);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrintAttendanceDoctors(ReportViewModel report) {

            report.ListDoctors = await _doctorServices.FindAllActiveAsync();

            if (!ModelState.IsValid) {
                return View(nameof(Report), report);
            }

            Person doctor = await _doctorServices.FindByIdAsync(report.DoctorId);

            if (doctor == null) {
                TempData["ErrorMessage"] = $"ID invalido";
                return RedirectToAction(nameof(Index));
            }

            if (doctor.Status != Status.Ativo) {
                TempData["ErrorMessage"] = $"ID invalido";
                return RedirectToAction(nameof(Index));
            }

            var attendance = await _attendanceServices.FindAttendanceByDoctorIdAndDate(report.DoctorId, report.Initial, report.Final);

            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {

                ClinicSetings clinicOpening = await _clinicOpeningServices.ListAsync();

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\PresMed\\wwwroot\\images\\logo_clinica.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic,
                    $"Rua {clinicOpening.Street} - Nº {clinicOpening.Number} - {clinicOpening.City} {clinicOpening.State}\n\n\n"
                };

                document.Add(paragraph1);

                document.Add(new Paragraph($"Atendimentos concluidos pelo medico: {doctor.Name}\n\n"));
                PdfPTable table = new PdfPTable(3);

                Paragraph p1 = new Paragraph("Paciente");
                PdfPCell H1 = new PdfPCell(p1);
                H1.BackgroundColor = new BaseColor(189, 189, 189);
                H1.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p2 = new Paragraph("Data");
                PdfPCell H2 = new PdfPCell(new Paragraph(p2));
                H2.BackgroundColor = new BaseColor(189, 189, 189);
                H2.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p3 = new Paragraph("Atendimento");
                PdfPCell H3 = new PdfPCell(p3);
                H3.BackgroundColor = new BaseColor(189, 189, 189);
                H3.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(H1);
                table.AddCell(H2);
                table.AddCell(H3);

                foreach (var item in attendance) {

                    PdfPCell cell1 = new PdfPCell(new Paragraph($"{item.Patient.Name}"));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell2 = new PdfPCell(new Paragraph($"{item.Scheduling.DayAttendence.ToShortDateString()}"));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell3 = new PdfPCell(new Paragraph($"{item.Scheduling.Procedures.Name}"));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;


                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                }

                table.SpacingAfter = 10f;
                document.Add(table);

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

            return File(stream, "application/pdf", $"Atendimentos {doctor.Name}.pdf");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PrintSchedulingByDoctor(ReportViewModel report) {

            report.ListDoctors = await _doctorServices.FindAllActiveAsync();

            if (!ModelState.IsValid) {
                return View(nameof(Report), report);
            }

            Person doctor = await _doctorServices.FindByIdAsync(report.DoctorId);

            if (doctor == null) {
                TempData["ErrorMessage"] = $"ID invalido";
                return RedirectToAction(nameof(Index));
            }

            if (doctor.Status != Status.Ativo) {
                TempData["ErrorMessage"] = $"ID invalido";
                return RedirectToAction(nameof(Index));
            }

            var scheduling = await _attendanceServices.FindSchedulingByDoctorIdAndDate(report.DoctorId, report.Initial, report.Final);

            Document document = new Document();

            MemoryStream stream = new MemoryStream();

            try {

                ClinicSetings clinicOpening = await _clinicOpeningServices.ListAsync();

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, stream);
                pdfWriter.CloseStream = false;
                var image = System.Drawing.Image.FromFile("D:\\TCC\\PresMed\\wwwroot\\images\\logo_clinica.png");
                document.Open();
                Image pic = Image.GetInstance(image, System.Drawing.Imaging.ImageFormat.Jpeg);
                pic.ScalePercent(15);
                Paragraph paragraph1 = new Paragraph {
                    pic,
                    $"Rua {clinicOpening.Street} - Nº {clinicOpening.Number} - {clinicOpening.City} {clinicOpening.State}\n\n\n"
                };

                document.Add(paragraph1);

                document.Add(new Paragraph($"Agendamentos do medico: {doctor.Name}\n\n"));
                PdfPTable table = new PdfPTable(5);

                Paragraph p1 = new Paragraph("Paciente");
                PdfPCell H1 = new PdfPCell(p1);
                H1.BackgroundColor = new BaseColor(189, 189, 189);
                H1.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p2 = new Paragraph("Data");
                PdfPCell H2 = new PdfPCell(new Paragraph(p2));
                H2.BackgroundColor = new BaseColor(189, 189, 189);
                H2.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p3 = new Paragraph("Atendimento");
                PdfPCell H3 = new PdfPCell(p3);
                H3.BackgroundColor = new BaseColor(189, 189, 189);
                H3.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p4 = new Paragraph("Hora inicial");
                PdfPCell H4 = new PdfPCell(p4);
                H4.BackgroundColor = new BaseColor(189, 189, 189);
                H4.HorizontalAlignment = Element.ALIGN_CENTER;

                Paragraph p5 = new Paragraph("Hora final");
                PdfPCell H5 = new PdfPCell(p5);
                H5.BackgroundColor = new BaseColor(189, 189, 189);
                H5.HorizontalAlignment = Element.ALIGN_CENTER;

                table.AddCell(H1);
                table.AddCell(H2);
                table.AddCell(H3);
                table.AddCell(H4);
                table.AddCell(H5);

                foreach (var item in scheduling) {
                    var time = await _timeServices.FindByDoctorDateIdAsync(report.DoctorId, item.DayAttendence.Date);

                    PdfPCell cell1 = new PdfPCell(new Paragraph($"{item.Patient.Name}"));
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell2 = new PdfPCell(new Paragraph($"{item.DayAttendence.ToShortDateString()}"));
                    cell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell3 = new PdfPCell(new Paragraph($"{item.Procedures.Name}"));
                    cell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell4 = new PdfPCell(new Paragraph($"{item.HourAttendence.ToShortTimeString()}"));
                    cell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell5 = new PdfPCell(new Paragraph($"{item.HourAttendence.AddMinutes(time.ServiceTime.Minute == 0 ? 60 : time.ServiceTime.Minute).ToShortTimeString()}"));
                    cell5.HorizontalAlignment = Element.ALIGN_CENTER;


                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    table.AddCell(cell5);
                }

                table.SpacingAfter = 10f;
                document.Add(table);

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

            return File(stream, "application/pdf", $"Agenda {doctor.Name}.pdf");
        }
    }
}
