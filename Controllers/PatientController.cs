using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.Threading.Tasks;
using PresMed.Models.ViewModels;

namespace PresMed.Controllers {
    public class PatientController : Controller {
        private readonly IPatientServices _patientService;

        public PatientController(IPatientServices patientServices) {
            _patientService = patientServices;
        }
        public async Task<IActionResult> Index() {
            ViewData["Title"] = "Listagem de pacientes ativos";
            var list = await _patientService.FindAllActiveAsync();
            return View(list);
        }

        public async Task<IActionResult> Inactive() {
            ViewData["Title"] = "Listagem de pacientes desativados";
            var list = await _patientService.FindAllDisableAsync();
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
            Person patient = await _patientService.FindByIdAsync(id.Value);
            if (patient == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }


            return View(PersonPatient.Parse(patient, null));
        }

        public async Task<IActionResult> Disable(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person patient = await _patientService.FindByIdAsync(id.Value);
            if (patient == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(patient);
        }


        public async Task<IActionResult> Enabled(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person patient = await _patientService.FindByIdAsync(id.Value);
            if (patient == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View("Disable", patient);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Person patient = await _patientService.FindByIdAsync(id.Value);
            if (patient == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PersonPatient patient) {
            try {
                patient.PersonType = PersonType.Patient;
                patient.Status = UserStatus.Inativado;

                if (!ModelState.IsValid) {
                    return View(patient);
                }

                string str = patient.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                patient.Cpf = str;

                Person person = Person.Parse(PersonAssistant.Parse(null, patient));

                await _patientService.InsertAsync(person);

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
                Person patient = await _patientService.FindByIdAsync(id);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                patient.Status = UserStatus.Inativado;
                await _patientService.UpdateAsync(patient);
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
                Person patient = await _patientService.FindByIdAsync(id);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                patient.Status = UserStatus.Ativo;
                await _patientService.UpdateAsync(patient);
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
        public async Task<IActionResult> Edit(PersonPatient personPatient) {

            try {

                if (!ModelState.IsValid) {
                    return View(personPatient);
                }

                Person patient = Person.Parse(PersonAssistant.Parse(null, personPatient));
                int id = (int)patient.Id;
                Person dbPerson = await _patientService.FindByIdAsync(id);

                if (dbPerson == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPerson.Phone = patient.Phone;
                dbPerson.Email = patient.Email;
                dbPerson.Street = patient.Street;
                dbPerson.District = patient.District;
                dbPerson.State = patient.State;
                dbPerson.City = patient.City;
                dbPerson.Complement = patient.Complement;
                dbPerson.Number = patient.Number;
                dbPerson.Speciality = patient.Speciality;
                dbPerson.Name = patient.Name;

                await _patientService.UpdateAsync(dbPerson);
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
