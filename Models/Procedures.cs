using PresMed.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Procedures {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(400, ErrorMessage = "Campo invalido")]
        [Display(Name = "Procedimento")]
        public string Name { get; set; }
        public Status Status { get; set; }

        public Procedures(string name, Status status) {
            Name = name;
            Status = status;
        }
    }
}
