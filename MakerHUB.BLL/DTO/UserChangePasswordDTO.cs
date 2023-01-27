using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class UserChangePasswordDTO
    {
        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        public string NewPasswordConfirm { get; set; } = string.Empty;
    }
}
