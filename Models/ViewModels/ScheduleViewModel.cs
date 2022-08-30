using PresMed.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ScheduleViewModel {


        [Display(Name = "Horario de atendimento")]
        public DateTime HourAttendence { get; set; }
        [Display(Name = "Dia de atendimento")]
        public DateTime DayAttendence { get; set; }
        [Display(Name = "Medicos")]
        public int Doctor { get; set; }
        [Display(Name = "Paciente")]
        public Person Patient { get; set; }
        [Display(Name = "Status do atendimento")]
        public StatusAttendence StatusAttendence { get; set; }
        public Time Hour { get; set; }
        [Display(Name = "Medicos")]
        public List<Person> Doctors { get; set; }
        public List<Scheduling> Schedulings { get; set; }

    }
}
