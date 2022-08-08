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
            if (_context.Doctor.Any()) {
                return;

            }
            Doctor d1 = new Doctor("Fernando Ferreira Filho", 62981174693, "fernandoferreirrafilho@gmail.com", "70281834164", "36", "Nossa Senhora da Penha", "GO", null, "Goianésia", "486", "FFERREIRA", "123456798", "GO-6878", "Ortopedista", UserStatus.Ativo, PersonType.Doctor, new DateTime(1997, 12, 31));
            Doctor d2 = new Doctor("Joao da Silva", 9223123372, "joaosilva@gmail.com", "88589476065", "18", "Centro", "GO", null, "Goianésia", "448", "JSILVA", "123456798", "DF-1234", "Clinico Geral", UserStatus.Ativo, PersonType.Doctor, new DateTime(2000, 04, 16));
            Doctor d3 = new Doctor("Antonio da Costa Silva", 9223123372, "antoniocostsasilva@gmail.com", "95743105073", "Jose Carrilho", "Boa Vista", "GO", null, "Goianésia", "1050", "ACSILVA", "123456798", "MG-1234", "Cardiologista", UserStatus.Ativo, PersonType.Doctor, new DateTime(1975, 01, 14));
            Doctor d4 = new Doctor("Elisagela Martins Machado", 8221348268, "elisagenlamachado@gmail.com", "90066048010", "14", "Universitario", "GO", null, "Goianésia", "35", "EMMACHADO", "123456798", "AP-1234", "Ginecologista", UserStatus.Ativo, PersonType.Doctor, new DateTime(1985, 06, 01));
            Doctor d5 = new Doctor("Jennifer Faida ", 4724215048, "jenniferfaida@gmail.com", "68713358030", "01", "Bela Vista", "GO", null, "Goianésia", "185", "JFAIDA", "123456798", "SC-1234", "Pediatra", UserStatus.Inativado, PersonType.Doctor, new DateTime(1963, 11, 10));



            _context.Doctor.AddRange(d1, d2, d3, d4, d5);
            _context.SaveChanges();
        }
    }
}
