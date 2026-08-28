using Microsoft.AspNetCore.Mvc;
using StudentApi.DTOs;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        /// <summary>
        /// Authenticate user with username and password to receive JWT token.
        /// Demo credentials:
        /// - Username: admin, Password: Admin@123 (Admin role)
        /// - Username: user, Password: User@123 (User role)
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_authService.ValidateCredentials(loginDto.Username, loginDto.Password))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var role = loginDto.Username == "admin" ? "Admin" : "User";
            var token = _authService.GenerateToken(loginDto.Username, role);

            if (token == null)
            {
                return StatusCode(500, new { message = "Failed to generate token" });
            }

            var expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "60");

            return Ok(new
            {
                token = token,
                username = loginDto.Username,
                role = role,
                expiresInSeconds = expiresInMinutes * 60
            });
        }
    }
}