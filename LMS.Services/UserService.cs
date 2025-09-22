using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Exceptions;
using LMS.Shared.DTOs.UserDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;


namespace LMS.Services;
public class UserService : IUserService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }
    public async Task<bool> UserExistsAsync(string id) => await uow.UserRepository.UserExistsAsync(id);

    public async Task<UserDto> GetUserByIdAsync(string id, bool includeCourse=true, bool trackChanges = false)
    {
        var user = includeCourse ? await uow.UserRepository.GetUserWithCourseAsync(id, trackChanges) : await uow.UserRepository.GetUserByIdAsync(id, trackChanges);
        if (user == null) throw new($"User with id {id} was not found"); // ?? throw new UserNotFoundException(id);

        var roles = await uow.UserRepository.GetUsersRolesAsync(user);
        
        UserDto dto = roles!.Contains("Student") ? mapper.Map<StudentDto>(user) : mapper.Map<TeacherDto>(user);
        dto.Roles  = roles.ToList();

        return dto;
    }

    public async Task<IEnumerable<UserDto>> GetStudents(Guid? id)
    {
        var students = await uow.UserRepository.GetAllOrClassmatesAsync(id);
        var dtos = students.Select(s => mapper.Map<UserDto>(s)).ToList();

        return dtos;
    }

    public async Task UpdateUserCourseAsync(string id, bool unassign, AddUserCourseByIdDto? dto, bool trackChanges = true)
    {
        var user = await uow.UserRepository.GetUserByIdAsync(id, trackChanges) ?? throw new Exception("User not found");
        if (user == null) throw new($"User with id {id} was not found");

        if (dto != null) user.CourseId = dto.CourseId;
        if (unassign)
        {
            user.CourseId = null;
            user.Course = null;
        }

        await uow.CompleteAsync();
    }


}

