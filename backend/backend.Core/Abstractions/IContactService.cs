using backend.Core.Models;

namespace backend.Application.Services
{
    public interface IContactService
    {
        Task<Guid> ContactCreate(Contact contact);
        Task<Guid> ContactDelete(Guid id);
        Task<List<Contact>> ContactGetAll();
        Task<Guid> ContactUpdate(Guid id, string name, string number, string description);
    }
}