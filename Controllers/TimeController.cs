using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
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

            return View(dbTime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Time time) {

            if (!ModelState.IsValid) {
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

            dbTime.FinalHour = time.FinalHour;
            dbTime.InitialHour = time.InitialHour;

            await _timeServices.UpdateAsync(dbTime);
            TempData["SuccessMessage"] = "Horario Alterado com sucesso";
            return RedirectToAction("Index");
        }
    }
}
