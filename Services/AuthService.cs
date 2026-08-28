using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace StudentApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        // Demo credentials - for learning/portfolio purposes only
        private static readonly Dictionary<string, string> DemoUsers = new()
        {
            { "admin", "Admin@123" },
            { "user", "User@123" }
        };

        private static readonly Dictionary<string, string> UserRoles = new()
        {
            { "admin", "Admin" },
            { "user", "User" }
        };

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateCredentials(string username, string password)
        {
            return DemoUsers.TryGetValue(username, out var storedPassword) &&
                   storedPassword == password;
        }

        public string? GenerateToken(string username, string role)
        {
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is not configured")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60")),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
