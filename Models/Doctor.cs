using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Doctor : Person {
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "O campo deve ter no minimo 3 letras")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        public string Password { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [Display(Name = "CRM")]
        public string Crm { get; set; }

        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Especialidade")]
        public string Speciality { get; set; }


        public Doctor() : base() { }

        public Doctor(string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, string user, string password, string crm, string speciality, UserStatus status) : base(name, phone, email, cpf, street, district, state, complement, city, number, status) {
            User = user;
            Password = password;
            Crm = crm;
            Speciality = speciality;

        }
    }
}
