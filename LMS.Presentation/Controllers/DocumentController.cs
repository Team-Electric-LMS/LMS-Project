using LMS.Shared.DTOs.DocumentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Presentation.Controllers;
[ApiController]
[Route("api/documents")]
public class DocumentsController(IServiceManager serviceManager) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetDocuments([FromQuery] string level, [FromQuery] Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest("Invalid ID");

        IEnumerable<DocumentDto> docs = level.ToLower() switch
        {
            "course" => await serviceManager.DocumentService.GetDocumentsByCourseAsync(id),
            "module" => await serviceManager.DocumentService.GetDocumentsByModuleAsync(id),
            "activity" => await serviceManager.DocumentService.GetDocumentsByActivityAsync(id),
            _ => null
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
    public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] DocumentUploadDto dto)
    {
        if (file == null) return BadRequest("No file uploaded");

        var document = await serviceManager.DocumentService.UploadAsync(file, dto);
        return Ok(document);
    }

    [HttpGet("download/{id}")]
    public async Task<IActionResult> Download(Guid id)
    {
        var result = await serviceManager.DocumentService.DownloadAsync(id);
        if (result == null) return NotFound();
        return result;
    }
}
