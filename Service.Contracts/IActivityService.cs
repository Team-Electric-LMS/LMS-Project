using LMS.Shared.DTOs.ActivityDTOs;

namespace Service.Contracts;

public interface IActivityService
{
    Task<ActivityDto> GetActivityAsync(Guid id, CancellationToken ct = default);
    Task<ActivityDto> CreateActivityAsync(CreateActivityDto createActivityDto, CancellationToken ct = default);
    Task UpdateActivityAsync(UpdateActivityDto updateActivityDto, CancellationToken ct = default);
    Task<IEnumerable<UpdateActivityDto>> GetByModuleIdAsync(Guid moduleId, CancellationToken ct = default);
    Task<IReadOnlyList<ActivityDto>> GetByModuleAsync(Guid moduleId, CancellationToken ct = default);
}