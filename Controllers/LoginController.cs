using Microsoft.AspNetCore.Mvc;

namespace PresMed.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
