using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LMS.Shared.DTOs;
using LMS.Shared.DTOs.CourseDTOs;

namespace Service.Contracts
{
    public interface ICourseService
    {
        Task<IReadOnlyList<CourseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CourseDto> GetByIdAsync(Guid courseId, CancellationToken cancellationToken = default);

        // NEW/USED in teachers endpoint
        Task<IReadOnlyList<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId, CancellationToken cancellationToken = default);
    }
}
