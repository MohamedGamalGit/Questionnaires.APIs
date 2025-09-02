using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Questionnaires.API.Data;
using Questionnaires.API.Data.Models;
using Questionnaires.API.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Questionnaires.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly QuestionnairesDbContext _context;

        public UsersController(IConfiguration config, QuestionnairesDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userLogin.Username);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, userLogin.Password);

            if (user != null && CustomPasswordHasher.VerifyPassword(userLogin.Password, user.PasswordHash))
            {
                var token = GenerateJwtToken(user.Username, user.Role);
                return Ok(new { token });
            }

            return Unauthorized("Invalid username or password");
        }

        private string GenerateJwtToken(string username, string role)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterDto model)
        {
            var existing = _context.Users.FirstOrDefault(x => x.Username == model.Username);
            if (existing != null)
                return BadRequest("User already exists");

            var hashedPassword = CustomPasswordHasher.HashPassword(model.Password);

            var user = new User
            {
                Username = model.Username,
                Role = model.Role // default role
            };
            user.PasswordHash = hashedPassword;

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok();
        }

    }
    public class UserLoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserRegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

