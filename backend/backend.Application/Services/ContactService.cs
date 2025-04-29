

using backend.Core.Models;

namespace backend.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<(List<Contact>,int)> ContactGetAll(Guid? userId,int page)
        {
            return await _contactRepository.Get(userId,page);
        }

        public async Task<(List<Contact>,int)> ContactSearch(Guid? userId, string? searchString,int page)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return await _contactRepository.Get(userId,page);
            }
            else
            {
                return await _contactRepository.Search(userId, searchString,page);
            }
        }

        public async Task<Guid> ContactCreate( Contact contact)
        {
            return await _contactRepository.Create(contact);
        }

        public async Task<Guid?> ContactUpdate(Guid id, Guid userId, bool isGlobal, string name, string number, string description)
        {
            return await _contactRepository.Update(id, userId, isGlobal, name, number, description);
        }

        public async Task<Guid?> ContactDelete(Guid userId, Guid id)
        {
            return await _contactRepository.Delete(userId, id);
        }
    }
}
