using backend.Core.Models;

namespace backend.DataAccess.Repositories
{
    public interface IContactRepository
    {
        Task<Guid> Create(Contact contact);
        Task<Guid> Delete(Guid id);
        Task<List<Contact>> Get();

        Task<List<Contact>> Search(string searchString);
        Task<Guid> Update(Guid id, string name, string number, string description);
    }
}