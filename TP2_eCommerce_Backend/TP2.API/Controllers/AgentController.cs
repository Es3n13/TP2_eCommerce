using Microsoft.AspNetCore.Mvc;
using TP2.Application.UseCases;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Agents;
using Microsoft.AspNetCore.Authorization;

namespace TP2.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "Agent")]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly GetPendingReviewsUseCase _getPendingReviewsUseCase;
        private readonly DecideDeclarationUseCase _decideUseCase;
        private readonly AssignDeclarationUseCase _assignUseCase;
        private readonly CreateAgentUseCase _createAgentUseCase;

        public AgentController(
            GetPendingReviewsUseCase getPendingReviewsUseCase,
            DecideDeclarationUseCase decideUseCase,
            AssignDeclarationUseCase assignUseCase,
            CreateAgentUseCase createAgentUseCase)
        {
            _getPendingReviewsUseCase = getPendingReviewsUseCase;
            _decideUseCase = decideUseCase;
            _assignUseCase = assignUseCase;
            _createAgentUseCase = createAgentUseCase;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAgent([FromBody] CreateAgentRequest request)
        {
            try
            {
                var agentId = await _createAgentUseCase.ExecuteAsync(request);
                return Ok(new { message = "Agent créé avec succès", id = agentId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = $"Erreur interne : {ex.Message}" });
            }
        }

        [HttpGet("pending-reviews")]
        public async Task<IActionResult> GetPendingReviews()
        {
            try
            {
                var reviews = await _getPendingReviewsUseCase.ExecuteAsync();

                if (reviews == null || reviews.Count == 0)
                {
                    return NotFound("Aucun dossier en attente de revue pour le moment.");
                }
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                // On logue l'erreur et on renvoie un 500
                return StatusCode(500, $"Erreur interne : {ex.Message}");
            }
        }

        [HttpPost("decide")]
        public async Task<IActionResult> Decide([FromBody] DecideDeclarationRequest request)
        {
            try
            {
                await _decideUseCase.ExecuteAsync(request);
                return Ok(new { message = "Décision enregistrée avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignDeclarationRequest request)
        {
            try
            {
                await _assignUseCase.ExecuteAsync(request);
                return Ok(new { message = "Dossier assigné avec succès" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
