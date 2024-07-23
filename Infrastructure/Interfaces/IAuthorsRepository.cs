using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IAuthorsRepository
    {
        Task<Guid> CreateAsync(Author author);
        Task<int> DleteAsync(Guid id);
        Task<List<Author>> GetAllAsync();
        Task<Author> GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid id, Author author);
    }
}