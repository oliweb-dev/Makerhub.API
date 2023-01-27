using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.DTO
{
    public class MeetingDTO
    {
        public int MeetingId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? UserPseudo { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public int MinParticipant { get; set; }
        public int MaxParticipant { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsRegistered { get; set; }
        public bool IsLiked { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfParticipants { get; set; }
        public bool DisplayListParticipants { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
