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

            Person p1 = new Person("FERNANDO FERREIRA FILHO", 62981174693, "FERNANDOFERREIRRAFILHO@GMAIL.COM", "70281834164", "36", "NOSSA SENHORA DA PENHA", "GO", null, "GOIANÉSIA", "486", Status.Ativo, PersonType.Doctor, "FFERRREIRA", "123456798", "GO-9695", "CARDIOLOGIA", new DateTime(2000, 11, 04));
            Person p2 = new Person("JOAO DA SILVA", 9223123372, "JOAOSILVA@GMAIL.COM", "88589476065", "18", "CENTRO", "GO", null, "GOIANÉSIA", "448", Status.Desativado, PersonType.Doctor, "JSILVA", "123456798", "DF-1234", "CLINICO GERAL", new DateTime(2000, 04, 16));
            Person p3 = new Person("ANTONIO DA COSTA SILVA", 9223123372, "ANTONIOCOSTSASILVA@GMAIL.COM", "95743105073", "JOSE CARRILHO", "BOA VISTA", "GO", null, "GOIANÉSIA", "1050", Status.Ativo, PersonType.Doctor, "ACSILVA", "123456798", "MG-1234", "CARDIOLOGISTA", new DateTime(1975, 01, 14));
            Person p4 = new Person("ELISAGELA MARTINS MACHADO", 8221348268, "ELISAGENLAMACHADO@GMAIL.COM", "90066048010", "14", "UNIVERSITARIO", "GO", null, "GOIANÉSIA", "35", Status.Ativo, PersonType.Doctor, "EMMACHADO ", "123456798", "AP-1234", "GINECOLOGISTA", new DateTime(1985, 06, 01));
            Person p5 = new Person("JENNIFER FAIDA", 4724215048, "JENNIFERFAIDA@GMAIL.COM", "68713358030", "01", "BELA VISTA", "GO", null, "GOIANÉSIA", "258", Status.Desativado, PersonType.Doctor, "JFAIDA", "123456798", "SC-1234", "PEDIATRA", new DateTime(1963, 11, 10));
            Person p6 = new Person("JULIA FEAN", 64971342715, "JULIAFEAN@GMAIL.COM", "65549765071", "14", "SETOR UNIVERSITARIO", "GO", "ESQUINA COM A 7", "GOIANÉSIA", "1049", Status.Ativo, PersonType.Assistant, "JFEAN", "123456798", null, null, new DateTime(1999, 07, 30));
            Person p7 = new Person("MARIA LENOE", 97987872420, "MARIALENOE@GMAIL.COM", "40415671060", "08", "SETOR SUL", "GO", null, "GOIANÉSIA", "36", Status.Desativado, PersonType.Assistant, "MLEONE", "123456798", null, null, new DateTime(2002, 12, 12));
            Person p8 = new Person("INSUS COMAN", 99969923714, "INSUSDOMAN@GMAIL.COM", "73947978057", "01", "COVOA", "GO", null, "GOIANÉSIA", "185", Status.Ativo, PersonType.Patient, "ICOMAN", "123456", null, null, new DateTime(1985, 01, 01));
            Person p9 = new Person("SOSPU BEGRAK", 65974442597, "SOPSUBEGRAK@GMAIL.COM", "45218321052", "25", "MORADA NOVA", "GO", null, "GOIANÉSIA", "01", Status.Desativado, PersonType.Patient, "SBEGRAK", "123456", null, null, new DateTime(1986, 03, 14));
            Person p10 = new Person("SNAGSOAUS TYOTU", 47987852834, "SNAGSOAUSTYOTU@GMAIL.COM", "03375819080", "19", "JARDIM DO CERRADO", "GO", null, "GOIANÉSIA", "136", Status.Desativado, PersonType.Patient, "STYOTU", "123456", null, null, new DateTime(1995, 08, 13));

            Procedures pd1 = new Procedures("PULSOTERAPIA", Status.Ativo);
            Procedures pd2 = new Procedures("ESOFAGOSTOMIA", Status.Ativo);
            Procedures pd3 = new Procedures("HEMOGRAMA COMPLETO [INCLUI: CONTAGEM GLOBAL DE LEUCÓCITOS; CONTAGEM GLOBAL DE ERITRÓCITOS; HEMATÓCRITO; HEMOGLOBINA; ÍNDICES HEMATIMÉTRICOS; CONTAGEM GLOBAL DE PLAQUETAS; CONTAGEM DIFERENCIAL DE LEUCÓCITOS (NEUTRÓFILOS, BASTÕES, EOSINÓFILOS, BASÓFILOS, MONÓCITOS, LINFÓCITOS); E EXAME MICROSCÓPICO DE ESFREGAÇO DE SANGUE CORADO]", Status.Desativado);
            Procedures pd4 = new Procedures("CONSULTA MÉDICA", Status.Ativo);
            Procedures pd5 = new Procedures("CARDIOTOCOGRAFIA", Status.Ativo);
            Procedures pd6 = new Procedures("CANDIDA ALBICANS, ANTICORPOS IGG E/ OU IGM E/ OU TOTAIS", Status.Ativo);
            Procedures pd7 = new Procedures("ANTI-ACTINA", Status.Ativo);



            _context.Person.AddRange(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);
            _context.Procedure.AddRange(pd1, pd2, pd3, pd4, pd5, pd6, pd7);
            _context.SaveChanges();
        }
    }
}
