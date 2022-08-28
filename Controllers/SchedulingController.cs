using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class SchedulingController : Controller {

        private readonly IDoctorServices _doctorServices;
        private readonly ITimeServices _timeServices;
        private readonly ISchedulingServices _schedulingServices;

        public SchedulingController(IDoctorServices doctorServices, ITimeServices timeServices, ISchedulingServices schedulingServices) {
            _doctorServices = doctorServices;
            _timeServices = timeServices;
            _schedulingServices = schedulingServices;
        }

        public async Task<IActionResult> Index() {

            SchedulingViewModel schedulingViewModel = new SchedulingViewModel() {
                Doctors = await _doctorServices.FindAllActiveAsync(),
                AttendenceDate = DateTime.Now,
            };
            return View(schedulingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Scheduling(SchedulingViewModel schedulingViewModel) {
            schedulingViewModel.Doctors = await _doctorServices.FindAllActiveAsync();

            if (!ModelState.IsValid) {

                return View("Index", schedulingViewModel);
            }
            schedulingViewModel.Time = await _timeServices.FindByIdAsync(schedulingViewModel.IdDoctor);
            schedulingViewModel.Schedulings = await _schedulingServices.FindByDateAndIdAsync(schedulingViewModel.IdDoctor, schedulingViewModel.AttendenceDate);

            return View("Index", schedulingViewModel);
        }
    }
}
