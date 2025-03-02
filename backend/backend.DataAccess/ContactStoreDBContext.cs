using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess
{
    public class ContactStoreDBContext: DbContext
    {
        public ContactStoreDBContext(DbContextOptions<ContactStoreDBContext> options) : base(options) { }

        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
