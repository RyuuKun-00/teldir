

using backend.Core.Models;
using backend.DataAccess.Repositories;

namespace backend.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<List<Contact>> ContactGetAll()
        {
            return await _contactRepository.Get();
        }

        public async Task<List<Contact>> ContactSearch(string? searchString)
        {
            if (String.IsNullOrEmpty(searchString))
            {
                return await _contactRepository.Get();
            }else{
                return await _contactRepository.Search(searchString);
            }
        }

        public async Task<Guid> ContactCreate(Contact contact)
        {
            return await _contactRepository.Create(contact);
        }

        public async Task<Guid> ContactUpdate(Guid id, string name, string number, string description)
        {
            return await _contactRepository.Update(id, name, number, description);
        }

        public async Task<Guid> ContactDelete(Guid id)
        {
            return await _contactRepository.Delete(id);
        }
    }
}
