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

        public TimeController(ITimeServices timeServices) {
            _timeServices = timeServices;
        }

        public async Task<IActionResult> Index() {
            var list = await _timeServices.FindAllActiveAsync();
            return View(list);
        }

        public async Task<IActionResult> Edit(int? id) {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Time time) {
            Time name = await _timeServices.FindByIdAsync(time.Id);
            time.Person = name.Person;
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

            int h1 = time.InitialHour.Hour;
            int h2 = time.FinalHour.Hour;

            int times = time.ServiceTime.Hour;

            dbTime.HourPerDay = (h2 - h1) / times;
            dbTime.FinalHour = time.FinalHour;
            dbTime.InitialHour = time.InitialHour;
            dbTime.ServiceTime = time.ServiceTime;

            await _timeServices.UpdateAsync(dbTime);
            TempData["SuccessMessage"] = "Horario Alterado com sucesso";
            return RedirectToAction("Index");
        }
    }
}