using Microsoft.AspNetCore.Mvc;

namespace PresMed.Controllers {
    public class DoctorController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
