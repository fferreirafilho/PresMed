using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Prescription {
        public int Id { get; set; }
        public Attendance Attendance { get; set; }
        [Display(Name = "Medicamento")]
        public Medicine Medicine { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Horario")]
        public DateTime Time { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Periodo de dias")]
        public int Days { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Posologia")]
        public string Dosage { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Observações")]
        public string Observation { get; set; }

        public Prescription() { }
        public Prescription(Attendance attendance, Medicine medicine, DateTime time, int days, string dosage) {
            Attendance = attendance;
            Medicine = medicine;
            Time = time;
            Days = days;
            Dosage = dosage;
        }

    }
}
