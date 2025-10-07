using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Shared.Parameters;
public class EntityParameters
{
    const int maxPageSize = 10;
    public int PageNumber { get; set; } = 1;
    private int _pageSize { get; set; } = 5;
    public string? SearchQuery { get; set; }
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > maxPageSize ? maxPageSize : value;
    }
}

public class CourseParameters : EntityParameters
{
    public string? Name { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public CourseParameters()
    {
        PageSize = 3;
    }
}