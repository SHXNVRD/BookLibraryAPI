using Core.Entities;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Helpers;
using Application.Interfaces;

namespace Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;

        public JwtService(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Audience = _options.Audience,
                Issuer = _options.Issuer,
                Subject = GenerateClaims(user),
                Expires = DateTime.Now.AddHours(_options.ExpiresHours),
                SigningCredentials = new SigningCredentials(KeyGenerator.GenerateFromXmlFile(_options.PrivateKeyPath), SecurityAlgorithms.RsaSha256)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            claims.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

            return claims;
        }
    }
}
