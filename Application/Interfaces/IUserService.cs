using Application.DTOs;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(LoginUserDto loginUserDto);
        Task RegisterAsync(RegisterUserDto registerUser);
    }
}