using MakerHUB.BLL.DTO;
using MakerHUB.DAL.Entities;

namespace MakerHUB.BLL.Mappers
{
    public static class ActivityMapper
    {

        public static ActivityDTO ToActivityDTO(this Activity a, int userId)
        {
            return new ActivityDTO()
            {
                ActivityId = a.ActivityId,
                Date = a.Date,
                Title = a.Title,
                Description = a.Description,
                Image = a.Image,
                UserPseudo = a.User?.Pseudo,
                UserId = a.User?.UserId,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                NumberOfLikes = a.ActivityLikeds.Count,
                IsLiked = a.ActivityLikeds.Any(al => al.ActivityId == a.ActivityId && al.UserId == userId),
                IsPublic = a.IsPublic
            };
        }

        public static Activity ToDatabaseAddDTO(this ActivityAddDTO a, int userId)
        {
            return new Activity()
            {
                Date = a.Date,
                Title = a.Title,
                Description = a.Description,
                Image = a.Image,
                UserId = userId,
                Latitude = a.Latitude,
                Longitude = a.Longitude,
                IsPublic = a.IsPublic
            };
        }

        public static Activity ToDatabaseEditDTO(this ActivityEditDTO a, Activity activity)
        {
            activity.Date = a.Date;
            activity.Title = a.Title;
            activity.Description = a.Description;
            activity.Image = a.Image;
            activity.Latitude = a.Latitude;
            activity.Longitude = a.Longitude;
            activity.IsPublic = a.IsPublic;
            activity.DateModified = DateTime.Now;

            return activity;
        }
    }
}
