using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.Threading.Tasks;
using PresMed.Models.ViewModels;
using PresMed.Filters;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
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

        public async Task<IActionResult> Password(int? id) {
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
                if (patient.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
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
                person.Password = Person.PasswordGenerate();
                string title = "Senha de acesso so sitema PresMed";
                string body = $"Olá, sua senha de acesso ao sistema presmed é: {person.Password}";
                await _patientService.InsertAsync(person);
                Person.SendMail(person.Email, body, title);

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

                if (dbPerson.Email != personPatient.Email) {
                    string title = "Alteração de e-mail sistema PresMed";
                    string body = $"Olá, ALERTA SISTEMA PREMED: O e-mail {dbPerson.Email} foi alterado para {personPatient.Email} caso não tenha sido você favor procurar sua clinica.";
                    Person.SendMail(dbPerson.Email, body, title);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Password(Person person) {
            try {

                Person patient = await _patientService.FindByIdAsync(person.Id);

                if (patient == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                if (patient.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Usuário desativado";
                    return RedirectToAction("Index");
                }

                patient.Password = Person.PasswordGenerate();
                string title = "Nova senha de acesso so sitema PresMed";
                string body = $"Olá, sua nova senha de acesso ao sistema presmed é: {patient.Password}";
                Person.SendMail(patient.Email, body, title);
                await _patientService.UpdateAsync(patient);
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
