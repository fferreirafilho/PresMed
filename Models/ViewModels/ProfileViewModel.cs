using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ProfileViewModel {

        public int id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "CRM")]
        public string Crm { get; set; }

        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Especialidade")]
        public string Speciality { get; set; }
    }
}
