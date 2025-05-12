using backend.Core.Models;

namespace backend.Application.Services
{
    public interface IContactRepository
    {
        Task<Guid> Create( Contact contact);
        Task<Guid?> Delete(Guid userId, Guid id);
        Task<(List<Contact>,int)> Get(Guid? userId, int page);
        Task<(List<Contact>,int)> Search(Guid? userId, string searchString,int page);
        Task<Guid?> Update(Guid id, Guid userId, bool isGlobal, string name, string number, string description);
    }
}