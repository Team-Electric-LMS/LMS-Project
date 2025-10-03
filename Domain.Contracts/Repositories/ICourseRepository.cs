using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ICourseRepository : IRepositoryBase<Course>
    {
        Task<IEnumerable<Course>> GetAllAsync(bool trackchanges = false);
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId);
        Task<Course?> GetCourseWithStudentsAsync(Guid studentId);
        Task<Course?> GetCourseWithTeachersAsync(Guid courseId);
        Task<Course?> GetCourseByIdAsync(Guid id, bool trackChanges = false);
        Task<IEnumerable<Course>> GetActiveCoursesExtendedAsync(bool trackChanges = false);
    }
}
