namespace Application.Services
{
    public class JwtOptions
    {
        public string PrivateKeyPath { get; set; } = null!;
        public string PublicKeyPath { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public int ExpiresHours { get; set; }
    }
}