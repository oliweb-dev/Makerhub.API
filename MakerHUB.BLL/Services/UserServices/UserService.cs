using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Services.ActivityServices;
using MakerHUB.BLL.Services.MeetingServices;
using MakerHUB.DAL.Data;
using MakerHUB.DAL.Entities;
using MakerHUB.DAL.Tools;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace MakerHUB.BLL.Services.UserServices
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly ActivityService _activityService;
        private readonly MeetingService _meetingService;

        public UserService(DataContext context, ActivityService activityService, MeetingService meetingService)
        {
            _context = context;
            _activityService = activityService;
            _meetingService = meetingService;
        }

        public void Register(UserRegisterDTO userRegister)
        {
            User user = new User
            {
                Email = userRegister.Email,
                Pseudo = userRegister.Pseudo,
                Firstname = userRegister.Firstname,
                Lastname = userRegister.Lastname,
            };

            if (UserEmailExists(user.Email))
                throw new ValidationException("Cette adresse email exite déjà");

            if (UserPseudoExists(user.Pseudo))
                throw new ValidationException("Ce pseudo existe déjà");

            CreatePasswordHash(userRegister.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmca = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmca.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void ChangePassword(int userId, UserChangePasswordDTO userChangePasswordDTO)
        {
            User? user = _context.Users.FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'existe pas");
            }

            if (!VerifyPasswordHash(userChangePasswordDTO.CurrentPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new PasswordDoesNotMatchExeption("Mot de passe actuel incorrect");
            }

            Utils.CreatePasswordHash(userChangePasswordDTO.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.SaveChanges();
        }

        public bool UserEmailExists(string email)
        {
            if (_context.Users.Any(user => user.Email.ToLower().Equals(email.ToLower())))
            {
                return true;
            }
            return false;
        }

        public bool UserPseudoExists(string pseudo)
        {
            if (_context.Users.Any(user => user.Pseudo.ToLower().Equals(pseudo.ToLower())))
            {
                return true;
            }
            return false;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public UserHomeDTO Home(int userId)
        {
            User? user = _context.Users.Find(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("Cet utilisateur n'a pas été trouvé");
            }

            UserHomeDTO userHomeDTO = new UserHomeDTO
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Pseudo = user.Pseudo,
                Image = user.Image,
            };

            userHomeDTO.Activities = _activityService.GetListByUserId(userId, 4);
            userHomeDTO.Meetings = _meetingService.GetRegisteredMeeting(userId, 4);
            userHomeDTO.NumberOfActivities = _activityService.CountPerUserId(userId);
            userHomeDTO.NumberOfMeetings = _meetingService.CountPerUserId(userId);

            return userHomeDTO;
        }
    }
}
