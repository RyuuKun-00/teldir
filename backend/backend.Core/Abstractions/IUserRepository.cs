using backend.Core.Models;

namespace backend.Application.Services
{
    public interface IUserRepository
    {
        Task<User> Create(AuthDto auth);
        Task<User?> Get(Guid id);
        Task<(User?,string?)> Get(string email);

        Task<bool> Exists(Guid id, string email);
    }
}