using Application.Interfaces;
using static BCrypt.Net.BCrypt;

namespace Application.Helpers
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
            => HashPassword(password);

        public static bool Verify(string password, string hashedPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}
