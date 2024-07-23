using Core.Entities;
using Infrastructure.Interfaces;
using Mapster;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public UserService(IUserRepository userRepository,
            IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task RegisterAsync(RegisterUserDto registerUser)
        {
            if (!_userRepository.IsUniqueEmail(registerUser.Email))
                throw new Exception();

            var user = registerUser.Adapt<User>();
            user.Password = PasswordHasher.Hash(registerUser.Password);
            await _userRepository.CreateAsync(user);
        }

        public async Task<string> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginUserDto.Email);
            var passwordIdVerified = PasswordHasher.Verify(loginUserDto.Password, user.Password);

            if (!passwordIdVerified)
                throw new Exception();

            return _jwtService.GenerateToken(user);
        }
    }
}
