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
            try {
                ViewData["Title"] = "Listagem de pacientes ativos";
                var list = await _patientService.FindAllActiveAsync();
                return View(list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Inactive() {
            try {
                ViewData["Title"] = "Listagem de pacientes desativados";
                var list = await _patientService.FindAllDisableAsync();
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
                Person patient = await _patientService.FindByIdAsync(id.Value);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                return View("Edit", PersonPatientViewModel.Parse(patient));
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
                Person patient = await _patientService.FindByIdAsync(id.Value);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(patient);
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
                Person patient = await _patientService.FindByIdAsync(id.Value);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", patient);
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
                Person patient = await _patientService.FindByIdAsync(id.Value);
                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(patient);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PersonPatientViewModel patient) {
            try {
                patient.PersonType = PersonType.Patient;
                patient.Status = Status.Desativado;

                if (!ModelState.IsValid) {
                    return View(patient);
                }

                string str = patient.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                patient.Cpf = str;

                Person person = Person.Parse(null, patient);
                person = _patientService.TransformUpperCase(person);
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
                patient.Status = Status.Desativado;
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
                patient.Status = Status.Ativo;
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
        public async Task<IActionResult> Edit(PersonPatientViewModel personPatient) {

            try {

                if (!ModelState.IsValid) {
                    return View(personPatient);
                }

                Person patient = Person.Parse(null, personPatient);
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
                dbPerson.Name = patient.Name;
                dbPerson.BirthDate = patient.BirthDate;
                dbPerson = _patientService.TransformUpperCase(dbPerson);
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
