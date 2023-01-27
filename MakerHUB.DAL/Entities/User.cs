using System.ComponentModel.DataAnnotations;

namespace MakerHUB.DAL.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [MaxLength(100)]
        public string Firstname { get; set; }

        [MaxLength(100)]
        public string Lastname { get; set; }

        [MaxLength(120)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Pseudo { get; set; }

        [MaxLength(60)]
        public string Image { get; set; }

        public byte[] PasswordHash { get; set; } // = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; }

        [MaxLength(10)]
        public string Role { get; set; } = "USER";
        public ICollection<ActivityLiked> LikeActivities { get; set; }
        public ICollection<MeetingParticipant> MeetingParticipants { get; set; }
        public ICollection<MeetingLiked> MeetingLikeds { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; } = DateTime.Now;
    }
}
