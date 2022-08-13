﻿using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class PersonAssistant {

        public long Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "O campo deve ter no minimo 3 letras")]
        [MaxLength(50, ErrorMessage = "O campo deve ter no minimo 50 letras")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Número de telefone invalido")]
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
        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 2 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Display(Name = "Rua")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [MaxLength(40, ErrorMessage = "O campo deve ter no minimo 40 letras")]
        [Display(Name = "Bairro")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(2, ErrorMessage = "O campo deve ter no minimo 2 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Display(Name = "UF")]
        public string State { get; set; }

        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [MaxLength(40, ErrorMessage = "O campo deve ter no minimo 40 letras")]
        [Display(Name = "Complemento")]
        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Display(Name = "Cidade")]
        public string City { get; set; }

        [MinLength(1, ErrorMessage = "O campo deve ter no minimo 1 numero")]
        [MaxLength(7, ErrorMessage = "O campo deve ter no minimo 7 letras")]
        [Display(Name = "Numero")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "O campo deve ter no minimo 3 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data invalida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter mais de 18 anos")]
        public DateTime? BirthDate { get; set; }


        public UserStatus Status { get; set; }

        public PersonType PersonType { get; set; }



        public static PersonAssistant Parse(Person person) {

            PersonAssistant personAssistant = new PersonAssistant { Id = person.Id, Name = person.Name, Phone = person.Phone.Value, Email = person.Email, Cpf = person.Cpf, Street = person.Street, District = person.District, State = person.State, Complement = person.Complement, City = person.City, Number = person.Number, Status = person.Status, PersonType = person.PersonType, User = person.User, BirthDate = person.BirthDate };


            return personAssistant;

        }
    }

}

