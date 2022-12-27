using PresMed.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Cid {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(3, ErrorMessage = "Campo inválido")]
        [MaxLength(5, ErrorMessage = "Campo inválido")]
        [Display(Name = "Codigo")]
        public string Cod { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(1, ErrorMessage = "Campo inválido")]
        [MaxLength(264, ErrorMessage = "Campo inválido")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }


        public Cid() { }

        public Cid(string cod, string description) {
            Cod = cod;
            Description = description;

        }
    }
}
