using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityDTOs;
using Service.Contracts;

namespace LMS.Services;
public class ActivityService : IActivityService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;

    public ActivityService(IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
    }
    public async Task<ActivityDto> GetActivityAsync(Guid id)
    {
        var activity = await uow.ActivityRepository.GetEntityByIdAsync(id, trackChanges: true);
        return activity == null ? throw new ArgumentException("Course not found") : mapper.Map<ActivityDto>(activity);
    }

    public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto)
    {
        var activity = mapper.Map<Activity>(createActivityDto);
        uow.ActivityRepository.Create(activity);
        await uow.CompleteAsync();
        return mapper.Map<ActivityDto>(activity);
    }
    public async Task UpdateActivityAsync(Guid id, UpdateActivityDto updateActivityDto)
    {
        var activity = await uow.ActivityRepository.GetEntityByIdAsync(id, trackChanges: true);
        mapper.Map(updateActivityDto, activity);
        await uow.CompleteAsync();
    }
}
