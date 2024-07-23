using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> CreateAsync(User user);
        Task<User> GetByEmailAsync(string email);
        bool IsUniqueEmail(string email);
    }
}