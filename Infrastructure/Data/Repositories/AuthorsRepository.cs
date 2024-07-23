using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class AuthorsRepository : IAuthorsRepository
    {
        private readonly BookLibraryDbContext _context;

        public AuthorsRepository(BookLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Author> GetByIdAsync(Guid id)
        {

            var book = await _context.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);

            return book ?? throw new NullReferenceException();
        }

        public async Task<Guid> CreateAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }

        public async Task<int> DleteAsync(Guid id)
        {
            var deletedAuthors = await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteDeleteAsync();

            if (deletedAuthors == 0)
                throw new Exception($"Author with id {id} not found");

            return deletedAuthors;
        }

        public async Task<Guid> UpdateAsync(Guid id, Author author)
        {
            int updatedAuthors = await _context.Authors
                .Where(a => a.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Id, a => author.Id)
                .SetProperty(a => a.Name, a => author.Name));

            if (updatedAuthors == 0)
                throw new Exception($"Author with id {id} not found");

            return author.Id;
        }
    }
}
