using AutoMapper;
using Domain.Contracts.Repositories;
using Domain.Models.Entities;
using LMS.Shared.DTOs.ActivityDTOs;
using LMS.Shared.DTOs.UserDTOs;
using Service.Contracts;

namespace LMS.Services;

public class ActivityService : IActivityService
{
    private IUnitOfWork uow;
    private readonly IMapper mapper;
    private readonly IActivityRepository repo;

    public ActivityService(IActivityRepository repo, IUnitOfWork uow, IMapper mapper)
    {
        this.uow = uow;
        this.mapper = mapper;
        this.repo = repo;
    }
    public async Task<ActivityDto> GetActivityAsync(Guid id, CancellationToken ct = default)
    {
        var activity = await uow.ActivityRepository.GetEntityByIdAsync(id, trackChanges: true);
        return activity == null ? throw new ArgumentException("Course not found") : mapper.Map<ActivityDto>(activity);
    }

    public async Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto, CancellationToken ct = default)
    {
        var activity = mapper.Map<Activity>(createActivityDto);
        var type = await uow.ActivityRepository.GetTypeByNameAsync(createActivityDto.ActivityTypeName);
        if (type == null) throw new Exception($"Activity type '{createActivityDto.ActivityTypeName}' not found.");

        activity.ActivityTypeId = type.Id;

        uow.ActivityRepository.Create(activity);
        await uow.CompleteAsync();
        return mapper.Map<ActivityDto>(activity);
    }
    public async Task UpdateActivityAsync(UpdateActivityDto updateActivityDto, CancellationToken ct = default)
    {
        var activity = await uow.ActivityRepository.GetEntityByIdAsync(updateActivityDto.Id, trackChanges: true);
        mapper.Map(updateActivityDto, activity);
        
       var type = await uow.ActivityRepository.GetTypeByNameAsync(updateActivityDto.ActivityTypeName);
       activity.ActivityTypeId = type.Id;
        

        await uow.CompleteAsync();
    }
    public async Task<IEnumerable<UpdateActivityDto>> GetByModuleIdAsync(Guid moduleId, CancellationToken ct = default)
    {
        var activities = await uow.ActivityRepository.GetByModuleIdAsync(moduleId, ct);
        var dtos = activities.Select(a => mapper.Map<UpdateActivityDto>(a)).ToList();

        return dtos;
    }

    public async Task<IReadOnlyList<ActivityDto>> GetByModuleAsync(Guid moduleId, CancellationToken ct = default)
    {
        var items = await repo.GetByModuleIdAsync(moduleId, ct);
        return mapper.Map<IReadOnlyList<ActivityDto>>(items);
    }
}