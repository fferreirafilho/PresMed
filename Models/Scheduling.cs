using PresMed.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Scheduling {
        public int Id { get; set; }

        [Display(Name = "Horario de atendimento")]
        public DateTime HourAttendence { get; set; }
        [Display(Name = "Horario de atendimento")]
        public DateTime DayAttendence { get; set; }
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        [Display(Name = "Status do atendimento")]
        public StatusAttendence StatusAttendence { get; set; }

        public Procedures Procedures { get; set; }

        public Scheduling() { }

        public Scheduling(DateTime hourAttendence, Person doctor, Person patient, StatusAttendence statusAttendence, Procedures procedures) {
            HourAttendence = hourAttendence;
            Doctor = doctor;
            Patient = patient;
            StatusAttendence = statusAttendence;
            Procedures = procedures;
        }
    }
}
