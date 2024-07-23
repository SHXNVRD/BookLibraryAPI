namespace Core.Entities
{
    public class Author
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime DayOfBirth { get; set; }

        public List<Book> Books { get; set; } = [];
    }
}
