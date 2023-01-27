using MakerHUB.API.Extensions;
using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Exceptions;
using MakerHUB.BLL.Services.UserServices;
using MakerHUB.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MakerHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            User? user = _userService.GetById(this.GetUserId());

            if (user is null)
            {
                return NotFound("L'utilisateur n'a pas été trouvé");
            }

            UserDTO userDTO = new UserDTO
            {
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Email = user.Email,
                Pseudo = user.Pseudo,
                Image = user.Image,
                DateCreated = user.DateCreated,
            };

            return Ok(userDTO);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Modify(UserEditDTO userEditDTO)
        {
            User? user = _userService.GetById(this.GetUserId());
            if (user == null)
            {
                return NotFound("L'enregistrement n'a pas été trouvé");
            }

            user.Lastname = userEditDTO.Lastname;
            user.Firstname = userEditDTO.Firstname;
            user.Image = userEditDTO.Image;

            try
            {
                _userService.Update(user);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("home")]
        [Authorize]
        public IActionResult HomePage()
        {
            UserHomeDTO userHomeDTO = _userService.Home(this.GetUserId());

            return Ok(userHomeDTO);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDTO userRegister)
        {
            try
            {
                _userService.Register(userRegister);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public IActionResult ChangePassword([FromBody] UserChangePasswordDTO userChangePasswordDTO)
        {
            if (userChangePasswordDTO.NewPassword != userChangePasswordDTO.NewPasswordConfirm)
            {
                return BadRequest("Le mot de passe de confirmation ne correspond pas");
            }

            try
            {
                _userService.ChangePassword(this.GetUserId(), userChangePasswordDTO);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (PasswordDoesNotMatchExeption ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
