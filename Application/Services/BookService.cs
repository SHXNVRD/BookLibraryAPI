using Core.Entities;
using Mapster;
using Application.DTOs;
using Application.Interfaces;

namespace Infrastructure.Interfaces
{
    public class BookService : IBookService
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IGenresRepository _genresRepository;
        private readonly IBookGenresRepository _bookGenresRepository;

        public BookService(IBooksRepository booksRepository, IGenresRepository genresRepository, IAuthorsRepository authorsRepository, IBookGenresRepository bookGenresRepository)
        {
            _booksRepository = booksRepository;
            _genresRepository = genresRepository;
            _authorsRepository = authorsRepository;
            _bookGenresRepository = bookGenresRepository;
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await _booksRepository.GetAllAsync();
            var bookDtos = books.Adapt<List<BookDto>>();
            return bookDtos;
        }

        public async Task<BookDto> GetBookByIdAsync(Guid id)
        {
            var book = await _booksRepository.GetByIdAsync(id);
            var bookDto = book.Adapt<BookDto>();
            return bookDto;
        }

        public async Task<Guid> CreateBookAsync(CreateBookDto createBookDto)
        {
            var bookId = await _booksRepository.CreateAsync(createBookDto.Adapt<Book>());
            var genres = await _genresRepository.GetByIdsAsync(createBookDto.GenreIds);

            var bookGenres = genres.Select(g => new BookGenre()
            {
                BookId = bookId,
                GenreId = g.Id
            }).ToList();

            await _bookGenresRepository.AddRangeAsync(bookGenres);

            return bookId;
        }

        public async Task<int> DeleteBookAsync(Guid id)
        {
            return await _booksRepository.DleteAsync(id);
        }

        public async Task<Guid> UpdateBookAsync(Guid id, UpdateBookDto updateBookDto)
        {
            var updatedBookId =  await _booksRepository.UpdateAsync(id, updateBookDto.Adapt<Book>());

            await _bookGenresRepository.UpdateByBookIdAsync(id, updateBookDto.GenreIds);

            return updatedBookId;
        }
    }
}
