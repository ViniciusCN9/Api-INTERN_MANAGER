using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DesafioAPI.application.Interfaces;
using DesafioAPI.domain.Entities;
using DesafioAPI.domain.Settings;
using Microsoft.IdentityModel.Tokens;

namespace DesafioAPI.application.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey.secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Username.ToString()),
                    new Claim(ClaimTypes.Email, account.Email.ToString()),
                    new Claim(ClaimTypes.Role, account.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}