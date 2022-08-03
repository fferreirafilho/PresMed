using PresMed.Models.Enums;

namespace PresMed.Models {
    public class Doctor : Person {
        public string User { get; set; }
        public string Password { get; set; }
        public string Crm { get; set; }
        public string Speciality { get; set; }


        public Doctor() : base() { }

        public Doctor(string name, long phone, string email, long cpf, string street, string district, string state, string complement, string city, string number, string user, string password, string crm, string speciality, UserStatus status) : base(name, phone, email, cpf, street, district, state, complement, city, number, status) {
            User = user;
            Password = password;
            Crm = crm;
            Speciality = speciality;

        }
    }
}
