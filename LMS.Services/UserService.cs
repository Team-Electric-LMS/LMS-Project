using AutoMapper;
using Domain.Contracts.Repositories;
using LMS.Shared.DTOs.UserDTOs;
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
    public async Task<bool> EmailExistsAsync(string email) => await uow.UserRepository.EmailExistsAsync(email);
    public async Task<bool> UserNameExistsAsync(string username) => await uow.UserRepository.NameExistsAsync(username);

    public async Task<UserDto> GetUserByIdAsync(string id, bool trackChanges = false)
    {
        var user = await uow.UserRepository.GetUserByIdAsync(id, trackChanges);
        if (user == null) throw new($"User with id {id} was not found"); // ?? throw new UserNotFoundException(id);
        var roles = await uow.UserRepository.GetUsersRolesAsync(user);
        var dto = mapper.Map<UserDto>(user);
        dto.Role = roles.FirstOrDefault();

        return dto;
    }

    public async Task<UserDto> GetUserByIdentityName(string name, bool trackChanges = false)
    {
        var user = await uow.UserRepository.GetUserByIdentityNameAsync(name, trackChanges);
        if (user == null) return null; //throw new($"User with username {name} was not found"); // ?? throw new UserNotFoundException(id);
        var roles = await uow.UserRepository.GetUsersRolesAsync(user);
        var dto = mapper.Map<UserDto>(user);
        dto.Role = roles.FirstOrDefault();
        return dto;
    }

    public async Task<UserDto> GetUserWithCourse(string? id, string? email, bool trackChanges = false)
    {
        var user = await uow.UserRepository.GetUserWithCourseAsync(id, email, trackChanges);
        if (user == null) return null; //throw new($"User with username {name} was not found"); // ?? throw new UserNotFoundException(id);
        var roles = await uow.UserRepository.GetUsersRolesAsync(user);
        UserDto dto = roles!.Contains("Student") ? mapper.Map<StudentDto>(user) : mapper.Map<TeacherDto>(user);
        dto.Role = roles.FirstOrDefault();
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

        if (unassign)
        {
            user.CourseId = null;
            user.Course = null;
        }
        else user.CourseId = dto.CourseId;


        await uow.CompleteAsync();
    }


}

