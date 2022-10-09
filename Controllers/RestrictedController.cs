using Microsoft.AspNetCore.Mvc;
using PresMed.Filters;

namespace PresMed.Controllers {
    [PageForUserLogged]

    public class RestrictedController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
