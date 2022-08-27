using Microsoft.AspNetCore.Mvc;

namespace PresMed.Controllers {
    public class SchedulingController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
