using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<Guid> CreateBookAsync(CreateBookDto createBookDto);
        Task<int> DeleteBookAsync(Guid id);
        Task<List<BookDto>> GetAllBooksAsync();
        Task<BookDto> GetBookByIdAsync(Guid id);
        Task<Guid> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto);
    }
}