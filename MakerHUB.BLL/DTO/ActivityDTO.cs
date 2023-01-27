using MakerHUB.DAL.Entities;

namespace MakerHUB.BLL.DTO
{
    public class ActivityDTO
    {
        public int ActivityId { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? UserPseudo { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsLiked { get; set; }
        public int NumberOfLikes { get; set; }
        public bool IsPublic { get; set; }
    }
}
