using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Shared.DTOs;
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
    public async Task<UserDto> GetUserByIdAsync(string id, bool trackChanges = false)
    {
        var user = await uow.UserRepository.GetUserByIdAsync(id, trackChanges); // ?? throw new MovieNotFoundException(id);
        var dto = mapper.Map<UserDto>(user);
        return dto;
    }
}
