using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Attendance {
        public int Id { get; set; }
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        [Required(ErrorMessage = "Campo invalido")]
        [Display(Name = "Relatorio de atendimento")]
        public string Report { get; set; }

        public ICollection<Medicine> Medicine { get; set; }
        public Attendance() { }
        public Attendance(int id, Person doctor, Person patient, string report) {
            Id = id;
            Doctor = doctor;
            Patient = patient;
            Report = report;
        }
    }
}
