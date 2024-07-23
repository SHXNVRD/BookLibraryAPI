using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IBookGenresRepository
    {
        Task<List<BookGenre>> GetByBookIdAsync(Guid bookId);
        Task<BookGenre> CreateAsync(BookGenre bookGenre);
        Task AddRangeAsync(List<BookGenre> bookGenres);
        Task RemoveRangeAsync(List<BookGenre> bookGenres);
        Task UpdateByBookIdAsync(Guid bookId, List<Guid> genreIds);
    }
}