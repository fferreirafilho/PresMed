using PresMed.Migrations;
using System;
using System.ComponentModel.DataAnnotations;
using static iTextSharp.text.pdf.AcroFields;

namespace PresMed.Models {
    public class ClinicSetings {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Horário de abertura da clínica")]
        public DateTime InitialHour { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Horário de fechamento da clínica")]
        public DateTime EndHour { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(40, ErrorMessage = "Campo inválido")]
        [Display(Name = "Bairro")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "UF")]
        public string State { get; set; }

        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(40, ErrorMessage = "Campo inválido")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [MinLength(1, ErrorMessage = "Campo inválido")]
        [MaxLength(7, ErrorMessage = "Campo inválido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Numero")]
        public string Number { get; set; }

        [MinLength(30, ErrorMessage = "Campo inválido")]
        [MaxLength(500, ErrorMessage = "Campo inválido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Texto do receituario")]
        public string RecipeText { get; set; }

        [MinLength(30, ErrorMessage = "Campo inválido")]
        [MaxLength(500, ErrorMessage = "Campo inválido")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Texto do atestado")]
        public string AttestedText { get; set; }

        public ClinicSetings() { }

        public ClinicSetings(DateTime initialHour, DateTime endHour, string street, string district, string state, string complement, string city, string number, string recipeText, string attestedText) {
            InitialHour = initialHour;
            EndHour = endHour;
            Street = street;
            District = district;
            State = state;
            Complement = complement;
            City = city;
            Number = number;
            RecipeText = recipeText;
            AttestedText = attestedText;
        }
    }
}
