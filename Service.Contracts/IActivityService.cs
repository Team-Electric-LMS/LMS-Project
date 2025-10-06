using LMS.Shared.DTOs.ActivityDTOs;

namespace Service.Contracts;

public interface IActivityService
{
    Task<ActivityDto> GetActivityAsync(Guid id);
    Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto);
    Task UpdateActivityAsync(Guid id, UpdateActivityDto updateActivityDto);
    Task<IReadOnlyList<ActivityDto>> GetByModuleAsync(Guid moduleId, CancellationToken ct = default);
}