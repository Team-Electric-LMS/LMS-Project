using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models.Entities;

namespace Domain.Contracts.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCoursesByTeacherAsync(Guid teacherId);
        Task<Course?> GetCourseWithTeachersAsync(Guid courseId);
    }
}
