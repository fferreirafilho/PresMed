using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Services;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class DoctorController : Controller {

        private readonly IDoctorServices _doctorService;

        public DoctorController(IDoctorServices doctorService) {
            _doctorService = doctorService;
        }

        public async Task<IActionResult> Index() {
            var list = await _doctorService.FindAllAsync();
            return View(list);
        }
        public IActionResult New() {
            return View();
        }

        public async Task<IActionResult> Edit(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public async Task<IActionResult> Details(int? id) {

            if (id == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            Doctor doctor = await _doctorService.FindByIdAsync(id.Value);
            if (doctor == null) {
                TempData["ErrorMessage"] = "ID não encontrado";
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(Doctor doctor) {
            doctor.Status = UserStatus.Ativo;
            await _doctorService.InsertAsync(doctor);
            return RedirectToAction("Index");

        }
    }
}
