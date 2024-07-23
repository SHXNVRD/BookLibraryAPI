using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BookGenresRepository : IBookGenresRepository
    {
        private readonly BookLibraryDbContext _context;

        public BookGenresRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookGenre>> GetByBookIdAsync(Guid bookId)
        {
            return await _context.BookGenres
                .Where(bg => bg.BookId == bookId)
                .ToListAsync();
        }

        public async Task<BookGenre> CreateAsync(BookGenre bookGenre)
        {
            await _context.AddAsync(bookGenre);
            await _context.SaveChangesAsync();
            return bookGenre;
        }

        public async Task AddRangeAsync(List<BookGenre> bookGenres)
        {
            await _context.AddRangeAsync(bookGenres);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateByBookIdAsync(Guid bookId, List<Guid> genreIds)
        {
            var existingGenres = await _context.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();

            if (existingGenres.Count != genreIds.Count)
                throw new Exception();

            // Удаление старых связей
            var currentBookGenres = await GetByBookIdAsync(bookId);
            var bookGenresToDelete = currentBookGenres
                .Where(bg => !genreIds.Contains(bg.GenreId))
                .ToList();
            await RemoveRangeAsync(bookGenresToDelete);

            // Добавление новых связей
            var currentBookGenreIds = currentBookGenres
                .Select(bg => bg.GenreId)
                .ToList();
            var bookGenresToAdd = genreIds
                .Where(genreId => !currentBookGenreIds.Contains(genreId))
                .Select(genreId => new BookGenre
                {
                    BookId = bookId,
                    GenreId = genreId
                }).ToList();
            await AddRangeAsync(bookGenresToAdd);
        }

        public async Task RemoveRangeAsync(List<BookGenre> bookGenres)
        {
            _context.RemoveRange(bookGenres);
            await _context.SaveChangesAsync();
        }
    }
}
