using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Services;
using System;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class ClinicOpeningController : Controller {

        private readonly IClinicOpeningServices _clinicalOpeningServices;

        public ClinicOpeningController(IClinicOpeningServices clinicalOpeningServices) {
            _clinicalOpeningServices = clinicalOpeningServices;
        }

        public async Task<IActionResult> Index() {
            try {
                ClinicOpening clinicOpening = await _clinicalOpeningServices.ListAsync();
                return View(clinicOpening);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Edit() {
            try {
                ClinicOpening clinicOpening = await _clinicalOpeningServices.ListAsync();
                return View(clinicOpening);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClinicOpening clinic) {
            try {

                if (!ModelState.IsValid) {
                    return View(clinic);
                }

                if (clinic.InitialHour > clinic.EndHour) {
                    TempData["ErrorMessage"] = "Horario invalido";
                    return View(clinic);
                }

                ClinicOpening clinicDb = await _clinicalOpeningServices.ListAsync();

                clinicDb.EndHour = clinic.EndHour;
                clinicDb.InitialHour = clinic.InitialHour;

                await _clinicalOpeningServices.UpdateAsync(clinicDb);
                TempData["SuccessMessage"] = "Horario Alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }
    }
}
