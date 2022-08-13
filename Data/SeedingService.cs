using PresMed.Models;
using System.Linq;
using System;
using PresMed.Models.Enums;

namespace PresMed.Data {
    public class SeedingService {
        private readonly BancoContext _context;

        public SeedingService(BancoContext context) {
            _context = context;
        }

        public void Seed() {
            if (_context.Person.Any()) {
                return;

            }
            //string name, long phone, string email, string cpf, string street, string district, string state, string complement, string city, string number, UserStatus status, PersonType person, string user, string crm, string speciality, DateTime birthDate

            Person p1 = new Person("Fernando Ferreira Filho", 62981174693, "fernandoferreirrafilho@gmail.com", "70281834164", "36", "Nossa senhora da penha", "GO", null, "Goianésia", "486", UserStatus.Ativo, PersonType.Doctor, "FFERRREIRA", "123456798", "GO-9695", "Cardiologia", new DateTime(2000, 11, 04));
            Person p2 = new Person("Joao da Silva", 9223123372, "joaosilva@gmail.com", "88589476065", "18", "Centro", "GO", null, "Goianésia", "448", UserStatus.Inativado, PersonType.Doctor, "JSILVA", "123456798", "DF-1234", "Clinico Geral", new DateTime(2000, 04, 16));
            Person p3 = new Person("Antonio da Costa Silva", 9223123372, "antoniocostsasilva@gmail.com", "95743105073", "Jose Carrilho", "Boa Vista", "GO", null, "Goianésia", "1050", UserStatus.Ativo, PersonType.Doctor, "ACSILVA", "123456798", "MG-1234", "Cardiologista", new DateTime(1975, 01, 14));
            Person p4 = new Person("Elisagela Martins Machado", 8221348268, "elisagenlamachado@gmail.com", "90066048010", "14", "Universitario", "GO", null, "Goianésia", "35", UserStatus.Ativo, PersonType.Doctor, "EMMACHADO ", "123456798", "AP-1234", "Ginecologista", new DateTime(1985, 06, 01));
            Person p5 = new Person("Jennifer Faida", 4724215048, "jenniferfaida@gmail.com", "68713358030", "01", "Bela Vista", "GO", null, "Goianésia", "258", UserStatus.Inativado, PersonType.Doctor, "JFAIDA", "123456798", "SC-1234", "Pediatra", new DateTime(1963, 11, 10));



            _context.Person.AddRange(p1, p2, p3, p4, p5);
            _context.SaveChanges();
        }
    }
}
