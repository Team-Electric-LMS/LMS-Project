using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Shared.DTOs.DocumentDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace LMS.Services;

public class DocumentService : IDocumentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorageService _fileStorage;
    private readonly IMapper _mapper;

    public DocumentService(IUnitOfWork unitOfWork, IFileStorageService fileStorage, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DocumentDto>> GetDocumentsByCourseAsync(Guid courseId)
    {
        var docs = await _unitOfWork.DocumentRepository.GetDocumentsByCourseAsync(courseId);
        var dtos = docs.Select(s => _mapper.Map<DocumentDto>(s)).ToList();
        return dtos;
    }

    public async Task<IEnumerable<DocumentDto>> GetDocumentsByModuleAsync(Guid moduleId)
    {
        var docs = await _unitOfWork.DocumentRepository.GetDocumentsByModuleAsync(moduleId);
        var dtos = docs.Select(s => _mapper.Map<DocumentDto>(s)).ToList();
        return dtos;
    }

    public async Task<IEnumerable<DocumentDto>> GetDocumentsByActivityAsync(Guid activityId)
    {
        var docs = await _unitOfWork.DocumentRepository.GetDocumentsByActivityAsync(activityId);
        var dtos = docs.Select(s => _mapper.Map<DocumentDto>(s)).ToList();
        return dtos;
    }


    public async Task<DocumentDto> UploadAsync(IFormFile file, DocumentUploadDto dto)
    {
        var relativePath = await _fileStorage.SaveFileAsync(file);

        var document = new Document
        {
            Name = dto.Name,
            Description = dto.Description,
            Link = relativePath,
            UploadDate = DateOnly.FromDateTime(DateTime.UtcNow),
            UploadedById = dto.UploadedById,
            CourseId = dto.CourseId,
            ModuleId = dto.ModuleId,
            ActivityId = dto.ActivityId
        };

        _unitOfWork.DocumentRepository.Create(document);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<DocumentDto>(document);
    }

    public async Task<FileStreamResult?> DownloadAsync(Guid documentId)
    {
        var doc = await _unitOfWork.DocumentRepository.GetEntityByIdAsync(documentId);
        if (doc == null) return null;

        var file = await _fileStorage.GetFileAsync(doc.Link, doc.Name);
        if (file == null) return null;

        var ext = Path.GetExtension(doc.Link).ToLower();
        var contentType = ext switch
        {
            ".pdf" => "application/pdf",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".txt" => "text/plain",
            _ => "application/octet-stream"
        };

        return new FileStreamResult(file.Value.Stream, contentType)
        {
            FileDownloadName = file.Value.FileName
        };
    }
}