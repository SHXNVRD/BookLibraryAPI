using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Isbn { get; set; } = null!;
        public string Title { get; set; } = null!;
        public List<GenreDto> Genres { get; set; } = [];
        public AuthorDto Author { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
