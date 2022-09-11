using PresMed.Models;
using System;
using System.Linq;


namespace PresMed.Data {
    public class SeedingClinicOpening {

        private readonly BancoContext _context;

        public SeedingClinicOpening(BancoContext bancoContext) {
            _context = bancoContext;
        }

        public void Seed() {
            if (_context.ClinicOpening.Any()) {
                return;
            }

            ClinicOpening c1 = new ClinicOpening(new DateTime(2022, 09, 10, 07, 00, 00), new DateTime(2022, 09, 10, 18, 00, 00));

            _context.ClinicOpening.Add(c1);
            _context.SaveChanges();
        }
    }
}
