using MakerHUB.BLL.DTO;
using MakerHUB.DAL.Entities;

namespace MakerHUB.BLL.Mappers
{
    public static class MeetingMapper
    {
        public static MeetingDTO ToMeetingDTO(this Meeting m, int userId)
        {

            return new MeetingDTO()
            {
                MeetingId = m.MeetingId,
                Date = m.Date,
                Title = m.Title,
                Description = m.Description,
                Image = m.Image,
                UserPseudo = m.User?.Pseudo,
                UserId = m.User?.UserId,
                MinParticipant = m.MinParticipant,
                MaxParticipant = m.MaxParticipant,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                IsRegistered = m.MeetingParticipants.Any(mp => mp.MeetingId == m.MeetingId && mp.UserId == userId),
                IsLiked = m.MeetingLikeds.Any(ml => ml.MeetingId == m.MeetingId && ml.UserId == userId),
                NumberOfLikes = m.MeetingLikeds.Count,
                NumberOfParticipants = m.MeetingLikeds.Count,
                DateCreated = m.DateCreated
            };
        }

        public static Meeting ToDatabaseAddDTO(this MeetingAddDTO m, int userId)
        {
            return new Meeting()
            {
                Date = m.Date,
                Title = m.Title,
                Description = m.Description,
                Image = m.Image,
                MinParticipant = m.MinParticipant,
                MaxParticipant = m.MaxParticipant,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                UserId = userId,
            };
        }

        public static Meeting ToDatabaseEditDTO(this MeetingEditDTO m, Meeting meeting)
        {
            meeting.Date = m.Date;
            meeting.Title = m.Title;
            meeting.Description = m.Description;
            meeting.Image = m.Image;
            meeting.MinParticipant = m.MinParticipant;
            meeting.MaxParticipant = m.MaxParticipant;
            meeting.Latitude = m.Latitude;
            meeting.Longitude = m.Longitude;
            meeting.DateModified = DateTime.Now;

            return meeting;
        }
    }
}
