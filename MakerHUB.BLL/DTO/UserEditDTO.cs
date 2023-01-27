using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class UserEditDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 caractères")]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        [MaxLength(100, ErrorMessage = "Max 100 caractères")]
        public string Lastname { get; set; } = string.Empty;
        public string? Image { get; set; }
    }
}
