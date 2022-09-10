using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
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
        private readonly IPatientServices _patientServices;

        public SchedulingController(IDoctorServices doctorServices, ISchedulingServices schedulingServices, ITimeServices timeServices, IProceduresServices proceduresServices, IPatientServices patientServices) {
            _doctorServices = doctorServices;
            _schedulingServices = schedulingServices;
            _timeServices = timeServices;
            _proceduresServices = proceduresServices;
            _patientServices = patientServices;
        }

        public async Task<IActionResult> Index() {
            Scheduling scheduling = new Scheduling();
            scheduling.DayAttendence = DateTime.Now;
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel() {
                Doctors = await _doctorServices.FindAllActiveAsync(),
                Scheduling = scheduling,

            };
            return View(scheduleViewModel);
        }

        public async Task<IActionResult> New(ScheduleViewModel scheduleViewModel) {

            if (DateTime.Now.Month > scheduleViewModel.Scheduling.DayAttendence.Month) {
                TempData["ErrorMessage"] = $"Data de agendamento invalido";
                return RedirectToAction(nameof(Index));
            }

            if (DateTime.Now.Day > scheduleViewModel.Scheduling.DayAttendence.Day) {
                TempData["ErrorMessage"] = $"Data de agendamento invalido";
                return RedirectToAction(nameof(Index));
            }

            if (DateTime.Now.Day == scheduleViewModel.Scheduling.DayAttendence.Day) {
                if (DateTime.Now.Hour > scheduleViewModel.Scheduling.HourAttendence.Hour) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }
            }
            scheduleViewModel.Scheduling.Doctor = await _doctorServices.FindByIdAsync(scheduleViewModel.Doctor);
            scheduleViewModel.Scheduling.HourAttendence = scheduleViewModel.Scheduling.HourAttendence;
            scheduleViewModel.Scheduling.DayAttendence = scheduleViewModel.Scheduling.DayAttendence;
            scheduleViewModel.Patients = await _patientServices.FindAllAsync();
            scheduleViewModel.Procedures = await _proceduresServices.FindAllActiveAsync();

            return View(scheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule(ScheduleViewModel scheduleViewModel) {
            scheduleViewModel.Doctors = await _doctorServices.FindAllActiveAsync();
            scheduleViewModel.Schedulings = await _schedulingServices.FindByIdAsync(scheduleViewModel.Doctor, scheduleViewModel.Scheduling.DayAttendence);
            scheduleViewModel.Hour = await _timeServices.FindScheduleByIdAsync(scheduleViewModel.Doctor);
            if (scheduleViewModel.Doctors.Count == 0) {
                return RedirectToAction(nameof(Index));
            }
            return View("Index", scheduleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ScheduleViewModel scheduleViewModel) {
            Scheduling scheduling = new Scheduling();
            scheduling.Procedures = await _proceduresServices.FindByIdAsync(scheduleViewModel.Procedure);
            scheduling.Doctor = await _doctorServices.FindByIdAsync(scheduleViewModel.Doctor);
            scheduling.HourAttendence = scheduleViewModel.Scheduling.HourAttendence;
            scheduling.DayAttendence = scheduleViewModel.Scheduling.DayAttendence;
            scheduling.Patient = await _patientServices.FindByIdAsync(scheduleViewModel.Patient);
            scheduling.StatusAttendence = StatusAttendence.Agendado;
            await _schedulingServices.InsertAsync(scheduling);
            TempData["SuccessMessage"] = $"Agendamento cadastrado com sucesso";
            return RedirectToAction(nameof(Index));

        }
    }
}
