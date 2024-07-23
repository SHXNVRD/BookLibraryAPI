using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateBookDto
    {
        public string Isbn { get; set; } = null!;
        public string Title { get; set; } = null!;
        public List<Guid> GenreIds { get; set; } = [];
        public Guid AuthorId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime IssuedDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
