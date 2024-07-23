using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class BookLibraryDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> BookGenres { get; set; }
        public DbSet<User> Users { get; set; }

        public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AuthorConfiguration())
                .ApplyConfiguration(new GenreConfiguration())
                .ApplyConfiguration(new BookConfiguration())
                .ApplyConfiguration(new BookGenreConfiguration())
                .ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
