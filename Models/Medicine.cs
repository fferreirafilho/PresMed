using System.ComponentModel.DataAnnotations;

namespace PresMed.Models {
    public class Medicine {


        public int Id { get; set; }
        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [Display(Name = "Nome")]
        [MinLength(5, ErrorMessage = "Campo invalido")]
        [MaxLength(50, ErrorMessage = "Campo invalido")]
        public string Name { get; set; }
    }
}
