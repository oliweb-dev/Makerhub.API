namespace MakerHUB.DAL.Entities
{
    public class MeetingParticipant
    {
        public int MeetingParticipantId { get; set; }
        public Meeting Meeting { get; set; }
        public int MeetingId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}
