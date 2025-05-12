using backend.Core.Models;
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess
{
    public class ContactStoreDBContext: DbContext
    {
        public ContactStoreDBContext(DbContextOptions<ContactStoreDBContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<UserAgentEntity> UsersAgent { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
    }
}
