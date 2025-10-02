using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.ActivityDTOs
{
    public class ActivityDto
    {
        public Guid Id { get; set; } //From Entity
        public Guid ModuleId { get; set; } //From Activity 
        public Guid ActivityTypeId { get; set; } //From Activity
        public string ActivityTitle { get; set; } = null!; //From EntityEdu
        public string Description { get; set; } = null!; //From EntityEdu
        public DateOnly StartDate { get; set; } //From EntityEdu
        public DateOnly EndDate { get; set; } //From EntityEdu
        public string ActivityTypeName { get; set; } = null!; //From ActivityType

        public string Title { get; set; }
        public DateTime? DueDate { get; set; }
        public string Type { get; set; }
    }
}
