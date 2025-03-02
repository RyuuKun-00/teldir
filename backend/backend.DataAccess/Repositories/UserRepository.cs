

namespace backend.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly ContactStoreDBContext _context;

        public UserRepository(ContactStoreDBContext context)
        {
            _context = context;
        }
    }
}
