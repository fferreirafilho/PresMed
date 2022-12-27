using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using PresMed.Helper;
using PresMed.Models.Enums;
using PresMed.Models.ValidationModels;
using PresMed.Models.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PresMed.Models {
    public class Person {

        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo inválido ")]
        [MaxLength(50, ErrorMessage = "Campo inválido ")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Campo inválido")]
        [Display(Name = "Telefone")]

        public long? Phone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Campo inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [CpfValidation(ErrorMessage = "Campo inválido")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; }

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
        [Display(Name = "Numero")]
        public string Number { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo inválido")]
        [MaxLength(20, ErrorMessage = "Campo inválido")]
        [Display(Name = "Usuario")]
        public string User { get; set; }

        public string Password { get; set; }

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

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [DataType(DataType.Date, ErrorMessage = "Data inválida favor inserir novamente")]
        [Display(Name = "Data de nascimento")]
        [BirthDateValidation(ErrorMessage = "O usuário deve ter mais de 18 anos e menos de 130 anos")]
        public DateTime? BirthDate { get; set; }


        public Status Status { get; set; }

        public PersonType PersonType { get; set; }

        public Person() { }

        public Person(string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, Status status, PersonType person, string user, string password, string crm, string speciality, DateTime birthDate) {
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

        public static Person Parse(PersonAssistantViewModel assistant = null, PersonPatientViewModel patient = null) {
            Person person = new Person();

            if (assistant != null) {
                person = new Person(assistant.Name, assistant.Phone.Value, assistant.Email, assistant.Cpf, assistant.Street, assistant.District, assistant.State, assistant.Complement, assistant.City, assistant.Number, assistant.Status, assistant.PersonType, assistant.User, "", null, null, assistant.BirthDate.Value);
                person.Id = assistant.Id;
            }
            if (patient != null) {
                person = new Person(patient.Name, patient.Phone.Value, patient.Email, patient.Cpf, patient.Street, patient.District, patient.State, patient.Complement, patient.City, patient.Number, patient.Status, patient.PersonType, "", "", null, null, patient.BirthDate.Value);
                person.Id = patient.Id;
            }
            return person;
        }

        public static string PasswordGenerate() {
            string chars = "abcdefghjkmnpqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXIZ.*()@#$%,|";
            string pass = "";
            Random random = new Random();
            for (int f = 0; f < 10; f++) {
                pass += chars.Substring(random.Next(0, chars.Length - 1), 1);
            }

            return pass;
        }

        public static void SendMail(string emailMessage, string message, string title) {
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("testesapps51@gmail.com"));
            email.To.Add(MailboxAddress.Parse(emailMessage));
            email.Subject = title;
            email.Body = new TextPart(TextFormat.Plain) { Text = message };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("testesapps51@gmail.com", "fahtsroanthccqxd");
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        public bool ValidPassword(string password) {

            return Password == password.MakeHash();
        }


        public void SetPasswordHash() {
            Password = Password.MakeHash();
        }
    }
}
