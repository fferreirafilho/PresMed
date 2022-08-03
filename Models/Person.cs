using PresMed.Models.Enums;

namespace PresMed.Models {
    public class Person {

        public long Id { get; set; }
        public string Name { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public long Cpf { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Complement { get; set; }
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
