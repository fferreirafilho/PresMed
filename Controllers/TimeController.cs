using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class TimeController : Controller {

        private readonly ITimeServices _timeServices;
        private readonly ISchedulingServices _schedulingServices;
        private readonly IClinicOpeningServices _clinicOpeningServices;

        public TimeController(ITimeServices timeServices, ISchedulingServices schedulingServices, IClinicOpeningServices clinicOpeningServices) {
            _timeServices = timeServices;
            _schedulingServices = schedulingServices;
            _clinicOpeningServices = clinicOpeningServices;
        }

        public async Task<IActionResult> Index() {
            try {
                var list = await _timeServices.FindAllActiveAsync();
                return View(list);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Edit(int? id) {
            try {
                if (id == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }
                Time dbTime = await _timeServices.FindByIdAsync(id.Value);

                if (dbTime == null) {
                    TempData["ErrorMessage"] = "ID não encontrado";
                    return RedirectToAction("Index");
                }

                if (dbTime.Person.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Medico desativado no sistema";
                    return RedirectToAction("Index");
                }

                return View(dbTime);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Time time) {
            try {
                Time name = await _timeServices.FindByIdAsync(time.Id);
                time.Person = name.Person;

                ClinicOpening clinicOpening = await _clinicOpeningServices.ListAsync();

                if (time.InitialHour.Hour < clinicOpening.InitialHour.Hour || time.FinalHour.Hour > clinicOpening.EndHour.Hour) {
                    TempData["ErrorMessage"] = "Fora do horario de funcionamento da clinica";
                    return RedirectToAction("Index");
                }

                if (clinicOpening.InitialHour.Hour == time.InitialHour.Hour || time.FinalHour.Hour == clinicOpening.EndHour.Hour) {

                    if (clinicOpening.InitialHour.Minute > time.InitialHour.Minute || time.FinalHour.Minute > clinicOpening.EndHour.Minute) {
                        TempData["ErrorMessage"] = "Fora do horario de funcionamento da clinica";
                        return RedirectToAction("Index");
                    }
                }

                if (!ModelState.IsValid) {
                    time = await _timeServices.FindByIdAsync(time.Id);
                    return View(time);
                }

                if (time.InitialHour > time.FinalHour) {
                    TempData["ErrorMessage"] = "Horario invalido";
                    return View(time);
                }

                Time dbTime = await _timeServices.FindByIdAsync(time.Id);

                if (dbTime == null) {
                    TempData["ErrorMessage"] = "ID não encontado";
                    return RedirectToAction("Index");
                }

                if (dbTime.Person.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Medico desativado no sistema";
                    return RedirectToAction("Index");
                }

                var list = await _schedulingServices.FindBylargerDate(time.Person.Id, DateTime.Now);
                if (list.Count > 0) {
                    TempData["ErrorMessage"] = "Existem agendas marcadas para o futuro";
                    return RedirectToAction("Index");
                }

                double minutes = time.FinalHour.Subtract(time.InitialHour).TotalMinutes;
                int min = time.ServiceTime.Minute;

                switch (min) {
                    case 00:
                        dbTime.HourPerDay = (int)minutes / 60;
                        break;
                    case 15:
                        dbTime.HourPerDay = (int)minutes / 15;
                        break;
                    case 30:
                        dbTime.HourPerDay = (int)minutes / 30;
                        break;
                    case 45:
                        dbTime.HourPerDay = (int)minutes / 45;
                        break;
                }

                dbTime.FinalHour = time.FinalHour;
                dbTime.InitialHour = time.InitialHour;
                dbTime.ServiceTime = time.ServiceTime;

                await _timeServices.UpdateAsync(dbTime);
                TempData["SuccessMessage"] = "Horario Alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }
    }
}