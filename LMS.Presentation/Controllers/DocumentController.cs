using LMS.Shared.DTOs.DocumentDTOs;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Swashbuckle.AspNetCore.Annotations;
using System.Buffers.Text;
using System.Collections.Generic;

namespace LMS.Presentation.Controllers;
[ApiController]
[Route("api/documents")]
public class DocumentsController(IServiceManager serviceManager) : ControllerBase
{

    [HttpGet]
    [Authorize]
    [SwaggerOperation(
        Summary = "Gets metadata for documents related to a course, module or activity",
        Description = "Gets a list of Document Ids, Names, Links based for a specific course, module, activity by Id")]
    [SwaggerResponse(StatusCodes.Status200OK, "Documents data")]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    public async Task<IActionResult> GetDocuments([FromQuery] string level, [FromQuery] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid ID");

        IEnumerable<DocumentDto> docs = level.ToLower() switch
        {
            "course" => await serviceManager.DocumentService.GetDocumentsByCourseAsync(id),
            "module" => await serviceManager.DocumentService.GetDocumentsByModuleAsync(id),
            "activity" => await serviceManager.DocumentService.GetDocumentsByActivityAsync(id)
        };

        if (docs == null)
            return NotFound($"No documents found for this {level}.");

        var result = docs.Select(d => new
        {
            d.Id,
            d.Name,
            d.Link
        }).ToList();

        return Ok(result);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] DocumentUploadDto dto)
    {
        if (file == null) return BadRequest("No file uploaded");

        var document = await serviceManager.DocumentService.UploadAsync(file, dto);
        return Ok(document);
    }

    [HttpGet("download/{id}")]
    [Authorize]
    [SwaggerOperation(
        Summary = "Download a document by Id",
        Description = "Download a document by Id")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized - JWT token missing or invalid")]
    public async Task<IActionResult> Download(Guid id)
    {
        var result = await serviceManager.DocumentService.DownloadAsync(id);
        if (result == null) return NotFound();
        return result;
    }
}