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

        public DoctorController(IDoctorServices doctorService) {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de medicos ativos";
            var list = await _doctorService.FindAllActiveAsync();
            return View(list);
        }

        public async Task<IActionResult> Inactive() {
            ViewData["Title"] = "Listagem de medicos desativados";
            var list = await _doctorService.FindAllDisableAsync();
            return View("Index", list);
        }

        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {

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

        public async Task<IActionResult> Disable(int? id) {

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


        public async Task<IActionResult> Enabled(int? id) {

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

        public async Task<IActionResult> Details(int? id) {

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Person doctor) {
            try {
                if (!ModelState.IsValid) {
                    return View(doctor);
                }
                doctor.PersonType = PersonType.Doctor;
                doctor.Status = UserStatus.Ativo;
                string str = doctor.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                doctor.Cpf = str;
                await _doctorService.InsertAsync(doctor);
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
                doctor.Status = UserStatus.Inativado;
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
                doctor.Status = UserStatus.Ativo;
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
