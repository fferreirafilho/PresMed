using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.Threading.Tasks;
using PresMed.Models.ViewModels;

namespace PresMed.Controllers {
    public class ProceduresController : Controller {

        private readonly IProceduresServices _proceduresServices;

        public ProceduresController(IProceduresServices proceduresServices) {
            _proceduresServices = proceduresServices;
        }
        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de procedimentos ativos";
                var list = await _proceduresServices.FindAllActiveAsync();
                return View(list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Inactive() {
            try {
                ViewData["Title"] = "Listagem de procedimentos desativados";
                var list = await _proceduresServices.FindAllDisableAsync();
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
                Procedures procedure = await _proceduresServices.FindByIdAsync(id.Value);
                if (procedure == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(procedure);
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
                Procedures procedures = await _proceduresServices.FindByIdAsync(id.Value);
                if (procedures == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(procedures);
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
                Procedures procedures = await _proceduresServices.FindByIdAsync(id.Value);
                if (procedures == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", procedures);

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
                Procedures procedures = await _proceduresServices.FindByIdAsync(id.Value);
                if (procedures == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(procedures);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Procedures procedures) {
            try {
                procedures.Status = Status.Ativo;

                if (!ModelState.IsValid) {
                    return View(procedures);
                }
                procedures = _proceduresServices.TransformUpperCase(procedures);
                await _proceduresServices.InsertAsync(procedures);

                TempData["SuccessMessage"] = "Procedimento cadastrado com sucesso";
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
                Procedures procedure = await _proceduresServices.FindByIdAsync(id);
                if (procedure == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                procedure.Status = Status.Desativado;
                await _proceduresServices.UpdateAsync(procedure);
                TempData["SuccessMessage"] = "Procedimento desativado com sucesso";
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
                Procedures procedure = await _proceduresServices.FindByIdAsync(id);
                if (procedure == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                procedure.Status = Status.Ativo;
                await _proceduresServices.UpdateAsync(procedure);
                TempData["SuccessMessage"] = "Procedimento ativado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Procedures procedures) {
            try {
                if (!ModelState.IsValid) {
                    return View(procedures);
                }
                Procedures dbPerson = await _proceduresServices.FindByIdAsync(procedures.Id);
                if (dbPerson == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbPerson.Name = procedures.Name;
                procedures = _proceduresServices.TransformUpperCase(dbPerson);
                await _proceduresServices.UpdateAsync(dbPerson);
                TempData["SuccessMessage"] = "Procedimento alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
