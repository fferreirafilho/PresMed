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


            Person p1 = new Person("FERNANDO FERREIRA FILHO", 62981174693, "FERNANDOFERREIRRAFILHO@GMAIL.COM", "70281834164", "36", "NOSSA SENHORA DA PENHA", "GO", null, "GOIANÉSIA", "486", Status.Ativo, PersonType.Doctor, "FFERREIRA", "7c4a8d09ca3762af61e59520943dc26494f8941b", "GO-9695", "CARDIOLOGIA", new DateTime(2000, 11, 04));
            Person p2 = new Person("JOAO DA SILVA", 9223123372, "JOAOSILVA@GMAIL.COM", "88589476065", "18", "CENTRO", "GO", null, "GOIANÉSIA", "448", Status.Desativado, PersonType.Doctor, "JSILVA", "7c4a8d09ca3762af61e59520943dc26494f8941b", "DF -1234", "CLINICO GERAL", new DateTime(2000, 04, 16));
            Person p3 = new Person("ANTONIO DA COSTA SILVA", 9223123372, "ANTONIOCOSTSASILVA@GMAIL.COM", "95743105073", "JOSE CARRILHO", "BOA VISTA", "GO", null, "GOIANÉSIA", "1050", Status.Ativo, PersonType.Doctor, "ACSILVA", "7c4a8d09ca3762af61e59520943dc26494f8941b", "MG-1234", "CARDIOLOGISTA", new DateTime(1975, 01, 14));
            Person p4 = new Person("ELISAGELA MARTINS MACHADO", 8221348268, "ELISAGENLAMACHADO@GMAIL.COM", "90066048010", "14", "UNIVERSITARIO", "GO", null, "GOIANÉSIA", "35", Status.Ativo, PersonType.Doctor, "EMMACHADO ", "7c4a8d09ca3762af61e59520943dc26494f8941b", "AP-1234", "GINECOLOGISTA", new DateTime(1985, 06, 01));
            Person p5 = new Person("JENNIFER FAIDA", 4724215048, "JENNIFERFAIDA@GMAIL.COM", "68713358030", "01", "BELA VISTA", "GO", null, "GOIANÉSIA", "258", Status.Desativado, PersonType.Doctor, "JFAIDA", "7c4a8d09ca3762af61e59520943dc26494f8941b", "SC-1234", "PEDIATRA", new DateTime(1963, 11, 10));
            Person p6 = new Person("JULIA FEAN", 64971342715, "JULIAFEAN@GMAIL.COM", "65549765071", "14", "SETOR UNIVERSITARIO", "GO", "ESQUINA COM A 7", "GOIANÉSIA", "1049", Status.Ativo, PersonType.Assistant, "JFEAN", "7c4a8d09ca3762af61e59520943dc26494f8941b", null, null, new DateTime(1999, 07, 30));
            Person p7 = new Person("MARIA LENOE", 97987872420, "MARIALENOE@GMAIL.COM", "40415671060", "08", "SETOR SUL", "GO", null, "GOIANÉSIA", "36", Status.Desativado, PersonType.Assistant, "MLEONE", "7c4a8d09ca3762af61e59520943dc26494f8941b", null, null, new DateTime(2002, 12, 12));
            Person p8 = new Person("INSUS COMAN", 99969923714, "INSUSDOMAN@GMAIL.COM", "73947978057", "01", "COVOA", "GO", null, "GOIANÉSIA", "185", Status.Ativo, PersonType.Patient, "ICOMAN", "7c4a8d09ca3762af61e59520943dc26494f8941b", null, null, new DateTime(1985, 01, 01));
            Person p9 = new Person("SOSPU BEGRAK", 65974442597, "SOPSUBEGRAK@GMAIL.COM", "45218321052", "25", "MORADA NOVA", "GO", null, "GOIANÉSIA", "01", Status.Desativado, PersonType.Patient, "SBEGRAK", "7c4a8d09ca3762af61e59520943dc26494f8941b", null, null, new DateTime(1986, 03, 14));
            Person p10 = new Person("SNAGSOAUS TYOTU", 47987852834, "SNAGSOAUSTYOTU@GMAIL.COM", "03375819080", "19", "JARDIM DO CERRADO", "GO", null, "GOIANÉSIA", "136", Status.Desativado, PersonType.Patient, "STYOTU", "7c4a8d09ca3762af61e59520943dc26494f8941b", null, null, new DateTime(1995, 08, 13));


            Time t1 = new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), p1, new DateTime(2022, 01, 01, 00, 30, 00), 20);
            Time t2 = new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), p2, new DateTime(2022, 01, 01, 00, 30, 00), 20);
            Time t3 = new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), p3, new DateTime(2022, 01, 01, 00, 30, 00), 20);
            Time t4 = new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), p4, new DateTime(2022, 01, 01, 00, 30, 00), 20);
            Time t5 = new Time(new DateTime(2022, 01, 01, 08, 00, 00), new DateTime(2022, 01, 01, 18, 00, 00), p5, new DateTime(2022, 01, 01, 00, 30, 00), 20);

            _context.Person.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            _context.Time.AddRange(t1, t2, t3, t4, t5);
            _context.SaveChanges();
        }
    }
}
