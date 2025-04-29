using backend.Core.Models;
using Microsoft.Extensions.Primitives;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace backend.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;
        private readonly AuthOptions _authOptions;

        public AuthService(IUserRepository userRepository, TokenService tokenService, AuthOptions authOptions)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _authOptions = authOptions;
        }

        public async Task<UserData> Registration(AuthDto authDto, StringValues userAgentData)
        {
            (User? user, _) = await _userRepository.Get(authDto.Email);
            if (user is not null)
            {
                throw new Exception("Failed to register, user with this email already exists!");
            }

            var hashPassword = Utilities.Hash(authDto.Password);

            authDto.Password = hashPassword;

            user = await _userRepository.Create(authDto);

            var tokens = _tokenService.GenerateTokens(user);

            var agentData = Utilities.GetUserAgentData(userAgentData);

            (tokens.Expired, tokens.LifeTime) = await _tokenService.SaveRefreshTokenAsync(user.Id, tokens.RefreshJwt, agentData);

            var userData = new UserData() { User = user, TokensData = tokens };

            return userData;

        }

        public async Task<UserData> Login(AuthDto authDto, StringValues userAgentData)
        {
            (User? user, string? password) = await _userRepository.Get(authDto.Email);

            if (user is null)
            {
                throw new Exception("User with such email is not found!");
            }

            var hashPassword = Utilities.Hash(authDto.Password);

            if (!password!.Equals(Utilities.Hash(authDto.Password)))
            {
                throw new Exception("Invalid password!");
            }

            var tokens = _tokenService.GenerateTokens(user);

            var agentData = Utilities.GetUserAgentData(userAgentData);

            (tokens.Expired, tokens.LifeTime) = await _tokenService.SaveRefreshTokenAsync(user.Id, tokens.RefreshJwt, agentData);

            var userData = new UserData() { User = user, TokensData = tokens };

            return userData;

        }

        public async Task<string> Logout(string refreshToken)
        {
            return await _tokenService.Delete(refreshToken);
        }

        public async Task<UserData> Refresh(TokensData tokensClient, StringValues userAgentData)
        {
            if (!await ValidateAccessToken(tokensClient.AccessJwt))
            {
                throw new Exception("Invalid access token!");
            }

            if (!await ValidateRefreshToken(tokensClient.RefreshJwt))
            {
                throw new Exception("Invalid refresh token!");
            }

            var agentData = Utilities.GetUserAgentData(userAgentData);

            var user = await _tokenService.GetUser(tokensClient.RefreshJwt, agentData);

            if (user is null)
            {
                throw new Exception("In database refresh token is not found!");
            }

            var tokens = _tokenService.GenerateTokens(user);

            (tokens.Expired, tokens.LifeTime) = await _tokenService.SaveRefreshTokenAsync(user.Id, tokens.RefreshJwt, agentData);

            var userData = new UserData() { User = user, TokensData = tokens };

            return userData;
        }

        private async Task<bool> ValidateAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claimsPrincipal = tokenHandler.ValidateToken(accessToken, _authOptions.GetTokenValidationParameters(false), out var _);

            var claims = claimsPrincipal.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier));
            if (userIdClaim is null)
            {
                return false;
            }

            var userEmailClaim = claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email));
            if (userEmailClaim is null)
            {
                return false;
            }

            var userExist = await _userRepository.Exists(new Guid(userIdClaim.Value), userEmailClaim.Value);

            if (!userExist)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateRefreshToken(string refreshToken)
        {
            (string? token, DateTime Expired) = await _tokenService.Get(refreshToken);

            if (token is null)
            {
                return false;
            }

            if (DateTime.UtcNow >= Expired)
            {
                return false;
            }

            return true;
        }
    }
}
