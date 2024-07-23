using Core.Entities;

namespace Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}