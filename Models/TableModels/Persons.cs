using PresMed.Models.Enums;
using System;

namespace PresMed.Models.TableModels {
    public class Persons : Person {
        public string Crm { get; set; }
        public string Speciality { get; set; }

        public DateTime BirthDate { get; set; }


        public Persons() : base() { }

        public Persons(string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, string user, string password, string crm, string speciality, UserStatus status, PersonType person, DateTime birthDate) : base(name, phone, email, cpf, street, district, state, complement, city, number, status, person, user) {
            User = user;
            Password = password;
            Crm = crm;
            Speciality = speciality;
            BirthDate = birthDate;
        }
    }
}
