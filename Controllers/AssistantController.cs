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
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PresMed.Controllers {
    public class AssistantController : Controller {
        private readonly IAssistantServices _assistantService;

        public AssistantController(IAssistantServices assistantService) {
            _assistantService = assistantService;
        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de assistentes ativos";
                var list = await _assistantService.FindAllActiveAsync();
                return View(list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> Inactive() {
            try {
                ViewData["Title"] = "Listagem de assistentes desativados";
                var list = await _assistantService.FindAllDisableAsync();
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
                Person assistant = await _assistantService.FindByIdAsync(id.Value);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }


                return View(PersonAssistantViewModel.Parse(assistant));
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
                Person assistant = await _assistantService.FindByIdAsync(id.Value);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(assistant);

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
                Person assistant = await _assistantService.FindByIdAsync(id.Value);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", assistant);

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
                Person assistant = await _assistantService.FindByIdAsync(id.Value);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(assistant);

            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PersonAssistantViewModel assistant) {
            try {
                assistant.PersonType = PersonType.Assistant;
                assistant.Status = Status.Ativo;

                if (!ModelState.IsValid) {
                    return View(assistant);
                }

                string str = assistant.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                assistant.Cpf = str;

                Person person = Person.Parse(assistant, null);
                person = _assistantService.TransformUpperCase(person);
                await _assistantService.InsertAsync(person);

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
                Person assistant = await _assistantService.FindByIdAsync(id);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                assistant.Status = Status.Desativado;
                await _assistantService.UpdateAsync(assistant);
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
                Person assistant = await _assistantService.FindByIdAsync(id);
                if (assistant == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                assistant.Status = Status.Ativo;
                await _assistantService.UpdateAsync(assistant);
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
        public async Task<IActionResult> Edit(PersonAssistantViewModel personAssistant) {

            try {

                if (!ModelState.IsValid) {
                    return View(personAssistant);
                }

                Person assistant = Person.Parse(personAssistant, null);
                int id = (int)assistant.Id;
                Person dbPerson = await _assistantService.FindByIdAsync(id);

                if (dbPerson == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPerson.Phone = assistant.Phone;
                dbPerson.Email = assistant.Email;
                dbPerson.Street = assistant.Street;
                dbPerson.District = assistant.District;
                dbPerson.State = assistant.State;
                dbPerson.City = assistant.City;
                dbPerson.Complement = assistant.Complement;
                dbPerson.Number = assistant.Number;
                dbPerson.Name = assistant.Name;
                dbPerson.BirthDate = assistant.BirthDate;
                dbPerson = _assistantService.TransformUpperCase(dbPerson);
                await _assistantService.UpdateAsync(dbPerson);
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
