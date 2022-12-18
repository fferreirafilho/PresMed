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

            ClinicOpening c1 = new ClinicOpening(new DateTime(2022, 09, 10, 07, 00, 00), new DateTime(2022, 09, 10, 23, 00, 00), "36", "Nova aurora", "Goiás", "", "Goianésia", "486", "Tomar {DOSAGEM} de {MEDICAMENTO} a cada {HORA}, observação: {OBSERVACAO}", "Atesto para os devidos fins, a pedido, que o(a) Sr(a). {NOMEDOPACIENTE}, inscrito(a) no CPF sob o nº {CPFDOPACIENTE}, paciente sob meus cuidados, foi atendido(a) no dia {DATAATUAL} às {HORAATUAL}, CID {CID} necessitando de {DIAAFASTAMENTO dias de repouso.");

            _context.ClinicOpening.Add(c1);
            _context.SaveChanges();
        }
    }
}
