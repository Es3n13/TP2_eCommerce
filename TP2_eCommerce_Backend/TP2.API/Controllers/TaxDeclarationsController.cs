using Microsoft.AspNetCore.Mvc;
using TP2.Application.DTOs;
using TP2.Application.UseCases.Declarations;

namespace TP2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // api/TaxDeclarations
    public class TaxDeclarationsController : ControllerBase
    {
        private readonly ISubmitTaxDeclarationUseCase _submitUseCase;
        private readonly IGetUserDeclarationsUseCase _getUserDeclarationsUseCase;
        private readonly IInitializeDeclarationUseCase _initUseCase;
        private readonly ISaveDeclarationDraftUseCase _saveDraftUseCase;
        private readonly IUploadSupportingDocumentUseCase _uploadDocUseCase;

        public TaxDeclarationsController(
            ISubmitTaxDeclarationUseCase submitUseCase,
            IGetUserDeclarationsUseCase getUserDeclarationsUseCase,
            IInitializeDeclarationUseCase initUseCase,
            ISaveDeclarationDraftUseCase saveDraftUseCase,
            IUploadSupportingDocumentUseCase uploadDocUseCase)
        {
            _submitUseCase = submitUseCase;
            _getUserDeclarationsUseCase = getUserDeclarationsUseCase;
            _initUseCase = initUseCase;
            _saveDraftUseCase = saveDraftUseCase;
            _uploadDocUseCase = uploadDocUseCase;
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
                return BadRequest(new { error = ex.ToString() });
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

        [HttpPost("initialize")]
        public async Task<IActionResult> Initialize([FromBody] InitializeDeclarationRequestDto request)
        {
            var id = await _initUseCase.ExecuteAsync(request);
            return Ok(new { Message = "Déclaration initialisée", DeclarationId = id });
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
    }
}
