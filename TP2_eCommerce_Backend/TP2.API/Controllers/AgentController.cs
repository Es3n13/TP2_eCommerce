using Microsoft.AspNetCore.Mvc;
using TP2.Application.UseCases;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Agents;

namespace TP2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentController : ControllerBase
    {
        private readonly GetPendingReviewsUseCase _getPendingReviewsUseCase;
        private readonly DecideDeclarationUseCase _decideUseCase;
        private readonly AssignDeclarationUseCase _assignUseCase;

        public AgentController(GetPendingReviewsUseCase getPendingReviewsUseCase, DecideDeclarationUseCase decideUseCase, AssignDeclarationUseCase assignDeclarationUseCase)
        {
            _getPendingReviewsUseCase = getPendingReviewsUseCase;
            _decideUseCase = decideUseCase;
            _assignUseCase = assignDeclarationUseCase;
        }
        /// <summary>
        /// Récupère la liste des déclarations en attente de revue (Statut: UnderReview)
        /// </summary>
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
                // Note : N'oublie pas d'injecter DecideDeclarationUseCase dans le constructeur !
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
