using PresMed.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ScheduleViewModel {



        public Scheduling Scheduling { get; set; }
        public Time Hour { get; set; }
        public int Doctor { get; set; }
        public int Patient { get; set; }
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
