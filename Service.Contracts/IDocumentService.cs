using LMS.Shared.DTOs.DocumentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;
public interface IDocumentService
{
    Task<IEnumerable<DocumentDto>> GetDocumentsByCourseAsync(Guid courseId);
    Task<IEnumerable<DocumentDto>> GetDocumentsByModuleAsync(Guid moduleId);
    Task<IEnumerable<DocumentDto>> GetDocumentsByActivityAsync(Guid activityId);
    Task<DocumentDto> UploadAsync(IFormFile file, DocumentUploadDto dto);
    Task<FileStreamResult?> DownloadAsync(Guid documentId);
}
