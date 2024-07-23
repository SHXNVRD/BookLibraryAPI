using Core.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Mappers
{
    public static class BookMappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Book, BookDto>.NewConfig()
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre).Adapt<List<GenreDto>>());
        }
    }
}

