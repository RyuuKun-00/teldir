using backend.Core.Models;

namespace backend.DataAccess.Repositories
{
    public interface ITokenRepository
    {
        Task<string> Delete(string refreshToken);
        Task<(string?, DateTime)> Get(string refreshToken);
        Task<User?> GetUser(string refreshToken, UserAgentData userAgentData);
        Task<(DateTime, int)> SaveRefreshTokenAsync(Guid userId, string refreshToken, UserAgentData userAgentData, int lifeTime);
    }
}