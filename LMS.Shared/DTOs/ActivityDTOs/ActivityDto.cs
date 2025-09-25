using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.ActivityDTOs
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public Guid ActivityTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string ActivityTypeName { get; set; } = null!;
    }
}
