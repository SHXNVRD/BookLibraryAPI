using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class GenresRepository : IGenresRepository
    {
        private readonly BookLibraryDbContext _context;

        public GenresRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Genre> GetByIdAsync(Guid id)
        {

            var book = await _context.Genres
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            return book ?? throw new NullReferenceException();
        }


        public async Task<List<Genre>> GetByBookIdAsync(Guid bookId)
        {
            var genres = await _context.BookGenres
                .AsNoTracking()
                .Where(bg => bg.BookId == bookId)
                .Select(bg => new Genre()
                {
                    Id = bg.GenreId,
                    Title = bg.Genre.Title
                }).ToListAsync();

            return genres;
        }

        public async Task<List<Genre>> GetByIdsAsync(List<Guid> genreIds)
        {
            return await _context.Genres.Where(g => genreIds.Contains(g.Id)).ToListAsync();
        }

        public async Task<Guid> CreateAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre.Id;
        }

        public async Task<int> DleteAsync(Guid id)
        {
            var deletedGenres = await _context.Genres
                .Where(g => g.Id == id)
                .ExecuteDeleteAsync();

            if (deletedGenres == 0)
                throw new Exception($"Book with id {id} not found");

            return deletedGenres;
        }

        public async Task<Guid> UpdateAsync(Guid id, Genre genre)
        {
            int updatedGenres = await _context.Genres
                .Where(g => g.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(g => g.Id, g => genre.Id)
                .SetProperty(g => g.Title, g => genre.Title));

            if (updatedGenres == 0)
                throw new Exception($"Genre with id {id} not found");

            return genre.Id;
        }
    }
}
