using PresMed.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Scheduling {
        public int Id { get; set; }
        [Display(Name = "Horario de atendimento")]
        public DateTime HourAttendence { get; set; }
        public Person Medic { get; set; }
        public Person Pacient { get; set; }
        [Display(Name = "Status do atendimento")]
        public StatusAttendence StatusAttendence { get; set; }

        public Scheduling() { }

        public Scheduling(DateTime hourAttendence, Person medic, Person pacient, StatusAttendence statusAttendence) {
            HourAttendence = hourAttendence;
            Medic = medic;
            Pacient = pacient;
            StatusAttendence = statusAttendence;
        }
    }
}
