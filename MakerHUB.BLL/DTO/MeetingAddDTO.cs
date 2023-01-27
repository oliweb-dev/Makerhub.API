using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class MeetingAddDTO
    {
        public DateTime Date { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le titre est obligatoire")]
        [MinLength(2, ErrorMessage = "Le titre doit comporter au minimum 2 caractères")]
        [MaxLength(100, ErrorMessage = "Le titre doit comporter au maximum 100 caractères")]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nombre minimum de participants est obligatoire")]
        [Range(1, 200, ErrorMessage = "Le nombre minimum de participants doit être compris entre 1 et 200")]
        public int MinParticipant { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Le nombre maximum de participants est obligatoire")]
        [Range(1, 200, ErrorMessage = "Le nombre maximum de participants doit être compris entre 1 et 200")]
        public int MaxParticipant { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool DisplayListParticipants { get; set; }
    }
}
