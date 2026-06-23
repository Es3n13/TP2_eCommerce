using Microsoft.AspNetCore.Mvc;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Users;

namespace TP2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController(ICreateUserUseCase createUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto request)
        {
            try
            {
                var userId = await _createUserUseCase.ExecuteAsync(request);
                return Ok(new { Message = "Utilisateur créé avec succès", UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                // Retourne un 400 si l'utilisateur existe déjà
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Erreur interne", Details = ex.Message });
            }
        }
    }
}