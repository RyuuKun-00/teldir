using backend.Core.Models;

namespace backend.Application.Services
{
    public interface IContactService
    {
        Task<Guid> ContactCreate( Contact contact);
        Task<Guid?> ContactDelete(Guid userId, Guid id);
        Task<(List<Contact>,int)> ContactGetAll(Guid? userId,int page);
        Task<(List<Contact>,int)> ContactSearch(Guid? userId, string? searchString,int page);
        Task<Guid?> ContactUpdate(Guid id, Guid userId, bool isGlobal, string name, string number, string description);
    }
}