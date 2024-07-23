using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IGenresRepository
    {
        Task<Guid> CreateAsync(Genre genre);
        Task<int> DleteAsync(Guid id);
        Task<List<Genre>> GetAllAsync();
        Task<List<Genre>> GetByIdsAsync(List<Guid> genreIds);
        Task<Genre> GetByIdAsync(Guid id);
        Task<List<Genre>> GetByBookIdAsync(Guid bookId);
        Task<Guid> UpdateAsync(Guid id, Genre genre);
    }
}