using Microsoft.AspNetCore.Mvc;
using PresMed.Models;
using PresMed.Models.Enums;
using PresMed.Models.ViewModels;
using PresMed.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PresMed.Filters;

namespace PresMed.Controllers {
    [PageForUserLogged]
    [PageOnlyAssistant]
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
            try {
                Scheduling scheduling = new Scheduling();
                scheduling.DayAttendence = DateTime.Now;
                ScheduleViewModel scheduleViewModel = new ScheduleViewModel() {
                    Doctors = await _doctorServices.FindAllActiveAsync(),
                    Scheduling = scheduling,

                };
                return View(scheduleViewModel);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        public async Task<IActionResult> New(ScheduleViewModel scheduleViewModel) {
            try {
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
                scheduleViewModel.Procedures = await _proceduresServices.FindAllActiveAsync();

                if (scheduleViewModel.Procedures.Count <= 0) {
                    TempData["ErrorMessage"] = $"Não existe nenhum procedimento ativo";
                    return RedirectToAction(nameof(Index));
                }

                scheduleViewModel.Scheduling.Doctor = await _doctorServices.FindByIdAsync(scheduleViewModel.Doctor);
                scheduleViewModel.Scheduling.HourAttendence = scheduleViewModel.Scheduling.HourAttendence;
                scheduleViewModel.Scheduling.DayAttendence = scheduleViewModel.Scheduling.DayAttendence;
                scheduleViewModel.Patients = await _patientServices.FindAllAsync();


                return View(scheduleViewModel);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        public async Task<IActionResult> Confirm(int? Id) {
            try {
                if (Id == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                Scheduling scheduling = await _schedulingServices.FindByIdAsync(Id.Value);

                if (scheduling == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (scheduling.StatusAttendence == StatusAttendence.Finalizado) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Month > scheduling.DayAttendence.Month) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day > scheduling.DayAttendence.Day) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day == scheduling.DayAttendence.Day) {
                    if (DateTime.Now.Hour > scheduling.HourAttendence.Hour) {
                        TempData["ErrorMessage"] = $"Data de agendamento invalido";
                        return RedirectToAction(nameof(Index));
                    }
                }

                return View(scheduling);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        public async Task<IActionResult> Delete(int? Id) {
            try {
                if (Id == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                Scheduling scheduling = await _schedulingServices.FindByIdAsync(Id.Value);

                if (scheduling == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (scheduling.StatusAttendence == StatusAttendence.Finalizado) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }


                if (DateTime.Now.Month > scheduling.DayAttendence.Month) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day > scheduling.DayAttendence.Day) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day == scheduling.DayAttendence.Day) {
                    if (DateTime.Now.Hour > scheduling.HourAttendence.Hour) {
                        TempData["ErrorMessage"] = $"Data de agendamento invalido";
                        return RedirectToAction(nameof(Index));
                    }
                }
                return View(scheduling);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Schedule(ScheduleViewModel scheduleViewModel) {
            try {
                //return Json(scheduleViewModel);
                scheduleViewModel.Doctors = await _doctorServices.FindAllActiveAsync();

                if (scheduleViewModel.Doctors.Count == 0) {
                    return RedirectToAction(nameof(Index));
                }

                IEnumerable<Time> listTime = await _timeServices.FindScheduleByIdAsync(scheduleViewModel.Doctor, scheduleViewModel.Scheduling.DayAttendence);

                var timeJob = listTime.Where(x => x.InitialDay <= scheduleViewModel.Scheduling.DayAttendence && x.FinalDay >= scheduleViewModel.Scheduling.DayAttendence).ToList();

                scheduleViewModel.Hour = timeJob.Count() == 0 ? await _timeServices.FindScheduleByIdAndFinalDateNullAsync(scheduleViewModel.Doctor) : timeJob[0];

                int minutes = scheduleViewModel.Hour.ServiceTime.Minute == 0 ? 60 : scheduleViewModel.Hour.ServiceTime.Minute;

                List<Scheduling> list = await _schedulingServices.FindByIdAndDateAsync(scheduleViewModel.Doctor, scheduleViewModel.Scheduling.DayAttendence);
                Scheduling[] array = new Scheduling[scheduleViewModel.Hour.HourPerDay];
                Scheduling[] listArray = list.ToArray();

                for (int i = 0; i < scheduleViewModel.Hour.HourPerDay; i++) {
                    Scheduling sc = new Scheduling();
                    sc.HourAttendence = scheduleViewModel.Hour.InitialHour.AddMinutes(minutes * i);
                    sc.StatusAttendence = StatusAttendence.Livre;
                    array[i] = sc;
                }

                for (int i = 0; i < scheduleViewModel.Hour.HourPerDay; i++) {
                    for (int j = 0; j < listArray.Length; j++) {
                        if (array[i].HourAttendence.ToShortTimeString() == listArray[j].HourAttendence.ToShortTimeString()) {
                            array[i] = listArray[j];
                        }
                    }
                }


                scheduleViewModel.Schedulings = array.ToList();

                return View(nameof(Index), scheduleViewModel);
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View(nameof(Index), scheduleViewModel);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ScheduleViewModel scheduleViewModel) {
            try {
                Scheduling scheduling = new Scheduling();

                if (scheduleViewModel == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }


                if (scheduling.StatusAttendence == StatusAttendence.Finalizado) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }


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
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm(int Id) {
            try {

                Scheduling scheduling = await _schedulingServices.FindByIdAsync(Id);

                if (scheduling == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (scheduling.StatusAttendence == StatusAttendence.Finalizado) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }


                if (DateTime.Now.Month > scheduling.DayAttendence.Month) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day > scheduling.DayAttendence.Day) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day == scheduling.DayAttendence.Day) {
                    if (DateTime.Now.Hour > scheduling.HourAttendence.Hour) {
                        TempData["ErrorMessage"] = $"Data de agendamento invalido";
                        return RedirectToAction(nameof(Index));
                    }
                }

                scheduling.StatusAttendence = StatusAttendence.Confirmado;
                await _schedulingServices.UpdateAsync(scheduling);
                TempData["SuccessMessage"] = $"Agendamento confirmado com sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id) {
            try {
                Scheduling scheduling = await _schedulingServices.FindByIdAsync(Id);

                if (scheduling == null) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (scheduling.StatusAttendence == StatusAttendence.Finalizado) {
                    TempData["ErrorMessage"] = $"ID invalido";
                    return RedirectToAction(nameof(Index));
                }


                if (DateTime.Now.Month > scheduling.DayAttendence.Month) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day > scheduling.DayAttendence.Day) {
                    TempData["ErrorMessage"] = $"Data de agendamento invalido";
                    return RedirectToAction(nameof(Index));
                }

                if (DateTime.Now.Day == scheduling.DayAttendence.Day) {
                    if (DateTime.Now.Hour > scheduling.HourAttendence.Hour) {
                        TempData["ErrorMessage"] = $"Data de agendamento invalido";
                        return RedirectToAction(nameof(Index));
                    }
                }

                await _schedulingServices.Delete(scheduling);
                TempData["SuccessMessage"] = $"Agendamento excluido com sucesso";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                TempData["ErrorMessage"] = $"Erro ao listar, erro: {ex.Message}";
                return View();
            }


        }
    }
}
