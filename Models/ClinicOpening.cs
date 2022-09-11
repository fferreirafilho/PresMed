using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class ClinicOpening {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Horario de abertura da clinica")]
        public DateTime InitialHour { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Horario de fechamento da clinica")]
        public DateTime EndHour { get; set; }

        public ClinicOpening() { }
        public ClinicOpening(DateTime initialHour, DateTime endHour) {
            InitialHour = initialHour;
            EndHour = endHour;
        }
    }
}
