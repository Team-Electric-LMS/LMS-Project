using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using LMS.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Infractructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly UserManager<ApplicationUser> userManager;
    public UserRepository(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }
    public async Task<bool> UserExistsAsync(string id) => await userManager.FindByIdAsync(id) != null;

    public async Task<ApplicationUser?> GetUserByIdAsync(string id, bool trackChanges = false) => await userManager.FindByIdAsync(id);
    
}
