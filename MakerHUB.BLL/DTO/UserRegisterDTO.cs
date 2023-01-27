using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class UserRegisterDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Le prénom est obligatoire")]
        [MinLength(1, ErrorMessage = "Le prénom doit comporter au minimum 1 caractère")]
        [MaxLength(100, ErrorMessage = "Le prénom doit comporter au maximum 100 caractères")]
        public string Firstname { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nom de famille est obligatoire")]
        [MinLength(1, ErrorMessage = "Le nom de famille doit comporter au minimum 1 caractère")]
        [MaxLength(100, ErrorMessage = "Le nom de famille doit comporter au maximum 100 caractères")]
        public string Lastname { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Le format de l'email est incorrect")]
        [MaxLength(120, ErrorMessage = "L'email doit comporter au maximum 120 caractères")]
        public string Email { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le pseudo est obligatoire")]
        [MinLength(2, ErrorMessage = "Le pseudo doit comporter au minimum 2 caractère")]
        [MaxLength(50, ErrorMessage = "Le pseudo doit comporter au maximum 50 caractères")]
        public string Pseudo { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe est obligatoire")]
        [MinLength(4, ErrorMessage = "Le mot de passe doit comporter au minimum 8 caractère")]
        [MaxLength(100, ErrorMessage = "Le mot de passe doit comporter au maximum 100 caractères")]
        public string Password { get; set; } = string.Empty;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le mot de passe de confirmation est obligatoire")]
        [Compare("Password", ErrorMessage = "Votre mot de passe de confirmation ne correspond pas")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
