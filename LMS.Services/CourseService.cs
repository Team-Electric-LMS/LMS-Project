using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.Parameters;
using Service.Contracts;

namespace LMS.Services;
// Service for managing course-related operations
public class CourseService : ICourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CourseDto> GetCourseAsync(Guid id)
    {
        var course = await _unitOfWork.Courses.GetCourseByIdAsync(id);
        return course == null ? throw new ArgumentException("Course not found") : _mapper.Map<CourseDto>(course);
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return courses.Select(c => _mapper.Map<CourseDto>(c));
    }

    // Fetch courses by teacher ID
    public async Task<IEnumerable<CourseDto>> GetCoursesByTeacherAsync(Guid teacherId)
    {
        if (teacherId == Guid.Empty)
            throw new ArgumentException("Invalid teacher ID.");
        // Ensure teacher exists, otherwise return null
        try
        {
            var courses = await _unitOfWork.Courses.GetCoursesByTeacherAsync(teacherId);
            return courses.Select(course => new CourseDto
            {
                Id = course.Id,
                Name = course.Name ?? string.Empty,
                Description = course.Description ?? string.Empty,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            });
        }
        catch (Exception ex)
        {
            // Log exceptions
            throw new ApplicationException("An error occurred while fetching courses.", ex);
        }
    }

    public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
    {
        var course = _mapper.Map<Course>(createCourseDto);
        _unitOfWork.Courses.Create(course);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<CourseDto>(course);
    }

    public async Task UpdateCourseAsync(Guid id, UpdateCourseDto updateCourseDto)
    {
        var course = await _unitOfWork.Courses.GetCourseByIdAsync(id, trackChanges: true);
        _mapper.Map(updateCourseDto, course);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<IEnumerable<CourseDto>> SearchCourseByNameAsync(string query)
    {
        var courses = await _unitOfWork.Courses.SearchCoursesByNameAsync(query);
        return courses.Select(c => _mapper.Map<CourseDto>(c));
    }
    
    public async Task<IEnumerable<CourseIdNameDto>> GetActiveCoursesExtendedAsync()
    {
        var courses = await _unitOfWork.Courses.GetActiveCoursesExtendedAsync();
        var dtos = _mapper.Map<List<CourseIdNameDto>>(courses);
        return dtos;
    }

    public async Task<PagedList<CourseDto>> GetCoursesPagedAsync(CourseParameters parameters, bool trackChanges = false)
    {
        if (parameters.StartDate.HasValue && parameters.EndDate.HasValue)
        {
            if (parameters.StartDate > parameters.EndDate)
            {
                throw new ArgumentException("Start Date cannot be later than End Date.");
            }
        }

        var courses = await _unitOfWork.Courses.GetAllPagedAsync(parameters, trackChanges);
        var dtos = courses.Select(c => _mapper.Map<CourseDto>(c)).ToList();

        return new PagedList<CourseDto>(
        dtos,
        courses.TotalCount,
        courses.CurrentPage,
        courses.PageSize);
    }
}