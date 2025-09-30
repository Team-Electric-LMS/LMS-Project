using Domain.Contracts.Repositories;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.UserDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using Service.Contracts;
using LMS.Shared.DTOs.ActivityDTOs;

namespace LMS.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<CourseWithTeachersDto?> GetCourseForStudentAsync(Guid studentId)
        {
            var student = await unitOfWork.Students.GetStudentByIdAsync(studentId);
            if (student == null || student.CourseId == null)
            {
                return null;
            }

            var course = await unitOfWork.Courses.GetCourseWithTeachersAsync(student.CourseId.Value);
            if (course == null)
                return null;

            var courseDto = new CourseDto
            {
                Id = course.Id,
                Name = course.Name ?? string.Empty,
                Description = course.Description ?? string.Empty,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            var teachers = course.Teachers.Select(t => new TeacherDto
            {
                Id = t.Id,
                FirstName = t.FirstName ?? string.Empty,
                LastName = t.LastName ?? string.Empty,
                Email = t.Email ?? string.Empty,
                CoursesTaught = t.CoursesTaught.Select(ct => new CourseDto
                {
                    Id = ct.Id,
                    Name = ct.Name ?? string.Empty,
                    Description = ct.Description ?? string.Empty,
                    StartDate = ct.StartDate,
                    EndDate = ct.EndDate
                }).ToList()
            }).ToList();

            return new CourseWithTeachersDto
            {
                Course = courseDto,
                Teachers = teachers
            };
        }

        public async Task<IEnumerable<ModuleDto>> GetModulesForStudentAsync(Guid studentId)
        {
            var student = await unitOfWork.Students.GetStudentWithCourseAsync(studentId);
            if (student?.Course == null)
                return Enumerable.Empty<ModuleDto>();

            var modules = student.Course.Modules
                .Select(m => new ModuleDto
                {
                    Id = m.Id,
                    Name = m.Name ?? string.Empty,
                    Description = m.Description ?? string.Empty,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    CourseId = m.CourseId
                }).ToList();

            return modules;
        }

        public async Task<CourseWithModulesDto?> GetCourseWithModulesForStudentAsync(Guid studentId)
        {
            var student = await unitOfWork.Students.GetStudentWithCourseAsync(studentId);
            if (student?.Course == null)
                return null;

            var course = student.Course;
            var modules = course.Modules.Select(m => new ModuleDto
            {
                Id = m.Id,
                Name = m.Name ?? string.Empty,
                Description = m.Description ?? string.Empty,
                StartDate = m.StartDate,
                EndDate = m.EndDate,
                CourseId = m.CourseId
            }).ToList();

            return new CourseWithModulesDto
            {
                Id = course.Id,
                Name = course.Name ?? string.Empty,
                Description = course.Description ?? string.Empty,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Modules = modules
            };
        }

        public async Task<IEnumerable<ActivityDto>> GetActivitiesForModuleAsync(Guid studentId, Guid moduleId)
        {
            var student = await unitOfWork.Students.GetStudentWithCourseAsync(studentId);
            if (student?.Course == null)
                return Enumerable.Empty<ActivityDto>();

            var module = await unitOfWork.Students.GetModuleWithActivitiesAsync(moduleId);
            if (module == null)
                return Enumerable.Empty<ActivityDto>();

            var activityDtos = module.Activities.Select(activity => new ActivityDto
            {
                Id = activity.Id,
                ModuleId = activity.ModuleId,
                ActivityTypeId = activity.ActivityTypeId,
                ActivityTitle = activity.Name,
                Description = activity.Description,
                StartDate = activity.StartDate,
                EndDate = activity.EndDate,
                ActivityTypeName = activity.ActivityType.Name
            }).ToList();

            return activityDtos;
        }


        public async Task<IEnumerable<StudentDto>> GetCoursematesAsync(Guid studentId)
        {
            var student = await unitOfWork.Students.GetStudentByIdAsync(studentId);
            if (student == null || student.CourseId == null)
            {
                return Enumerable.Empty<StudentDto>();
            }

            var course = await unitOfWork.Courses.GetCourseWithStudentsAsync(student.CourseId.Value);
            if (course == null || course.Students == null)
                return Enumerable.Empty<StudentDto>();

            var coursemates = course.Students
                .Select(s => new StudentDto
                {
                    FirstName = s.FirstName ?? string.Empty,
                    LastName = s.LastName ?? string.Empty,
                    Email = s.Email ?? string.Empty,
                })
                .ToList();

            return coursemates;
        }
    }
}
