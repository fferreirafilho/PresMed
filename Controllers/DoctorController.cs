using Microsoft.AspNetCore.Mvc;

namespace PresMed.Controllers {
    public class DoctorController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult New() {
            return View();
        }
        public IActionResult Edit() {
            return View();
        }
    }
}
