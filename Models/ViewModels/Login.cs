using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class Login {
        [Required(ErrorMessage = "Digite o usuario")]
        [Display(Name = "Usuario")]
        public string User { get; set; }
        [Required(ErrorMessage = "Digite a senha")]
        [Display(Name = "Usuario")]
        public string Password { get; set; }
    }
}
