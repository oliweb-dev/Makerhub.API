using MakerHUB.BLL.DTO;
using MakerHUB.BLL.Services.TokenServices;
using MakerHUB.BLL.Services.UserServices;
using MakerHUB.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace MakerHUB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly TokenService _tokenService;

        public AuthenticationController(UserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginDTO cmd)
        {
            User? user = _userService.GetByEmail(cmd.Email);

            if (user is null)
            {
                return BadRequest("Veuillez vérifier votre email et votre mot de passe pour vous connecter");
            }

            if (!_userService.VerifyPasswordHash(cmd.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Veuillez vérifier votre email et votre mot de passe pour vous connecter");
            }

            return Ok(new { token = _tokenService.CreateToken(user) });
        }

    }
}
