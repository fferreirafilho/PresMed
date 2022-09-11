using PresMed.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Scheduling {
        public int Id { get; set; }

        [Display(Name = "Horario de atendimento")]
        public DateTime HourAttendence { get; set; }
        [Display(Name = "Dia de atendimento")]
        public DateTime DayAttendence { get; set; }
        [Display(Name = "Medico")]
        public Person Doctor { get; set; }
        [Display(Name = "Paciente")]
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
