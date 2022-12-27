﻿using iTextSharp.text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
    public class TimeController : Controller {

        private readonly ITimeServices _timeServices;
        private readonly ISchedulingServices _schedulingServices;
        private readonly IClinicSetingsServices _clinicOpeningServices;

        public TimeController(ITimeServices timeServices, ISchedulingServices schedulingServices, IClinicSetingsServices clinicOpeningServices) {
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
                    TempData["ErrorMessage"] = "Médico desativado no sistema";
                    return RedirectToAction("Index");
                }
                List<Time> listTime = await _timeServices.FindAllByPersonId(dbTime.Person.Id);
                TimeViewModel time = new TimeViewModel { Id = dbTime.Id, ServiceTime = dbTime.ServiceTime, FinalDay = dbTime.FinalDay, FinalHour = dbTime.FinalHour, HourPerDay = dbTime.HourPerDay, InitialDay = dbTime.InitialDay, InitialHour = dbTime.InitialHour, Person = dbTime.Person, ListTime = listTime };

                return View(time);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TimeViewModel time) {
            try {

                Time name = await _timeServices.FindByIdAsync(time.Id);
                time.Person = name.Person;
                time.ListTime = await _timeServices.FindAllByPersonId(time.Person.Id);

                if (time.InitialDay.ToShortDateString() == DateTime.Now.ToShortDateString()) {
                    TempData["ErrorMessage"] = "Data tem que ser maior que hoje";
                    return View(time);
                }


                if (time.InitialDay.Date < DateTime.Now.Date) {
                    TempData["ErrorMessage"] = "Data tem que ser maior que hoje";
                    return View(time);
                }


                ClinicSetings clinicOpening = await _clinicOpeningServices.ListAsync();

                if (time.InitialHour.Hour < clinicOpening.InitialHour.Hour || time.FinalHour.Hour > clinicOpening.EndHour.Hour) {
                    TempData["ErrorMessage"] = "Fora do horário de funcionamento da clínica";
                    return View(time);
                }

                if (clinicOpening.InitialHour.Hour == time.InitialHour.Hour || time.FinalHour.Hour == clinicOpening.EndHour.Hour) {

                    if (clinicOpening.InitialHour.Minute > time.InitialHour.Minute || time.FinalHour.Minute > clinicOpening.EndHour.Minute) {
                        TempData["ErrorMessage"] = "Fora do horário de funcionamento da clínica";
                        return View(time);
                    }
                }


                if (!ModelState.IsValid) {
                    Time timedb = await _timeServices.FindByIdAsync(time.Id);

                    time.InitialHour = timedb.InitialHour;
                    time.InitialDay = timedb.InitialDay;
                    time.HourPerDay = timedb.HourPerDay;
                    time.FinalDay = timedb.FinalDay;
                    time.FinalHour = timedb.FinalHour;
                    time.Person = timedb.Person;

                    return View(time);
                }

                if (time.InitialHour > time.FinalHour) {
                    TempData["ErrorMessage"] = "Horário invalido";
                    return View(time);
                }

                Time dbTime = await _timeServices.FindByIdAsync(time.Id);

                if (dbTime == null) {
                    TempData["ErrorMessage"] = "ID não encontado";
                    return RedirectToAction("Index");
                }

                if (dbTime.Person.Status == Status.Desativado) {
                    TempData["ErrorMessage"] = "Médico desativado no sistema";
                    return RedirectToAction("Index");
                }

                if (dbTime.InitialDay >= time.InitialDay) {
                    TempData["ErrorMessage"] = "Data inicial tem que ser maior que a data inicial da última alteração ";
                    return View(time);
                }


                var list = await _schedulingServices.FindBylargerDate(time.Person.Id, time.InitialDay);
                if (list.Count > 0) {
                    TempData["ErrorMessage"] = "Existem agendas marcadas para o futuro";
                    return View(time);
                }

                double minutes = time.FinalHour.Subtract(time.InitialHour).TotalMinutes;
                int min = time.ServiceTime.Minute;

                switch (min) {
                    case 00:
                        time.HourPerDay = (int)minutes / 60;
                        break;
                    case 15:
                        time.HourPerDay = (int)minutes / 15;
                        break;
                    case 30:
                        time.HourPerDay = (int)minutes / 30;
                        break;
                    case 45:
                        time.HourPerDay = (int)minutes / 45;
                        break;
                }

                dbTime.FinalDay = time.InitialDay.Subtract(TimeSpan.FromDays(1));
                time.Id = 0;
                await _timeServices.UpdateAsync(dbTime);
                Time insertTime = new Time {
                    InitialHour = time.InitialHour,
                    InitialDay = time.InitialDay,
                    HourPerDay = time.HourPerDay,
                    FinalDay = time.FinalDay,
                    FinalHour = time.FinalHour,
                    Person = time.Person,
                };

                await _timeServices.InsertAsync(insertTime);

                TempData["SuccessMessage"] = "Horário Alterado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }
    }
}