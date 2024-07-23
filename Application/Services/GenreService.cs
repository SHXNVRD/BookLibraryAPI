using Core.Entities;
using Infrastructure.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
    public class GenreService : IGenreService
    {
        private readonly Infrastructure.Interfaces.IGenresRepository _genreRepository;

        public GenreService(Infrastructure.Interfaces.IGenresRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return genres.Select(g => g.Adapt<GenreDto>()).ToList();
        }

        public async Task<GenreDto> GetGenreByIdAsync(Guid id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return genre.Adapt<GenreDto>();
        }

        public async Task<Guid> CreateGenreAsync(GenreDto genreDto)
        {
            return await _genreRepository.CreateAsync(genreDto.Adapt<Genre>());
        }

        public async Task<int> DeleteGenreAsync(Guid id)
        {
            return await _genreRepository.DleteAsync(id);
        }

        public async Task<Guid> UpdateGenreAsync(Guid id, GenreDto genreDto)
        {
            return await _genreRepository.UpdateAsync(id, genreDto.Adapt<Genre>());
        }
    }
}
