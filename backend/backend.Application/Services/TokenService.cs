using backend.Core.Models;
using backend.DataAccess.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace backend.Application.Services
{
    public class TokenService
    {
        private readonly AuthOptions _authOptions;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(AuthOptions authOptions, ITokenRepository tokenRepository)
        {
            _authOptions = authOptions;
            _tokenRepository = tokenRepository;
        }

        public async Task<(DateTime,int)> SaveRefreshTokenAsync(
            Guid userId,
            string refreshToken,
            UserAgentData userAgentData)
        {
            return await _tokenRepository.SaveRefreshTokenAsync(userId, refreshToken, userAgentData,_authOptions.RefreshTokenLifetimeMinute);

        }

        public async Task<string> Delete(string refreshToken)
        {
            return await _tokenRepository.Delete(refreshToken);
        }

        public TokensData GenerateTokens(User user)
        {

            var refreshToken = GenerateRefreshToken();

            var accessJwt = GenerateAccessToken(user);

            return new TokensData { AccessJwt = accessJwt, RefreshJwt = refreshToken };
        }

        private string GenerateAccessToken(User user)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_authOptions.AccessTokenLifetimeMinute);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email)
            };

            var jwt = new JwtSecurityToken(_authOptions.Issuer,
                _authOptions.Audience,
                claims,
                expires: expires,
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public async Task<(string?,DateTime)> Get(string refreshToken)
        {
            return await _tokenRepository.Get(refreshToken);
        }

        public async Task<User?> GetUser(string refreshToken, UserAgentData userAgentData)
        {
            return await _tokenRepository.GetUser(refreshToken, userAgentData);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

    }
}
