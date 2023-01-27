using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class UserDTO
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
