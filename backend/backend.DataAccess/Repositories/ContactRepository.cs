using backend.Core.Models;
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactStoreDBContext _context;

        public ContactRepository(ContactStoreDBContext context)
        {
            _context = context;
        }

        public async Task<List<Contact>> Get()
        {
            var contactsEntity = await _context.Contacts
                .AsNoTracking()
                .ToListAsync();

            var contacts = contactsEntity.Select(x => new Contact(x.Id, x.Name, x.Number, x.Description)).ToList();

            return contacts;
        }

        public async Task<Guid> Create(Contact contact)
        {
            var contactEntity = new ContactEntity()
            {
                Id = contact.Id,
                Name = contact.Name,
                Number = contact.Number,
                Description = contact.Description
            };

            await _context.AddAsync(contactEntity);
            await _context.SaveChangesAsync();

            return contactEntity.Id;
        }


        public async Task<Guid> Update(Guid id, string name, string number, string description)
        {
            await _context.Contacts
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync<ContactEntity>(y => y
                    .SetProperty(a => a.Name, b => name)
                    .SetProperty(a => a.Number, b => number)
                    .SetProperty(a => a.Description, b => description)
                );
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Contacts
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
            return id;
        }
    }
}
