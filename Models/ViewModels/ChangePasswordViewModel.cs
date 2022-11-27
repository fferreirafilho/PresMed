using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class ChangePasswordViewModel {

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo não pode ser vazio")]
        [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")]
        public string ConfirmPassword { get; set; }

    }
}
