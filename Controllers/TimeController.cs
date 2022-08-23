using Microsoft.AspNetCore.Mvc;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System.Threading.Tasks;
using System.Linq;

namespace PresMed.Controllers {
    public class TimeController : Controller {

        private readonly ITimeServices _timeServices;
        private readonly IDoctorServices _doctorServices;

        public TimeController(ITimeServices timeServices, IDoctorServices doctorServices) {
            _timeServices = timeServices;
            _doctorServices = doctorServices;
        }

        public async Task<IActionResult> Index() {
            TimeViewModel timeViewModel = new TimeViewModel();
            var list = await _timeServices.FindAllAsync();
            timeViewModel.Time = list.Where(x => x.Person.Status == Models.Enums.Status.Ativo).ToList();
            timeViewModel.Person = await _doctorServices.FindAllActiveAsync();
            return View(timeViewModel);
        }
    }
}
