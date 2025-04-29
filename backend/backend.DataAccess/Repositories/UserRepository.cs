

using backend.Application.Services;
using backend.Core.Models;
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContactStoreDBContext _context;

        public UserRepository(ContactStoreDBContext context)
        {
            _context = context;
        }

        public async Task<User> Create(AuthDto auth)
        {

            var entity = new UserEntity()
            {
                Id = new Guid(),
                Email = auth.Email,
                Password = auth.Password
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new User() { Id = entity.Id, Email = entity.Email };
        }

        public async Task<User?> Get(Guid id)
        {
            var entity = await _context.Users.Where(x => x.Id.Equals(id)).ToListAsync();

            var user = entity.Select(x => new User() { Id = x.Id, Email = x.Email! }).FirstOrDefault();

            return user;
        }

        public async Task<(User?,string?)> Get(string email)
        {
            var entity = await _context.Users.Where(x => x.Email.Equals(email)).ToListAsync();

            var userData = entity.Select(x => (new User() { Id = x.Id, Email = x.Email!},x.Password)).FirstOrDefault();

            return userData;
        }

        public async Task<bool> Exists(Guid id,string email)
        {
            var result = await _context.Users.AnyAsync(x => x.Id.Equals(id) && x.Email.Equals(email));

            return result;
        }
    }
}
