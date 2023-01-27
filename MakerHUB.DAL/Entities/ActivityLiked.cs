namespace MakerHUB.DAL.Entities
{
    public class ActivityLiked
    {
        public int ActivityLikedId { get; set; }
        public Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
