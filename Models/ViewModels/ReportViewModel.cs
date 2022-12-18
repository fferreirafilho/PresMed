using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ReportViewModel {

        [Required(ErrorMessage = "Campo não pode ser vazio")]
        [Display(Name = "Data inicial")]
        public DateTime Initial { get; set; }

        [Required(ErrorMessage = "Campo não pode ser vazio")]
        [Display(Name = "Data final")]
        public DateTime Final { get; set; }

        public int DoctorId { get; set; }
        [Display(Name = "Medico")]
        public List<Person> ListDoctors { get; set; }
    }
}
