using Application.DTOs;

namespace Application.Interfaces
{
    public interface IGenreService
    {
        Task<Guid> CreateGenreAsync(GenreDto genreDto);
        Task<int> DeleteGenreAsync(Guid id);
        Task<List<GenreDto>> GetAllGenresAsync();
        Task<GenreDto> GetGenreByIdAsync(Guid id);
        Task<Guid> UpdateGenreAsync(Guid id, GenreDto genreDto);
    }
}