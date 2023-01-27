namespace MakerHUB.DAL.Entities
{
    public class MeetingLiked
    {
        public int MeetingLikedId { get; set; }
        public Meeting Meeting { get; set; }
        public int MeetingId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
