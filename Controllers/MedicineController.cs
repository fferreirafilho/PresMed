using Microsoft.AspNetCore.Mvc;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
    public class MedicineController : Controller {

        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService) {
            _medicineService = medicineService;
        }

        public async Task<IActionResult> Index() {
            try {
                ViewData["Title"] = "Listagem de medicamentos ativos";
                var list = await _medicineService.FindAllAsync();
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
                Medicine procedure = await _medicineService.FindByIdAsync(id.Value);
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
                Medicine medicine = await _medicineService.FindByIdAsync(id.Value);
                if (medicine == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(medicine);
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
                Medicine medicine = await _medicineService.FindByIdAsync(id.Value);
                if (medicine == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View("Disable", medicine);

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
                Medicine medicine = await _medicineService.FindByIdAsync(id.Value);
                if (medicine == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                return View(medicine);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Medicine medicine) {
            try {
                if (!ModelState.IsValid) {
                    return View(medicine);
                }
                medicine = _medicineService.TransformUpperCase(medicine);
                await _medicineService.InsertAsync(medicine);

                TempData["SuccessMessage"] = "Medicamento cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Medicine medicine) {
            try {
                if (!ModelState.IsValid) {
                    return View(medicine);
                }
                Medicine db = await _medicineService.FindByIdAsync(medicine.Id);
                if (db == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                db.Name = medicine.Name;
                medicine = _medicineService.TransformUpperCase(db);
                await _medicineService.UpdateAsync(db);
                TempData["SuccessMessage"] = "Medicamento alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
