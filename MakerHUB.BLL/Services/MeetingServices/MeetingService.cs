using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Mappers;
using MakerHUB.DAL.Data;
using MakerHUB.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MakerHUB.BLL.Services.MeetingServices
{
    public class MeetingService
    {
        private readonly DataContext _context;

        public MeetingService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<MeetingDTO> GetAll(int userId, int limit = 9999)
        {


            return _context.Meetings.Include(m => m.MeetingParticipants)
                                    .Include(m => m.MeetingLikeds)
                                    .Include(m => m.User)
                                    .OrderByDescending(e => e.Date)
                                    .Take(limit)
                                    .Select(m => m.ToMeetingDTO(userId));
        }

        public IEnumerable<MeetingDTO> GetUpcomingMeetings(int userId, int limit = 9999)
        {
            return _context.Meetings.Include(m => m.MeetingParticipants)
                                    .Include(m => m.MeetingLikeds)
                                    .Include(m => m.User)
                                    .Where(m => m.Date >= DateTime.Now.AddDays(-1))
                                    .OrderByDescending(e => e.Date)
                                    .Take(limit)
                                    .Select(m => m.ToMeetingDTO(userId));
        }

        public IEnumerable<MeetingDTO> GetRegisteredMeeting(int userId, int limit = 9999)
        {
            return _context.Meetings.Include(m => m.MeetingParticipants)
                                    .Include(m => m.MeetingLikeds)
                                    .Include(m => m.User)
                                    .Where(m => m.Date >= DateTime.Now.AddDays(-1) && m.MeetingParticipants.Any(mp => mp.UserId == userId))
                                    .OrderByDescending(e => e.Date)
                                    .Take(limit)
                                    .Select(m => m.ToMeetingDTO(userId));
        }

        public MeetingDTO GetById(int meetingId, int userId)
        {
            MeetingDTO? meetingDTO = _context.Meetings.Include(m => m.MeetingParticipants)
                                    .Include(m => m.MeetingLikeds)
                                    .Include(m => m.User)
                                    .Where(m => m.MeetingId == meetingId)
                                    .Select(m => m.ToMeetingDTO(userId))
                                    .FirstOrDefault();
            if (meetingDTO == null)
            {
                throw new KeyNotFoundException("L'événement n'a pas été trouvé");
            }

            return meetingDTO;
        }

        public int Create(MeetingAddDTO meetingAddDTO, int userId)
        {
            Meeting meeting = MeetingMapper.ToDatabaseAddDTO(meetingAddDTO, userId);
            _context.Meetings.Add(meeting);
            _context.SaveChanges();

            return meeting.MeetingId;
        }

        public void Update(MeetingEditDTO m, int userId)
        {
            Meeting? meeting = _context.Meetings.FirstOrDefault(x => x.MeetingId == m.MeetingId);

            if (m.MinParticipant > m.MaxParticipant)
            {
                throw new ValidationException("Il y a une incohérence dans le nombre de participants");
            }

            if (meeting == null)
            {
                throw new KeyNotFoundException("Cet événement n'a pas été trouvé");
            }

            if (meeting.UserId != userId)
            {
                throw new ForbiddenException("Vous ne pouvez pas modifier cet événement");
            }

            meeting = m.ToDatabaseEditDTO(meeting);

            _context.Meetings.Update(meeting);
            _context.SaveChanges();
        }

        public void Delete(int id, int userId)
        {
            Meeting? meeting = _context.Meetings.FirstOrDefault(m => m.MeetingId == id);

            if (meeting == null)
            {
                throw new KeyNotFoundException("Cet événement n'a pas été trouvé");
            }

            if (meeting.UserId != userId)
            {
                throw new ForbiddenException("Vous ne pouvez pas supprimer cet événement");
            }

            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
        }

        public void Subscribe(int meetingId, int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas");
            }

            Meeting? meeting = _context.Meetings.Find(meetingId);

            if (meeting == null)
            {
                throw new KeyNotFoundException("Cet événement n'existe pas");
            }

            if (meeting.Date < DateTime.Now)
            {
                throw new ValidationException("Vous ne pouvez plus vous inscrire à cet événement");
            }

            if (_context.MeetingParticipants.Any(mp => mp.MeetingId == meetingId && mp.UserId == userId))
            {
                throw new AlreadyExistException("Vous êtes déjà inscrit à cet événement");
            }

            MeetingParticipant meetingParticipant = new MeetingParticipant
            {
                MeetingId = meetingId,
                UserId = userId,
            };

            _context.MeetingParticipants.Add(meetingParticipant);
            _context.SaveChanges();
        }

        public void Unsubscribe(int meetingId, int userId)
        {
            MeetingParticipant? meetingParticipant = _context.MeetingParticipants
                                    .Include(mp => mp.Meeting)
                                    .FirstOrDefault(mp => mp.MeetingId == meetingId && mp.UserId == userId);

            if (meetingParticipant == null)
            {
                throw new KeyNotFoundException("Cette inscription n'a pas été trouvée");
            }

            if (meetingParticipant.Meeting.Date < DateTime.Now)
            {
                throw new ValidationException("Vous ne pouvez plus vous désinscrire de cet événement");
            }

            _context.MeetingParticipants.Remove(meetingParticipant);
            _context.SaveChanges();
        }

        public void Like(int meetingId, int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas");
            }

            Meeting? meeting = _context.Meetings.Find(meetingId);

            if (meeting == null)
            {
                throw new KeyNotFoundException("Cet événement n'existe pas");
            }

            if (_context.MeetingLikeds.Any(mp => mp.MeetingId == meetingId && mp.UserId == userId))
            {
                throw new AlreadyExistException("Vous aimez déjà cet événement");
            }

            MeetingLiked meetingLiked = new MeetingLiked
            {
                MeetingId = meetingId,
                UserId = userId,
            };

            _context.MeetingLikeds.Add(meetingLiked);
            _context.SaveChanges();
        }
        public void Dislike(int meetingId, int userId)
        {
            MeetingLiked? meetingLiked = _context.MeetingLikeds.FirstOrDefault(ml => ml.MeetingId == meetingId && ml.UserId == userId);

            if (meetingLiked == null)
            {
                throw new KeyNotFoundException("Ce like n'a pas été trouvé");
            }

            _context.MeetingLikeds.Remove(meetingLiked);
            _context.SaveChanges();
        }

        public int CountPerUserId(int userId)
        {
            return _context.Meetings.Count(m => m.Date >= DateTime.Now.AddDays(-1) && m.MeetingParticipants.Any(mp => mp.UserId == userId));
        }
    }
}

