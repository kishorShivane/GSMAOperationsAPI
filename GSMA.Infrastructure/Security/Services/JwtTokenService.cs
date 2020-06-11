using GSMA.DataProvider.Data;
using GSMA.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GSMA.Infrastructure.Security.Services
{
    public class JwtTokenService
    {
        private readonly string _secret;
        private readonly string _expDate;

        public JwtTokenService(IConfiguration config)
        {
            _secret = config.GetSection("JwtConfig").GetSection("Secret").Value;
            _expDate = config.GetSection("JwtConfig").GetSection("ExpirationInDays").Value;
        }

        public string GenerateSecurityToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "GSMA Operations",
                Audience = "GSMA Users",
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("ID", user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("UserTypeID", user.UserTypeId.ToString()),
                    new Claim("CreatedDateTime", user.CapturedDateTime.Value.ToShortDateString()),
                }),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(int.Parse(_expDate)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
