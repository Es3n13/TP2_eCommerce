using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Declarations;

namespace TP2.API.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    public class TaxDeclarationsController : ControllerBase
    {
        private readonly ISubmitTaxDeclarationUseCase _submitUseCase;
        private readonly IGetUserDeclarationsUseCase _getUserDeclarationsUseCase;
        private readonly IInitializeDeclarationUseCase _initUseCase;
        private readonly ISaveDeclarationDraftUseCase _saveDraftUseCase;
        private readonly IUploadSupportingDocumentUseCase _uploadDocUseCase;
        private readonly IDownloadNoaUseCase _downloadNoaUseCase;

        public TaxDeclarationsController(
            ISubmitTaxDeclarationUseCase submitUseCase,
            IGetUserDeclarationsUseCase getUserDeclarationsUseCase,
            IInitializeDeclarationUseCase initUseCase,
            ISaveDeclarationDraftUseCase saveDraftUseCase,
            IUploadSupportingDocumentUseCase uploadDocUseCase,
            IDownloadNoaUseCase downloadNoaUseCase)
        {
            _submitUseCase = submitUseCase;
            _getUserDeclarationsUseCase = getUserDeclarationsUseCase;
            _initUseCase = initUseCase;
            _saveDraftUseCase = saveDraftUseCase;
            _uploadDocUseCase = uploadDocUseCase;
            _downloadNoaUseCase = downloadNoaUseCase;
        }

        private int GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim)) throw new UnauthorizedAccessException("Utilisateur non identifié.");
            return int.Parse(userIdClaim);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyDeclarations()
        {
            try
            {
                int userId = GetAuthenticatedUserId();
                var declarations = await _getUserDeclarationsUseCase.ExecuteAsync(userId);
                return Ok(declarations);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] SubmitDeclarationRequestDto request)
        {
            try
            {
                int authUserId = GetAuthenticatedUserId();
                var result = await _submitUseCase.ExecuteAsync(authUserId, request);
                return Ok(new { Message = "Déclaration soumise avec succès", DeclarationId = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.ToString() });
            }
        }

        [HttpPost("initialize")]
        public async Task<IActionResult> Initialize([FromBody] InitializeDeclarationRequestDto request)
        {
            try
            {
                int authUserId = GetAuthenticatedUserId();

                request.UserId = authUserId;

                var id = await _initUseCase.ExecuteAsync(request);
                return Ok(new { Message = "Déclaration initialisée", DeclarationId = id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("save-draft")]
        public async Task<IActionResult> SaveDraft([FromBody] SaveDeclarationDraftRequestDto request)
        {
            await _saveDraftUseCase.ExecuteAsync(request);
            return Ok(new { Message = "Brouillon sauvegardé" });
        }

        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocument(int declarationId, IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            var docId = await _uploadDocUseCase.ExecuteAsync(declarationId, file.FileName, ms.ToArray());
            return Ok(new { Message = "Document tranféré", DocumentId = docId });
        }

        [HttpGet("download-noa/{declarationId}")]
        public async Task<IActionResult> DownloadNoa(int declarationId)
        {
            try
            {
                var fileData = await _downloadNoaUseCase.ExecuteAsync(declarationId);

                if (fileData == null)
                {
                    return NotFound(new { Message = "Aucun avis de cotisation trouvé pour cette déclaration." });
                }

                return File(fileData.Content, fileData.ContentType, fileData.FileName);
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
