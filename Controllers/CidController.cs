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
    public class CidController : Controller {

        private readonly ICidServices _cidServices;

        public CidController(ICidServices cidServices) {
            _cidServices = cidServices;
        }
        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de CIDS";
                var list = await _cidServices.FindAllAsync();
                return View(list);
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
                Cid cid = await _cidServices.FindByIdAsync(id.Value);
                if (cid == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(cid);
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
                Cid cid = await _cidServices.FindByIdAsync(id.Value);
                if (cid == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(cid);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Cid cid) {
            try {

                if (!ModelState.IsValid) {
                    return View(cid);
                }
                cid = _cidServices.TransformUpperCase(cid);
                await _cidServices.InsertAsync(cid);

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
        public async Task<IActionResult> Edit(Cid cid) {
            try {
                if (!ModelState.IsValid) {
                    return View(cid);
                }
                Cid dbCid = await _cidServices.FindByIdAsync(cid.Id);
                if (dbCid == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                dbCid.Description = cid.Description;
                cid = _cidServices.TransformUpperCase(dbCid);
                await _cidServices.UpdateAsync(dbCid);
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
