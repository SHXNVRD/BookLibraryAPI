using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder
                .Property(g => g.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasMany(g => g.BookGenres)
                .WithOne(bg => bg.Genre)
                .HasForeignKey(bg => bg.GenreId);
        }
    }
}
