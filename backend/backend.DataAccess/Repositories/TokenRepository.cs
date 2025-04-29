using backend.Core.Models;
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ContactStoreDBContext _context;

        public TokenRepository(ContactStoreDBContext context)
        {
            _context = context;
        }

        public async Task<(DateTime, int)> SaveRefreshTokenAsync(
            Guid userId,
            string refreshToken,
            UserAgentData userAgentData, int lifeTime)
        {
            var userToken = await _context.Tokens
                                      .Include(t => t.AgentEntity)
                                      .FirstOrDefaultAsync(t => t.UserId.Equals(userId)
                                                                && t.AgentEntity.OS.Equals(userAgentData.OS)
                                                                && t.AgentEntity.Browser.Equals(userAgentData.Browser));

            (DateTime, int) tokenData;

            var now = DateTime.UtcNow;

            if (userToken is not null)
            {

                userToken.RefreshToken = refreshToken;
                userToken.Created = now;
                userToken.Expired = now.AddMinutes(userToken.LifeTime);

                tokenData.Item1 = userToken.Expired;
                tokenData.Item2 = userToken.LifeTime;

                await _context.SaveChangesAsync();
            }
            else
            {
                var transaction = await _context.Database.BeginTransactionAsync();

                var token = await _context.Tokens.AddAsync(new TokenEntity { 
                                                                        UserId = userId, 
                                                                        RefreshToken = refreshToken, 
                                                                        LifeTime = lifeTime ,
                                                                        Created = now,
                                                                        Expired = now.AddMinutes(lifeTime)
                });

                tokenData.Item1 = token.Entity.Expired;
                tokenData.Item2 = token.Entity.LifeTime;

                await _context.UsersAgent.AddAsync(new UserAgentEntity { Id = token.Entity.Id, OS = userAgentData.OS, Browser = userAgentData.Browser });

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }

            return tokenData;
        }

        public async Task<(string?, DateTime)> Get(string refreshToken)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.RefreshToken.Equals(refreshToken));

            if (token == null)
            {
                return (null, DateTime.Now);
            }

            return (token.RefreshToken, token.Expired);
        }

        public async Task<User?> GetUser(string refreshToken, UserAgentData userAgentData)
        {
            var user = await _context.Tokens.Include(t => t.AgentEntity)
                                            .Where(t => t.RefreshToken.Equals(refreshToken) &&
                                                      t.AgentEntity.OS.Equals(userAgentData.OS) &&
                                                      t.AgentEntity.Browser.Equals(userAgentData.Browser))
                                            .Select(t => t.User)
                                            .FirstOrDefaultAsync();
            if (user is null)
            {
                return null;
            }

            return new User() { Id = user.Id, Email = user.Email };
        }

        public async Task<string> Delete(string refreshToken)
        {
            var token = await _context.Tokens.FirstOrDefaultAsync(t => t.RefreshToken.Equals(refreshToken));

            if (token == null)
            {
                throw new Exception("In database refresh token is not found!");
            }

            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
