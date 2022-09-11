using Microsoft.AspNetCore.Mvc;
using PresMed.Services;
using System;

namespace PresMed.Controllers {
    public class ClinicalOpeningController : Controller {

        private readonly IClinicalOpeningServices _clinicalOpeningServices;

        public ClinicalOpeningController(IClinicalOpeningServices clinicalOpeningServices) {
            _clinicalOpeningServices = clinicalOpeningServices;
        }

        public IActionResult Index() {
            try {
                return View();
            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {e.Message}";
                return View();
            }

        }
    }
}
