using backend.Application.Services;
using backend.Contracts;
using backend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController:Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        private readonly string refreshTokenKey;

        public AuthController(IAuthService authService,IConfiguration configuration,ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
            refreshTokenKey = configuration.GetSection("CookiesNames").GetChildren().First(c => c.Key.Equals("RefreshToken")).Value;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<AuthResponce>> Registration([FromBody] AuthRequest request)
        {
            try
            {
                var authDto = new AuthDto() { Email = request.Email, Password = request.Password };

                var userAgentData = Request.Headers["User-Agent"];

                var userData = await _authService.Registration(authDto, userAgentData);
                _logger.LogInformation($"Email: {userData.User.Email}\n" +
                    $"Access: {userData.TokensData.AccessJwt}\n" +
                    $"Refresh: {userData.TokensData.RefreshJwt}\n" +
                    $"Expired: {userData.TokensData.Expired}\n" +
                    $"LifeTime: {userData.TokensData.LifeTime}");

                AddRefreshTokenCookie(userData);

                return Ok(new AuthResponce(userData.User.Id,userData.User.Email,userData.TokensData.AccessJwt));

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponce>> Login([FromBody] AuthRequest request)
        {
            try
            {
                var authDto = new AuthDto() { Email = request.Email, Password = request.Password };

                var userAgentData = Request.Headers["User-Agent"];

                var userData = await _authService.Login(authDto, userAgentData);

                _logger.LogInformation($"Email: {userData.User.Email}\n" +
                    $"Access: {userData.TokensData.AccessJwt}\n" +
                    $"Refresh: {userData.TokensData.RefreshJwt}\n" +
                    $"Expired: {userData.TokensData.Expired}\n" +
                    $"LifeTime: {userData.TokensData.LifeTime}");

                AddRefreshTokenCookie(userData);

                return Ok(new AuthResponce(userData.User.Id, userData.User.Email, userData.TokensData.AccessJwt));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("logout")]
        public async Task<ActionResult<string>> Logout()
        {
            try
            {
                var cookie = Request.Cookies.TryGetValue(refreshTokenKey, out var refreshToken);

                if (!cookie)
                {
                    throw new Exception("Refresh token cookie is not found!");
                }

                await _authService.Logout(refreshToken!);

                Response.Cookies.Delete(refreshTokenKey);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("refresh")]
        public async Task<ActionResult<AuthResponce>> Refresh([FromBody] string accessToken)
        {
            try
            {
                var cookie = Request.Cookies.TryGetValue(refreshTokenKey, out var refreshToken);

                if (!cookie)
                {
                    throw new Exception("Refresh token cookie is not found!");
                }

                var userAgentData = Request.Headers["User-Agent"];

                var tokens = new TokensData() { AccessJwt = accessToken, RefreshJwt = refreshToken! };

                var userData = await _authService.Refresh(tokens, userAgentData);

                _logger.LogInformation($"Email: {userData.User.Email}\n" +
                    $"Access: {userData.TokensData.AccessJwt}\n" +
                    $"Refresh: {userData.TokensData.RefreshJwt}\n" +
                    $"Expired: {userData.TokensData.Expired}\n" +
                    $"LifeTime: {userData.TokensData.LifeTime}");

                AddRefreshTokenCookie(userData);

                return Ok(new AuthResponce(userData.User.Id, userData.User.Email, userData.TokensData.AccessJwt));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private void AddRefreshTokenCookie(UserData userData)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Domain = "localhost",
                Expires = new DateTimeOffset(userData.TokensData.Expired),
                MaxAge = TimeSpan.FromMinutes(userData.TokensData.LifeTime)
            };

            Response.Cookies.Append(refreshTokenKey, userData.TokensData.RefreshJwt, cookieOptions);
        }
    }
}
