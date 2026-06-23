using Microsoft.AspNetCore.Mvc;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Declarations;

namespace TP2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // L'URL sera : api/TaxDeclarations
    public class TaxDeclarationsController : ControllerBase
    {
        private readonly ISubmitTaxDeclarationUseCase _submitUseCase;
        private readonly IGetUserDeclarationsUseCase _getUserDeclarationsUseCase;
        public TaxDeclarationsController(
            ISubmitTaxDeclarationUseCase submitUseCase,
            IGetUserDeclarationsUseCase getUserDeclarationsUseCase)
        {
            _submitUseCase = submitUseCase;
            _getUserDeclarationsUseCase = getUserDeclarationsUseCase;
        }

        // POST: api/TaxDeclarations/submit
        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitDeclarationRequestDto request)
        {
            try
            {
                var result = await _submitUseCase.ExecuteAsync(request.UserId, request);
                return Ok(new { Message = "Déclaration soumise avec succès", DeclarationId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // GET: api/TaxDeclarations/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            try
            {
                var declarations = await _getUserDeclarationsUseCase.ExecuteAsync(userId);
                return Ok(declarations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
