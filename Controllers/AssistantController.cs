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
            ViewData["Title"] = "Listagem de assistentes ativos";
            var list = await _assistantService.FindAllActiveAsync();
            return View(list);
        }

        public async Task<IActionResult> Inactive() {
            ViewData["Title"] = "Listagem de assistentes desativados";
            var list = await _assistantService.FindAllDisableAsync();
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
            Person assistant = await _assistantService.FindByIdAsync(id.Value);
            if (assistant == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }


            return View(PersonAssistant.Parse(assistant));
        }

        public async Task<IActionResult> Disable(int? id) {

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


        public async Task<IActionResult> Enabled(int? id) {

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

        public async Task<IActionResult> Details(int? id) {

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(PersonAssistant assistant) {
            try {
                assistant.PersonType = PersonType.Assistant;
                assistant.Status = UserStatus.Ativo;

                if (!ModelState.IsValid) {
                    return View(assistant);
                }

                string str = assistant.Cpf;
                str = str.Trim();
                str = str.Replace(".", "").Replace("-", "");
                assistant.Cpf = str;

                Person person = Person.Parse(assistant);

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
                assistant.Status = UserStatus.Inativado;
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
                assistant.Status = UserStatus.Ativo;
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
        public async Task<IActionResult> Edit(PersonAssistant personAssistant) {

            try {

                if (!ModelState.IsValid) {
                    return View(personAssistant);
                }

                Person assistant = Person.Parse(personAssistant);
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
                dbPerson.Speciality = assistant.Speciality;
                dbPerson.Name = assistant.Name;

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
