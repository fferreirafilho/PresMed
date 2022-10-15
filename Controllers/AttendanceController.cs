using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresMed.Filters;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyDoctor]
    public class AttendanceController : Controller {

        private readonly ISchedulingServices _schedulingServices;
        private readonly ITimeServices _timeServices;

        public AttendanceController(ISchedulingServices schedulingServices, ITimeServices timeServices) {
            _schedulingServices = schedulingServices;
            _timeServices = timeServices;
        }

        public async Task<IActionResult> Index() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                attendance.Hour = await _timeServices.FindScheduleByIdAsync(person.Id);

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);


                var newList = list.Where(x => x.StatusAttendence == StatusAttendence.Confirmado);

                attendance.Schedulings = newList;

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Attended() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                attendance.Hour = await _timeServices.FindScheduleByIdAsync(person.Id);

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);


                var newList = list.Where(x => x.StatusAttendence == StatusAttendence.Finalizado);

                attendance.Schedulings = newList;

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> Scheduling() {
            try {

                string sessionUser = HttpContext.Session.GetString("sessionLoggedUser");
                if (string.IsNullOrEmpty(sessionUser)) return null;

                Person person = JsonConvert.DeserializeObject<Person>(sessionUser);
                AttendanceViewModel attendance = new AttendanceViewModel { Person = person };

                attendance.Hour = await _timeServices.FindScheduleByIdAsync(person.Id);

                int minutes = attendance.Hour.ServiceTime.Minute == 0 ? 60 : attendance.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(attendance.Person.Id, DateTime.Now);
                Scheduling[] array = new Scheduling[attendance.Hour.HourPerDay];
                Scheduling[] listArray = list.ToArray();

                for (int i = 0; i < attendance.Hour.HourPerDay; i++) {
                    Scheduling sc = new Scheduling();
                    sc.HourAttendence = attendance.Hour.InitialHour.AddMinutes(minutes * i);
                    sc.StatusAttendence = StatusAttendence.Livre;
                    array[i] = sc;
                }

                for (int i = 0; i < attendance.Hour.HourPerDay; i++) {
                    for (int j = 0; j < listArray.Length; j++) {
                        if (array[i].HourAttendence.ToShortTimeString() == listArray[j].HourAttendence.ToShortTimeString()) {
                            array[i] = listArray[j];
                        }
                    }
                }


                attendance.Schedulings = array.ToList();

                return View(attendance);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }
        }

    }
}
