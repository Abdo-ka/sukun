using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sukun.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sukun.Application.Implemantation
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryMinutes;

        public JwtService(IConfiguration configuration)
        {
            _secretKey = configuration["jwtSettings:secret"] ?? throw new ArgumentNullException("jwtSettings:secret");
            _issuer = configuration["jwtSettings:issuer"] ?? "Sukun";
            _audience = configuration["jwtSettings:audience"] ?? "Sukun";
            _expiryMinutes = int.Parse(configuration["jwtSettings:AccessTokenExpireDate"] ?? "60");
        }

        public string GenerateToken(Guid adminId, string email, string role)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, adminId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}