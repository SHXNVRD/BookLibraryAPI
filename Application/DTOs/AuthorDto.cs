using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DayOfBirth { get; set; }
    }
}
