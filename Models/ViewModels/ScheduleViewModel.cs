using PresMed.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ScheduleViewModel {


        [Display(Name = "Agenda")]
        public Scheduling Scheduling { get; set; }
        [Display(Name = "Horario")]
        public Time Hour { get; set; }
        [Display(Name = "Medico")]
        public int Doctor { get; set; }
        [Display(Name = "Paciente")]
        public int Patient { get; set; }
        [Display(Name = "Procedimento")]
        public int Procedure { get; set; }
        [Display(Name = "Medicos")]
        public List<Person> Doctors { get; set; }
        public List<Scheduling> Schedulings { get; set; }
        [Display(Name = "Pacientes")]
        public List<Person> Patients { get; set; }
        [Display(Name = "Procedimento")]
        public List<Procedures> Procedures { get; set; }
    }
}
