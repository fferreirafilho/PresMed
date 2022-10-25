using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Medicine {


        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Nome")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Medicamento")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string Drug { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Registro")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string Record { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Concentração")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string Concentration { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Forma farmacêutica")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string PharmaceuticalForm { get; set; }
        public ICollection<Prescription> AttendanceMedicines { get; set; }
        public Medicine() { }

        public Medicine(string name, string drug, string record, string concentration, string pharmaceuticalForm) {
            Name = name;
            Drug = drug;
            Record = record;
            Concentration = concentration;
            PharmaceuticalForm = pharmaceuticalForm;
        }
    }
}
