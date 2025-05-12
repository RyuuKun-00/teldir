using backend.Application.Services;
using backend.Core.Models;
using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DataAccess.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactStoreDBContext _context;
        const int limitPage = 40;

        public ContactRepository(ContactStoreDBContext context)
        {
            _context = context;
        }

        public async Task<(List<Contact>,int)> Get(Guid? userId,int page)
        {
            List<ContactEntity> contactsEntity;

            var count = await GetCountContacts(userId);

            if(count <= (page - 1) * limitPage)
            {
                return (new List<Contact>(), count);
            }

            if (userId is null)
            {
                contactsEntity = await _context.Contacts
                    .Where(x => x.IsGlobal)
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * limitPage)
                    .Take(limitPage)
                    .ToListAsync();
            }
            else
            {
                contactsEntity = await _context.Contacts
                    .Where(x => x.UserEntityId == userId || x.IsGlobal)
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * limitPage)
                    .Take(limitPage)
                    .ToListAsync();
            }

            var contacts = contactsEntity
                .Select(x => new Contact(
                    x.Id,
                    (userId is not null && x.UserEntityId == userId) ? x.UserEntityId : null,
                    x.IsGlobal,
                    x.Name,
                    x.Number,
                    x.Description))
                .ToList();

            

            return (contacts,count);
        }

        public async Task<(List<Contact>,int)> Search(Guid? userId, string searchString, int page)
        {
            string str = searchString.ToLower();

            var count = await GetCountContacts(userId, searchString);

            List<ContactEntity> contactsEntity;

            if (count <= (page - 1) * limitPage)
            {
                return (new List<Contact>(), count);
            }

            if (userId is null)
            {
                contactsEntity = await _context.Contacts
                    .Where(p => p.IsGlobal && (EF.Functions.Like(p.Name.ToLower(), $"%{str}%") || EF.Functions.Like(p.Number.ToLower(), $"%{str}%")))
                    .OrderBy(c => c.Name)
                    .Skip((page - 1) * limitPage)
                    .Take(limitPage)
                    .ToListAsync();
            }
            else
            {
                contactsEntity = await _context.Contacts
                    .Where(p => (p.UserEntityId == userId || p.IsGlobal) && (EF.Functions.Like(p.Name.ToLower(), $"%{str}%") || EF.Functions.Like(p.Number.ToLower(), $"%{str}%")))
                    .OrderBy(c => c.Name)
                    .Skip((page -1)*limitPage)
                    .Take(limitPage)
                    .ToListAsync();
            }

            var contacts = contactsEntity
                .Select(x => new Contact(
                    x.Id,
                    (userId is not null && x.UserEntityId == userId) ? x.UserEntityId : null,
                    x.IsGlobal,
                    x.Name,
                    x.Number,
                    x.Description))
                .ToList();

            

            return (contacts, count);
        }

        public async Task<Guid> Create( Contact contact)
        {
            var contactEntity = new ContactEntity()
            {
                Id = contact.Id,
                UserEntityId = contact.UserId,
                IsGlobal = contact.IsGlobal,
                Name = contact.Name,
                Number = contact.Number,
                Description = contact.Description
            };

            await _context.AddAsync(contactEntity);
            await _context.SaveChangesAsync();

            return contactEntity.Id;
        }


        public async Task<Guid?> Update(Guid id, Guid userId, bool isGlobal, string name, string number, string description)
        {
            var count = await _context.Contacts
                .Where(x => x.Id == id && x.UserEntityId == userId)
                .ExecuteUpdateAsync<ContactEntity>(y => y
                    .SetProperty(a => a.IsGlobal, b => isGlobal)
                    .SetProperty(a => a.Name, b => name)
                    .SetProperty(a => a.Number, b => number)
                    .SetProperty(a => a.Description, b => description)
                );
            return (count > 0) ? id : null;
        }

        public async Task<Guid?> Delete(Guid userId, Guid id)
        {
            var count = await _context.Contacts
                .Where(x => x.Id == id && x.UserEntityId == userId)
                .ExecuteDeleteAsync();
            return (count > 0) ? id : null;
        }

        private async Task<int> GetCountContacts(Guid? userId,string? search = null)
        {
            int count;
            if(userId is null)
            {
                if(String.IsNullOrEmpty(search))
                {
                    count = await _context.Contacts.CountAsync(x=>x.IsGlobal);
                }
                else
                {
                    count = await _context.Contacts.CountAsync(x =>x.IsGlobal && (EF.Functions.Like(x.Name.ToLower(), $"%{search}%") || EF.Functions.Like(x.Number.ToLower(), $"%{search}%")));
                }
            }
            else
            {
                if (String.IsNullOrEmpty(search))
                {
                    count = await _context.Contacts.CountAsync(x => x.UserEntityId == userId || x.IsGlobal);
                }
                else
                {
                    count = await _context.Contacts.CountAsync(x => (x.UserEntityId == userId || x.IsGlobal) && (EF.Functions.Like(x.Name.ToLower(), $"%{search}%") || EF.Functions.Like(x.Number.ToLower(), $"%{search}%")));
                }
            }
            return count;
        }
    }
}
