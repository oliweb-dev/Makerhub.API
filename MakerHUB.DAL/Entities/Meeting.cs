using System.ComponentModel.DataAnnotations;

namespace MakerHUB.DAL.Entities
{
    public class Meeting
    {
        public int MeetingId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(60)]
        public string Image { get; set; }
        public int MinParticipant { get; set; }
        public int MaxParticipant { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public User? User { get; set; }
        public int? UserId { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
        public ICollection<MeetingLiked> MeetingLikeds { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
