using backend.Core.Models;
using Microsoft.Extensions.Primitives;

namespace backend.Application.Services
{
    public interface IAuthService
    {
        Task<UserData> Login(AuthDto authDto, StringValues userAgentData);
        Task<string> Logout(string refreshToken);
        Task<UserData> Refresh(TokensData tokensClient, StringValues userAgentData);
        Task<UserData> Registration(AuthDto authDto, StringValues userAgentData);
    }
}