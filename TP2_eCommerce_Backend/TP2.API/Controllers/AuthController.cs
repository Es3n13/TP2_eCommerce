using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAgentRepository _agentRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IAgentRepository agentRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _agentRepository = agentRepository;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            // On tente de trouver l'utilisateur
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user != null && user.PasswordHash == dto.Password)
            {
                return GenerateJwtToken(user.Id, "User", user.FirstName);
            }

            // On tente de trouver l'agent
            var agent = await _agentRepository.GetByEmailAsync(dto.Email);
            if (agent != null && agent.PasswordHash == dto.Password)
            {
                return GenerateJwtToken(agent.Id, "Agent", agent.FullName);
            }

            return Unauthorized(new { message = "Identifiants invalides" });
        }
        private IActionResult GenerateJwtToken(int id, string role, string username)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                role = role
            });
        }
    }
}
