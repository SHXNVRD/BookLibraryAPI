using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookLibraryDbContext _context;

        public BooksRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .ToListAsync();
        }

        public async Task<Book> GetByIdAsync(Guid id)
        {
            var book = await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);

            return book ?? throw new NullReferenceException();
        }

        public async Task<Guid> CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<int> DleteAsync(Guid id)
        {
            var deletedBooks = await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();

            if (deletedBooks == 0)
                throw new Exception($"Book with id {id} not found");

            return deletedBooks;
        }

        public async Task<Guid> UpdateAsync(Guid id, Book book)
        {
            //var existingBook = await _context.Books
            //    .FirstOrDefaultAsync(b => b.Id == id) ?? throw new Exception($"Book with Id = {id} not found");

            var author = await _context.Authors
                .FirstOrDefaultAsync(a => a.Id == book.AuthorId) ?? throw new Exception($"Author with Id = {book.AuthorId} not found");

            int updatedBooks = await _context.Books
                .Where(b => b.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.Isbn, b => book.Isbn)
                .SetProperty(b => b.Title, b => book.Title)
                .SetProperty(b => b.AuthorId, b => book.AuthorId)
                .SetProperty(b => b.Description, b => book.Description)
                .SetProperty(b => b.IssuedDate, b => book.IssuedDate)
                .SetProperty(b => b.ReturnDate, b => book.ReturnDate));

            if (updatedBooks == 0)
                throw new Exception($"Book with Id = {id} not found");

            return id;
        }
    }
}
