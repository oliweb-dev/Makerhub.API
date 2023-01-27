using MakerHUB.DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MakerHUB.BLL.Services.TokenServices
{
    public class TokenConfig
    {
        public string Signature { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }

    public class TokenService
    {
        private readonly TokenConfig _config;

        public TokenService(TokenConfig config)
        {
            _config = config;
        }

        public string CreateToken(User user)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config.Issuer,
                claims: CreateClaims(user),
                expires: DateTime.Now.AddDays(1),
                signingCredentials: CreateCredentials()
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SigningCredentials CreateCredentials()
        {
            return new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Signature)),
                SecurityAlgorithms.HmacSha512Signature
            );
        }

        private IEnumerable<Claim> CreateClaims(User user)
        {
            //yield return new Claim(ClaimTypes.Gender, user.Gender.ToString());
            yield return new Claim(ClaimTypes.Role, user.Role);
            yield return new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString(), ClaimValueTypes.Integer);
        }
    }
}
