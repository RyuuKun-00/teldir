
using backend.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace backend.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAuthOptions _authOptions;

        public JwtService(IAuthOptions authOptions)
        {
            _authOptions = authOptions;
        }

        public string Generate()
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, "Ryuu") };

            var credentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature);

            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(
                    issuer: _authOptions.ISSUER,
                    audience: _authOptions.AUDIENCE,
                    claims: claims,
                    notBefore: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                    expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2))
                );

            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
