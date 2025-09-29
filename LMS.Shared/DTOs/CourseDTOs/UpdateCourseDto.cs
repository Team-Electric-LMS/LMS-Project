using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.DTOs.CourseDTOs;

public record UpdateCourseDto(
    Guid Id,
    string Name,
    string Description,
    DateOnly StartDate,
    DateOnly EndDate
);