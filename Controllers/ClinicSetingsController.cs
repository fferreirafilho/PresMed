using Microsoft.AspNetCore.Mvc;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
    public class ClinicSetingsController : Controller {

        private readonly IClinicSetingsServices _clinicalOpeningServices;
        private readonly ITimeServices _timeServices;

        public ClinicSetingsController(IClinicSetingsServices clinicalOpeningServices, ITimeServices timeServices) {
            _clinicalOpeningServices = clinicalOpeningServices;
            _timeServices = timeServices;
        }

        public async Task<IActionResult> Index() {
            try {
                ClinicSetings clinicOpening = await _clinicalOpeningServices.ListAsync();
                return View(clinicOpening);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Edit() {
            try {
                ClinicSetings clinicOpening = await _clinicalOpeningServices.ListAsync();
                return View(clinicOpening);
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClinicSetings clinic) {
            try {

                if (!ModelState.IsValid) {
                    return View(clinic);
                }

                if (clinic.InitialHour > clinic.EndHour) {
                    TempData["ErrorMessage"] = "Horario invalido";
                    return View(clinic);
                }

                List<Time> times = await _timeServices.FindAllActiveAsync();

                foreach (Time time in times) {
                    if (clinic.InitialHour.Hour > time.InitialHour.Hour || time.FinalHour.Hour > clinic.EndHour.Hour) {
                        TempData["ErrorMessage"] = "Existem médicos com horários de atendimento cadastrados fora do horário informado";
                        return View(clinic);
                    }

                    if (clinic.InitialHour.Hour == time.InitialHour.Hour || time.FinalHour.Hour == clinic.EndHour.Hour) {

                        if (clinic.InitialHour.Minute > time.InitialHour.Minute || time.FinalHour.Minute > clinic.EndHour.Minute) {
                            TempData["ErrorMessage"] = "Existem médicos com horários de atendimento cadastrados fora do horário informado";
                            return View(clinic);
                        }
                    }

                }

                ClinicSetings clinicDb = await _clinicalOpeningServices.ListAsync();

                clinicDb.EndHour = clinic.EndHour;
                clinicDb.InitialHour = clinic.InitialHour;
                clinicDb.City = clinic.City;
                clinicDb.Street = clinic.Street;
                clinicDb.Number = clinic.Number;
                clinicDb.Complement = clinic.Complement;
                clinicDb.District = clinic.District;
                clinicDb.State = clinic.State;
                clinicDb.AttestedText = clinic.AttestedText;
                clinicDb.RecipeText = clinic.RecipeText;

                await _clinicalOpeningServices.UpdateAsync(clinicDb);
                TempData["SuccessMessage"] = "Informações da clinica alteradas com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }
    }
}
