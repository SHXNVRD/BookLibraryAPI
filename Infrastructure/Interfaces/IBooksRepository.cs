using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBooksRepository
    {
        Task<Guid> CreateAsync(Book book);
        Task<int> DleteAsync(Guid id);
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(Guid id);
        Task<Guid> UpdateAsync(Guid id, Book book);
    }
}