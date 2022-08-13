using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using PresMed.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresMed.Models {
    public class Person {

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

        public string Password { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Display(Name = "CRM")]
        public string Crm { get; set; }

        [MinLength(4, ErrorMessage = "O campo deve ter no minimo 4 letras")]
        [MaxLength(20, ErrorMessage = "O campo deve ter no minimo 20 letras")]
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Especialidade")]
        public string Speciality { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data invalida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter mais de 18 anos e menos de 130 anos")]
        public DateTime? BirthDate { get; set; }


        public UserStatus Status { get; set; }

        public PersonType PersonType { get; set; }

        public Person() { }

        public Person(string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, UserStatus status, PersonType person, string user, string password, string crm, string speciality, DateTime birthDate) {
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
            PersonType = person;
            User = user;
            Crm = crm;
            Speciality = speciality;
            BirthDate = birthDate;
            Password = password;
        }

        public static Person Parse(PersonAssistant assistant = null, PersonPatient patient = null) {
            Person person = new Person();

            if (assistant != null) {
                person = new Person(assistant.Name, assistant.Phone.Value, assistant.Email, assistant.Cpf, assistant.Street, assistant.District, assistant.State, assistant.Complement, assistant.City, assistant.Number, assistant.Status, assistant.PersonType, assistant.User, "", null, null, assistant.BirthDate.Value);
                person.Id = assistant.Id;
            }
            if (patient != null) {
                person = new Person(patient.Name, patient.Phone.Value, patient.Email, patient.Cpf, patient.Street, patient.District, patient.State, patient.Complement, patient.City, patient.Number, patient.Status, patient.PersonType, patient.User, "", null, null, patient.BirthDate.Value);
                person.Id = patient.Id;
            }


            return person;
        }
    }
}
