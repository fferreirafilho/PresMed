﻿using Microsoft.AspNetCore.Mvc;

namespace PresMed.Controllers {
    public class AssistantController : Controller {
        public IActionResult Index() {
            return View();
        }

        public IActionResult New() {
            return View();
        }
    }
}
