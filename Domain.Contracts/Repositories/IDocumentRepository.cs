using Domain.Models.Entities;
using LMS.Shared.DTOs.DocumentDTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Contracts.Repositories;
public interface IDocumentRepository : IRepositoryBase<Document>
{
    Task<Document?> GetByIdAsync(Guid id, bool trackChanges = false);
    Task<List<Document>> GetDocumentsByCourseAsync(Guid courseId, bool trackChanges = false);
    Task<List<Document>> GetDocumentsByModuleAsync(Guid moduleId, bool trackChanges = false);
    Task<List<Document>> GetDocumentsByActivityAsync(Guid activityId, bool trackChanges = false);
}