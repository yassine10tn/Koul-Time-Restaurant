using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mot de passe obligatoire")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nom obligatoire")]
        [Display(Name = "Nom complet")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email obligatoire")]
        [EmailAddress(ErrorMessage = "Email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mot de passe obligatoire")]
        [MinLength(6, ErrorMessage = "Minimum 6 caractères")]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirmation obligatoire")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}