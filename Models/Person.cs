using PresMed.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Person {

        public long Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public long Cpf { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string District { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string State { get; set; }

        public string Complement { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        public string City { get; set; }

        public string Number { get; set; }
        public UserStatus Status { get; set; }

        public Person() { }

        public Person(string name, long phone, string email, long cpf, string street, string district, string state, string complement, string city, string number, UserStatus status) {
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
