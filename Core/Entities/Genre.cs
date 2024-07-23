namespace Core.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public List<BookGenre> BookGenres { get; set; } = [];
    }
}