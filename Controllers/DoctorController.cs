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

namespace PresMed.Controllers {
    public class DoctorController : Controller {

        private readonly IDoctorServices _doctorService;
        private readonly ITimeServices _timeService;
        private readonly ISchedulingServices _schedulingServices;

        public DoctorController(IDoctorServices doctorService, ITimeServices timeServices, ISchedulingServices schedulingServices) {
            _doctorService = doctorService;
            _timeService = timeServices;
            _schedulingServices = schedulingServices;
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
                await _doctorService.InsertAsync(doctor);
                await _timeService.InsertAsync(new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), doctor, new DateTime(2022, 01, 01, 00, 30, 00), 20));
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

    }
}
