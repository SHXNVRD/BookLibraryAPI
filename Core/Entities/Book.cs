namespace Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }
        
        public string Isbn { get; set; } = null!;

        public string Title { get; set; } = null!;

        public List<BookGenre> BookGenres { get; set; } = [];

        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }

        public string Description { get; set; } = null!;

        public DateTime IssuedDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}
