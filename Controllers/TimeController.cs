using Microsoft.AspNetCore.Mvc;
using PresMed.Models.Enums;
using PresMed.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class TimeController : Controller {

        private readonly ITimeServices _timeServices;
        private readonly IDoctorServices _doctorServices;

        public TimeController(ITimeServices timeServices, IDoctorServices doctorServices) {
            _timeServices = timeServices;
            _doctorServices = doctorServices;
        }

        public async Task<IActionResult> Index() {

            return View();
        }
    }
}
