using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class ActivityEditDTO
    {
        [Required]
        public int ActivityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La date est obligatoire")]
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le titre est obligatoire")]
        [MinLength(2, ErrorMessage = "Le titre doit comporter au minimum 2 caractères")]
        [MaxLength(100, ErrorMessage = "Le titre doit comporter au maximum 100 caractères")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsPublic { get; set; }
    }
}
