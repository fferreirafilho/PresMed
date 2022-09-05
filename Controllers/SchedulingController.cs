using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    public class SchedulingController : Controller {
        private readonly IDoctorServices _doctorServices;
        private readonly ISchedulingServices _schedulingServices;
        private readonly ITimeServices _timeServices;
        private readonly IProceduresServices _proceduresServices;

        public SchedulingController(IDoctorServices doctorServices, ISchedulingServices schedulingServices, ITimeServices timeServices, IProceduresServices proceduresServices) {
            _doctorServices = doctorServices;
            _schedulingServices = schedulingServices;
            _timeServices = timeServices;
            _proceduresServices = proceduresServices;
        }

        public async Task<IActionResult> Index() {
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel() {
                Doctors = await _doctorServices.FindAllActiveAsync(),
                DayAttendence = DateTime.Now,

            };
            return View(scheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule(ScheduleViewModel scheduleViewModel) {
            scheduleViewModel.Doctors = await _doctorServices.FindAllActiveAsync();
            scheduleViewModel.Schedulings = await _schedulingServices.FindByIdAsync(scheduleViewModel.Doctor, scheduleViewModel.DayAttendence);
            scheduleViewModel.Hour = await _timeServices.FindScheduleByIdAsync(scheduleViewModel.Doctor);
            if (scheduleViewModel.Doctors.Count == 0) {
                return RedirectToAction(nameof(Index));
            }
            return View("Index", scheduleViewModel);
        }
    }
}
