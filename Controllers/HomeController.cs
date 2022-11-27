using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    public class HomeController : Controller {

        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService) {
            _loginService = loginService;
        }

        public IActionResult Index() {

            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            if (person.PersonType == Models.Enums.PersonType.Doctor) {
                return RedirectToAction("Index", "Attendance");

            }

            return View(person);


        }


        public IActionResult ChangePassword() {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel pwd) {

            if (!ModelState.IsValid) {
                return View(pwd);
            }


            if (pwd.Password != pwd.ConfirmPassword) {
                TempData["ErrorMessage"] = $"As senhas não conferem";
                return View(pwd);
            }


            string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
            if (string.IsNullOrEmpty(sessionUser)) return null;

            Person person = JsonConvert.DeserializeObject<Person>(sessionUser);

            person.Password = pwd.Password;

            person.SetPasswordHash();

            await _loginService.ChangePasswordAsync(person);

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
