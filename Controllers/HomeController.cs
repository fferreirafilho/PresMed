using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    public class HomeController : Controller {

        private readonly ILoginService _loginService;
        private readonly ITimeServices _timeServices;
        private readonly IDoctorServices _doctorServices;
        private readonly IClinicOpeningServices _clinicOpeningServices;

        public HomeController(ILoginService loginService, ITimeServices timeServices, IDoctorServices doctorServices, IClinicOpeningServices clinicOpeningServices) {
            _loginService = loginService;
            _timeServices = timeServices;
            _doctorServices = doctorServices;
            _clinicOpeningServices = clinicOpeningServices;
        }

        public IActionResult Index() {

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            if (person.PersonType == PersonType.Doctor) {
                return RedirectToAction("Index", "Attendance");

            }

            return View(person);


        }


        public IActionResult ChangePassword() {

            return View();
        }

        public IActionResult Report() {

            return View();
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

                ClinicOpening clinicOpening = await _clinicOpeningServices.ListAsync();

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

                    PdfPCell H1 = new PdfPCell(new Paragraph("Dia Inicial"));
                    PdfPCell H2 = new PdfPCell(new Paragraph("Dia Final"));
                    PdfPCell H3 = new PdfPCell(new Paragraph("Hora Inicial"));
                    PdfPCell H4 = new PdfPCell(new Paragraph("Hora Final"));
                    PdfPCell H5 = new PdfPCell(new Paragraph("Tempo de atendimento"));

                    table.AddCell(H1);
                    table.AddCell(H2);
                    table.AddCell(H3);
                    table.AddCell(H4);
                    table.AddCell(H5);

                    foreach (var itens in item.ListTime) {
                        PdfPCell cell1 = new PdfPCell(new Paragraph($"{itens.InitialDay.ToShortDateString()}"));
                        PdfPCell cell2 = new PdfPCell(new Paragraph($"{itens.FinalDay?.ToShortDateString()}"));
                        PdfPCell cell3 = new PdfPCell(new Paragraph($"{itens.InitialHour.ToShortTimeString()}"));
                        PdfPCell cell4 = new PdfPCell(new Paragraph($"{itens.FinalHour.ToShortTimeString()}"));
                        PdfPCell cell5 = new PdfPCell(new Paragraph($"{itens.ServiceTime.ToShortTimeString()}"));

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
    }
}
