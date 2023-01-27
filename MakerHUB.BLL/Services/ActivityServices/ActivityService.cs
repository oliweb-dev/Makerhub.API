using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Mappers;
using MakerHUB.DAL.Data;
using MakerHUB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.Services.ActivityServices
{
    public class ActivityService
    {
        private readonly DataContext _context;

        public ActivityService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ActivityDTO> GetListByUserId(int userId, int limit = 9999)
        {
            return _context.Activities.Include(a => a.User)
                                      .Include(al => al.ActivityLikeds)
                                      .Where(a => a.UserId == userId)
                                      .OrderByDescending(a => a.Date)
                                      .Take(limit)
                                      .Select(a => a.ToActivityDTO(userId));
        }

        public IEnumerable<ActivityDTO> GetListPublic(int userId, int limit = 9999)
        {
            return _context.Activities.Include(a => a.User)
                                      .Include(al => al.ActivityLikeds)
                                      .Where(a => a.IsPublic == true || a.UserId == userId)
                                      .OrderByDescending(a => a.Date)
                                      .Take(limit)
                                      .Select(a => a.ToActivityDTO(userId));
        }

        public Activity? GetByActivityIdAndUserId(int activityId, int userId)
        {
            return _context.Activities.SingleOrDefault(a => a.ActivityId == activityId && a.UserId == userId);
        }

        public int Create(ActivityAddDTO activityAddDTO, int userId)
        {
            Activity activity = ActivityMapper.ToDatabaseAddDTO(activityAddDTO, userId);
            _context.Activities.Add(activity);
            _context.SaveChanges();

            return activity.ActivityId;
        }

        public ActivityDTO? GetById(int id, int userId)
        {
            ActivityDTO? activityDTO = _context.Activities.Include(a => a.User)
                                                         .Include(a => a.ActivityLikeds)
                                                         .ThenInclude(la => la.User)
                                                         .Where(a => a.ActivityId == id)
                                                         .Select(a => a.ToActivityDTO(userId))
                                                         .FirstOrDefault();
            if (activityDTO == null)
            {
                throw new KeyNotFoundException("L'activité n'a pas été trouvée");
            }

            return activityDTO;
        }

        public void Delete(int id, int userId)
        {
            Activity? activity = _context.Activities.FirstOrDefault(a => a.ActivityId == id);

            if (activity == null)
            {
                throw new KeyNotFoundException("Cette action n'a pas été trouvée");
            }

            if (activity.UserId != userId)
            {
                throw new ForbiddenException("Vous ne pouvez pas supprimer cette action");
            }

            _context.Activities.Remove(activity);
            _context.SaveChanges();
        }

        public void Update(ActivityEditDTO activityEditDTO, int userId)
        {
            Activity? activity = _context.Activities.FirstOrDefault(a => a.ActivityId == activityEditDTO.ActivityId);

            if (activity == null)
            {
                throw new KeyNotFoundException("Cette activité n'a pas été trouvée");
            }

            if (activity.UserId != userId)
            {
                throw new ForbiddenException("Vous ne pouvez pas modifier cette activité");
            }

            activity = activityEditDTO.ToDatabaseEditDTO(activity);

            _context.Activities.Update(activity);
            _context.SaveChanges();

        }

        public void Like(int activityId, int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas");
            }

            Activity? activity = _context.Activities.Find(activityId);

            if (activity == null)
            {
                throw new KeyNotFoundException("Cette activité n'existe pas");
            }

            if (_context.ActivityLikeds.Any(al => al.ActivityId == activityId && al.UserId == userId))
            {
                throw new AlreadyExistException("Vous aimez déjà cette action");
            }

            ActivityLiked activityLiked = new ActivityLiked
            {
                ActivityId = activityId,
                UserId = userId,
            };

            _context.ActivityLikeds.Add(activityLiked);
            _context.SaveChanges();
        }

        public void Dislike(int activityId, int userId)
        {
            ActivityLiked? activityLiked = _context.ActivityLikeds.FirstOrDefault(al => al.ActivityId == activityId && al.UserId == userId);

            if (activityLiked == null)
            {
                throw new KeyNotFoundException("Ce like n'a pas été trouvé");
            }

            _context.ActivityLikeds.Remove(activityLiked);
            _context.SaveChanges();
        }

        public int CountPerUserId(int userId)
        {
            return _context.Activities.Count(a => a.UserId == userId);
        }
    }
}
