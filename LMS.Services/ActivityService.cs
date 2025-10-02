using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts.Repositories;
using LMS.Shared.DTOs.ActivityDTOs;
using Service.Contracts;

namespace LMS.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ActivityDto>> GetByModuleIdAsync(Guid moduleId, CancellationToken cancellationToken = default)
        {
            var entities = await _repo.GetByModuleIdAsync(moduleId, cancellationToken);
            return entities.Select(_mapper.Map<ActivityDto>).ToList();
        }
    }
}
