using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class PersonAssistantViewModel {

        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido ")]
        [MaxLength(50, ErrorMessage = "Campo invalido ")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Campo invalido")]
        [Display(Name = "Telefone")]

        public long? Phone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Campo invalido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [CpfValidation(ErrorMessage = "Campo invalido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(40, ErrorMessage = "Campo invalido")]
        [Display(Name = "Bairro")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "UF")]
        public string State { get; set; }

        [MinLength(4, ErrorMessage = "Campo invalido")]
        [MaxLength(40, ErrorMessage = "Campo invalido")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [MinLength(1, ErrorMessage = "Campo invalido")]
        [MaxLength(7, ErrorMessage = "Campo invalido")]
        [Display(Name = "Numero")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo invalido")]
        [MaxLength(20, ErrorMessage = "Campo invalido")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data invalida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter de menos de 130 anos")]
        public DateTime? BirthDate { get; set; }


        public Status Status { get; set; }

        public PersonType PersonType { get; set; }



        public static PersonAssistantViewModel Parse(Person person) {

            PersonAssistantViewModel personAssistant = new PersonAssistantViewModel { Id = person.Id, Name = person.Name, Phone = person.Phone.Value, Email = person.Email, Cpf = person.Cpf, Street = person.Street, District = person.District, State = person.State, Complement = person.Complement, City = person.City, Number = person.Number, Status = person.Status, PersonType = person.PersonType, User = person.User, BirthDate = person.BirthDate };


            return personAssistant;

        }
    }

}

