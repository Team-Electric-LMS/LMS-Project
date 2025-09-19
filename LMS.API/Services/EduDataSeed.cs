using LMS.Infractructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Services;

public class EduDataSeed
{
    public static async Task InitAsync(ApplicationDbContext context)
    {
        Console.WriteLine("Seed Edu Data: Starting...");

        if (await context.Courses.AnyAsync())
        {
            Console.WriteLine("Seed Edu Data: Skip (already exists).");
            return;
        }

        var (courses, modules, activities, activityTypes) = GenerateEduData();

        await context.AddRangeAsync(activityTypes);
        await context.AddRangeAsync(courses);
        await context.AddRangeAsync(modules);
        await context.AddRangeAsync(activities);

        await context.SaveChangesAsync();
        Console.WriteLine("Seed Edu Data: Done.");
    }

    public static (IEnumerable<Course>, IEnumerable<Module>, IEnumerable<Activity>, IEnumerable<ActivityType>)
        GenerateEduData()
    {
        // === Activity Types ===
        var seminarType = new ActivityType { Id = Guid.NewGuid(), Name = "Seminar"};
        var workshopType = new ActivityType { Id = Guid.NewGuid(), Name = "Workshop"};
        var assignmentType = new ActivityType { Id = Guid.NewGuid(), Name = "Assignment"};

        var activityTypes = new List<ActivityType> { seminarType, workshopType, assignmentType };

        // We'll seed 2 demo courses with sequential dates
        var courses = new List<Course>();
        var modules = new List<Module>();
        var activities = new List<Activity>();

        var currentStart = new DateOnly(2025, 1, 20);

        // === Course 1 ===
        var course1 = new Course
        {
            Id = Guid.NewGuid(),
            Name = "Introduction to Software Engineering",
            Description = "Learn basics of software development",
            StartDate = currentStart,
            EndDate = currentStart.AddMonths(5)
        };
        courses.Add(course1);

        var module1a = new Module
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            Name = "Fundamentals",
            Description = "Programming basics and tools",
            StartDate = currentStart,
            EndDate = currentStart.AddMonths(2)
        };
        modules.Add(module1a);

        var module1b = new Module
        {
            Id = Guid.NewGuid(),
            CourseId = course1.Id,
            Name = "Advanced Topics",
            Description = "Architecture and agile processes",
            StartDate = currentStart.AddMonths(2).AddDays(1),
            EndDate = course1.EndDate
        };
        modules.Add(module1b);

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module1a.Id,
            ActivityTypeId = seminarType.Id,
            Name = "Intro Seminar",
            Description = "Kick-off seminar",
            StartDate = currentStart.AddDays(2),
            EndDate = currentStart.AddDays(2)
        });

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module1a.Id,
            ActivityTypeId = workshopType.Id,
            Name = "Git Workshop",
            Description = "Hands-on Git session",
            StartDate = currentStart.AddDays(10),
            EndDate = currentStart.AddDays(10)
        });

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module1b.Id,
            ActivityTypeId = assignmentType.Id,
            Name = "Final Project",
            Description = "Build a small app",
            StartDate = currentStart.AddMonths(4),
            EndDate = currentStart.AddMonths(5)
        });

        // Move start for next course
        currentStart = course1.EndDate.AddDays(10);

        // === Course 2 ===
        var course2 = new Course
        {
            Id = Guid.NewGuid(),
            Name = "Data Science Essentials",
            Description = "Statistics, Python, and ML basics",
            StartDate = currentStart,
            EndDate = currentStart.AddMonths(4)
        };
        courses.Add(course2);

        var module2a = new Module
        {
            Id = Guid.NewGuid(),
            CourseId = course2.Id,
            Name = "Statistics",
            Description = "Probability and data distributions",
            StartDate = currentStart,
            EndDate = currentStart.AddMonths(1)
        };
        modules.Add(module2a);

        var module2b = new Module
        {
            Id = Guid.NewGuid(),
            CourseId = course2.Id,
            Name = "Machine Learning",
            Description = "Regression and classification",
            StartDate = currentStart.AddMonths(1).AddDays(1),
            EndDate = course2.EndDate
        };
        modules.Add(module2b);

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module2a.Id,
            ActivityTypeId = seminarType.Id,
            Name = "Stats Seminar",
            Description = "Intro to probability",
            StartDate = currentStart.AddDays(5),
            EndDate = currentStart.AddDays(5)
        });

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module2b.Id,
            ActivityTypeId = workshopType.Id,
            Name = "ML Workshop",
            Description = "Hands-on ML with Python",
            StartDate = currentStart.AddMonths(2),
            EndDate = currentStart.AddMonths(2)
        });

        activities.Add(new Activity
        {
            Id = Guid.NewGuid(),
            ModuleId = module2b.Id,
            ActivityTypeId = assignmentType.Id,
            Name = "ML Project",
            Description = "Build a predictive model",
            StartDate = currentStart.AddMonths(3),
            EndDate = course2.EndDate
        });

        return (courses, modules, activities, activityTypes);
    }
}

