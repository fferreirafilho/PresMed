using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PresMed.Helper;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class LoginController : Controller {

        private readonly ILoginService _loginService;
        private readonly ISessionUser _session;

        public LoginController(ILoginService loginService, ISessionUser sessionUser) {
            _loginService = loginService;
            _session = sessionUser;
        }

        public IActionResult Index() {
            // se usuario estiver logado redirecionar para a home
            if (_session.FindSessionUser() != null) {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Exit() {
            _session.removeSessionUser();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login) {
            try {
                if (!ModelState.IsValid) {
                    return View("Index", login);
                }
                Person person = await _loginService.FindByLoginAsync(login.User);

                if (person != null) {

                    if (person.Status != Status.Ativo) {
                        TempData["ErrorMessage"] = $"Usuário desabilitado, favor procurar sua clínica";
                        return View("Index");
                    }

                    if (person.ValidPassword(login.Password)) {
                        _session.createSessionUser(person);
                        return RedirectToAction("Index", "Home");
                    }

                }
                TempData["ErrorMessage"] = $"Usuário ou senha invalido";
                return View("Index");


            }
            catch (Exception e) {
                TempData["ErrorMessage"] = $"Erro ao fazer login, ERRO: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
