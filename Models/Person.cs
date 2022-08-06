using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresMed.Models {
    public class Person {

        public long Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "O campo deve ter no minimo 3 letras")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Numero de telefone invalido")]
        [Display(Name = "Telefone")]
        public long? Phone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail invalido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [CpfValidation(ErrorMessage = "O CPF informado é invalido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 2 letras ou numeros")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [Display(Name = "Bairro")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 2 letras")]
        [Display(Name = "UF")]
        public string State { get; set; }

        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 2 numeros")]
        [Display(Name = "Numero")]
        public string Number { get; set; }
        public UserStatus Status { get; set; }

        public Person() { }

        public Person(string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, UserStatus status) {
            Name = name;
            Phone = phone;
            Email = email;
            Cpf = cpf;
            Street = street;
            District = district;
            State = state;
            Complement = complement;
            City = city;
            Number = number;
            Status = status;
        }
    }
}
