using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using PresMed.Models.ViewModels;
using PresMed.Filters;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
    public class DoctorController : Controller {

        private readonly IDoctorServices _doctorService;
        private readonly ITimeServices _timeService;
        private readonly ISchedulingServices _schedulingServices;
        private readonly IClinicOpeningServices _clinicOpeningServices;

        public DoctorController(IDoctorServices doctorService, ITimeServices timeServices, ISchedulingServices schedulingServices, IClinicOpeningServices clinicOpeningServices) {
            _doctorService = doctorService;
            _timeService = timeServices;
            _schedulingServices = schedulingServices;
            _clinicOpeningServices = clinicOpeningServices;
        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de medicos ativos";
                var list = await _doctorService.FindAllActiveAsync();
                return View(list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Inactive() {
            try {
                ViewData["Title"] = "Listagem de medicos desativados";
                var list = await _doctorService.FindAllDisableAsync();
                return View("Index", list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Person doctor = await _doctorService.FindByIdAsync(id.Value);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(doctor);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Disable(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Person doctor = await _doctorService.FindByIdAsync(id.Value);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(doctor);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }


        public async Task<IActionResult> Enabled(int? id) {
            try {

                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Person doctor = await _doctorService.FindByIdAsync(id.Value);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", doctor);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Details(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Person doctor = await _doctorService.FindByIdAsync(id.Value);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(doctor);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        public async Task<IActionResult> Password(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Person doctor = await _doctorService.FindByIdAsync(id.Value);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (doctor.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Assistente desativado";
                    return RedirectToAction("Index");
                }
                return View(doctor);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Person doctor) {
            try {
                if (!ModelState.IsValid) {
                    return View(doctor);
                }
                doctor.PersonType = PersonType.Doctor;
                doctor.Status = Status.Ativo;
                string str = doctor.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                doctor.Cpf = str;
                doctor = _doctorService.TransformUpperCase(doctor);
                doctor.Password = Person.PasswordGenerate();
                string title = "Senha de acesso so sitema PresMed";
                string body = $"Olá, sua senha de acesso ao sistema presmed é: {doctor.Password}";
                await _doctorService.InsertAsync(doctor);
                Person.SendMail(doctor.Email, body, title);
                ClinicOpening clinicOpening = await _clinicOpeningServices.ListAsync();
                await _timeService.InsertAsync(new Time(clinicOpening.InitialHour, clinicOpening.EndHour, doctor, new DateTime(2022, 01, 01, 00, 30, 00), (clinicOpening.EndHour.Hour - clinicOpening.InitialHour.Hour) * 2));
                TempData["SuccessMessage"] = "Usuario cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disable(int id) {

            try {
                Person doctor = await _doctorService.FindByIdAsync(id);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                var list = await _schedulingServices.FindBylargerDate(id, DateTime.Now);
                if (list.Count > 0) {
                    TempData["ErrorMessage"] = "Existem agendas marcadas para o futuro";
                    return RedirectToAction("Index");
                }

                doctor.Status = Status.Desativado;
                await _doctorService.UpdateAsync(doctor);
                TempData["SuccessMessage"] = "Usuário desativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enable(int id) {

            try {
                Person doctor = await _doctorService.FindByIdAsync(id);
                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                ClinicOpening clinicOpening = await _clinicOpeningServices.ListAsync();
                Time dbTime = await _timeService.FindByIdAsync(id);
                double minutes = clinicOpening.EndHour.Subtract(clinicOpening.InitialHour).TotalMinutes;
                int min = dbTime.ServiceTime.Minute;

                switch (min) {
                    case 00:
                        dbTime.HourPerDay = (int)minutes / 60;
                        break;
                    case 15:
                        dbTime.HourPerDay = (int)minutes / 15;
                        break;
                    case 30:
                        dbTime.HourPerDay = (int)minutes / 30;
                        break;
                    case 45:
                        dbTime.HourPerDay = (int)minutes / 45;
                        break;
                }

                dbTime.FinalHour = clinicOpening.EndHour;
                dbTime.InitialHour = clinicOpening.InitialHour;
                await _timeService.UpdateAsync(dbTime);

                doctor.Status = Status.Ativo;
                await _doctorService.UpdateAsync(doctor);
                TempData["SuccessMessage"] = "Usuario ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Person doctor) {
            try {
                if (!ModelState.IsValid) {
                    return View(doctor);
                }
                int id = (int)doctor.Id;
                Person dbPerson = await _doctorService.FindByIdAsync(id);
                if (dbPerson == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (dbPerson.Email != doctor.Email) {
                    string title = "Alteração de e-mail sistema PresMed";
                    string body = $"Olá, ALERTA SISTEMA PREMED: O e-mail {dbPerson.Email} foi alterado para {doctor.Email} caso não tenha sido você favor procurar sua clinica.";
                    Person.SendMail(dbPerson.Email, body, title);
                }

                dbPerson.Phone = doctor.Phone;
                dbPerson.Email = doctor.Email;
                dbPerson.Street = doctor.Street;
                dbPerson.District = doctor.District;
                dbPerson.State = doctor.State;
                dbPerson.City = doctor.City;
                dbPerson.Complement = doctor.Complement;
                dbPerson.Number = doctor.Number;
                dbPerson.Speciality = doctor.Speciality;
                dbPerson.Name = doctor.Name;
                dbPerson.BirthDate = doctor.BirthDate;
                dbPerson = _doctorService.TransformUpperCase(dbPerson);
                await _doctorService.UpdateAsync(dbPerson);
                TempData["SuccessMessage"] = "Usuario alterado com sucesso";

                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(Person person) {
            try {

                Person doctor = await _doctorService.FindByIdAsync(person.Id);

                if (doctor == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (doctor.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Assistente desativado";
                    return RedirectToAction("Index");
                }

                doctor.Password = Person.PasswordGenerate();
                string title = "Nova senha de acesso so sitema PresMed";
                string body = $"Olá, sua nova senha de acesso ao sistema presmed é: {doctor.Password}";
                Person.SendMail(doctor.Email, body, title);
                await _doctorService.UpdateAsync(doctor);
                TempData["SuccessMessage"] = "Senha enviada com sucesso";
                return RedirectToAction("Index");


            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return RedirectToAction("Index");
            }


        }

    }
}
