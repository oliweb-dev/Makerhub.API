namespace MakerHUB.BLL.DTO
{
    public class UserHomeDTO
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Pseudo { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int NumberOfActivities { get; set; } = 0;
        public int NumberOfMeetings { get; set; } = 0;
        public IEnumerable<ActivityDTO>? Activities { get; set; }
        public IEnumerable<MeetingDTO>? Meetings { get; set; }
    }
}
