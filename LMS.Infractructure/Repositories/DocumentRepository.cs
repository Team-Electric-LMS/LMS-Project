using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infractructure.Repositories;
public class DocumentRepository : RepositoryBase<Document>, IDocumentRepository
{
    private readonly ApplicationDbContext _context;

    public DocumentRepository(ApplicationDbContext context) : base(context) { }
    public async Task<Document?> GetByIdAsync(Guid id, bool trackChanges = false) => await GetEntityByIdAsync(id, trackChanges);
    public async Task<List<Document>> GetDocumentsByCourseAsync(Guid courseId, bool trackChanges = false) =>  await FindByCondition(d => d.CourseId == courseId, trackChanges).ToListAsync();
    public async Task<List<Document>> GetDocumentsByModuleAsync(Guid moduleId, bool trackChanges = false) => await FindByCondition(d => d.ModuleId == moduleId, trackChanges).ToListAsync();
    public async Task<List<Document>> GetDocumentsByActivityAsync(Guid activityId, bool trackChanges = false) => await FindByCondition(d => d.ActivityId == activityId, trackChanges).ToListAsync();
    
}
