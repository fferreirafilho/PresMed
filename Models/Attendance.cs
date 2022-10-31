using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Attendance {
        public int Id { get; set; }
        public Person Doctor { get; set; }
        public Person Patient { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(50, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Nome")]
        public string Report { get; set; }
        public Attendance() { }
        public Attendance(Person doctor, Person patient, string report) {
            Doctor = doctor;
            Patient = patient;
            Report = report;
        }
    }
}
