using System;
using Bogus;
using Domain.Models.Entities;
using LMS.Infractructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LMS.API.Services;

//Add in secret.json
//{
//   "password" :  "YourSecretPasswordHere"
//}
public class DataSeedHostingService : IHostedService
{
    private readonly IServiceProvider serviceProvider;
    private readonly IConfiguration configuration;
    private readonly ILogger<DataSeedHostingService> logger;
    private UserManager<ApplicationUser> userManager = null!;
    private RoleManager<IdentityRole> roleManager = null!;
    private const string TeacherRole = "Teacher";
    private const string StudentRole = "Student";

    public DataSeedHostingService(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<DataSeedHostingService> logger)
    {
        this.serviceProvider = serviceProvider;
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (!env.IsDevelopment()) return;

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if (await context.Users.AnyAsync(cancellationToken)) return;

        userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));

        try
        {
            await AddRolesAsync([TeacherRole, StudentRole]);
            await AddDemoUsersAsync();
            await AddUsersAsync(20);
            await AssignStudentsAsync(context);
            await AddDemoCoursesAsync(context);
            logger.LogInformation("Seed complete");
        }
        catch (Exception ex)
        {
            logger.LogError($"Data seed fail with error: {ex.Message}");
            throw;
        }
    }

    private async Task AddRolesAsync(string[] rolenames)
    {
        foreach (string rolename in rolenames)
        {
            if (await roleManager.RoleExistsAsync(rolename)) continue;
            var role = new IdentityRole { Name = rolename };
            var res = await roleManager.CreateAsync(role);

            if (!res.Succeeded) throw new Exception(string.Join("\n", res.Errors));
        }
    }
    private async Task AddDemoUsersAsync()
    {
        var teacher = new ApplicationUser
        {
            UserName = "teacher@test.com",
            Email = "teacher@test.com",
            FirstName="FN",
            LastName = "LN"
        };
        
        var student = new ApplicationUser
        {
            UserName = "student@test.com",
            Email = "student@test.com",
            FirstName = "FN",
            LastName = "LN"
        };

        await AddUserToDb([teacher, student]);

        var teacherRoleResult = await userManager.AddToRoleAsync(teacher, TeacherRole);
        if (!teacherRoleResult.Succeeded) throw new Exception(string.Join("\n", teacherRoleResult.Errors));

        var studentRoleResult = await userManager.AddToRoleAsync(student, StudentRole);
        if (!studentRoleResult.Succeeded) throw new Exception(string.Join("\n", studentRoleResult.Errors));
    }

    private async Task AddUsersAsync(int nrOfUsers)
    {
        var faker = new Faker<ApplicationUser>("sv").Rules((f, e) =>
        {
            e.Email = f.Person.Email;
            e.UserName = f.Person.Email;
            e.FirstName = f.Person.FirstName;
            e.LastName = f.Person.LastName;
        });

        await AddUserToDb(faker.Generate(nrOfUsers));
    }

    private async Task AddUserToDb(IEnumerable<ApplicationUser> users)
    {
        var passWord = configuration["password"];
        ArgumentNullException.ThrowIfNull(passWord, nameof(passWord));

        foreach (var user in users)
        {
            var result = await userManager.CreateAsync(user, passWord);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
        }
    }

    private async Task AssignStudentsAsync(ApplicationDbContext context)
    {
        var allUsers = userManager.Users.ToList();

        foreach (var user in allUsers)
        {
            var roles = await userManager.GetRolesAsync(user);
            if (!roles.Contains("Student"))
            {
                await userManager.AddToRoleAsync(user, "Student");
            }

            await userManager.UpdateAsync(user);
        }
    }

/*static List<Document> GenerateDocuments(int nrOfDocuments, EntityEdu entity, ApplicationUser owner)
        {

            var documents = new List<Document>();

            var documentTypes = new List<DocumentType>
            {
                new() { Id = Guid.NewGuid(), Name = "EvaluationCriteria" },
                new() { Id = Guid.NewGuid(), Name = "CourseLiterature" },
                new() { Id = Guid.NewGuid(), Name = "ProjectDescription" }
            };

            for (int i = 0; i < nrOfDocuments; i++)
            {
                var document = new Document
                {
                    Id = Guid.NewGuid(),
                    Name = $"Document -  {entity.Name}",
                    Description = $"Description for module of module {entity.Name}",
                    Link = $"{entity.Name}.pdf",
                    DocumentType = documentTypes[i],
                    UploadedById = owner.Id
                };

                if (entity is Module)
                {
                    document.ModuleId = entity.Id;

                }
                else if (entity is Course)
                {
                    document.CourseId = entity.Id;

                }
                else if (entity is Activity)
                {
                    document.ActivityId = entity.Id;
                };

            documents.Add(document);
            }
            return documents;
        } */

    private async Task AddDemoCoursesAsync(ApplicationDbContext context)
    {
        if (await context.Courses.AnyAsync()) return;

        var teachers = await userManager.GetUsersInRoleAsync("Teacher");
        var students = await userManager.GetUsersInRoleAsync("Student");
        var allUsers = userManager.Users.ToList();
        var random = new Random();

        var activityTypes = new List<ActivityType>
    {
        new() { Id = Guid.NewGuid(), Name = "Seminar" },
        new() { Id = Guid.NewGuid(), Name = "Workshop" },
        new() { Id = Guid.NewGuid(), Name = "Assignment" }
    };


        var courses = new List<Course>();
        var modules = new List<Module>();
        var activities = new List<Activity>();
        //var demoDocuments = new List<Document>();

        var currentStart = new DateOnly(2025, 1, 20);

        

        for (int c = 1; c <= 4; c++) 
        {
            var course = new Course
            {
                Id = Guid.NewGuid(),
                Name = $"Demo Course {c}",
                Description = $"Description for course {c}",
                StartDate = currentStart,
                EndDate = currentStart.AddMonths(4)
            };

            if (teachers.Any())
            {
                var teacher = teachers[random.Next(teachers.Count)];
                course.Teachers.Add(teacher);
            }

            if (students.Any())
            {
                var assignedStudents = students.OrderBy(x => random.Next()).Take(20).ToList();
                foreach (var student in assignedStudents)
                {
                    student.CourseId = course.Id;
                    course.Students.Add(student);
                }
            }

            /*demoDocuments = GenerateDocuments(1, course, teachers[0]);

            foreach (Document doc in demoDocuments)
            {
                course.Documents.Add(doc);
            } */
            courses.Add(course);

            for (int m = 1; m <= 3; m++) 
            {
                var module = new Module
                {
                    Id = Guid.NewGuid(),
                    Name = $"Module {m} - Course {c}",
                    Description = $"Description for module {m} of course {c}",
                    StartDate = currentStart.AddMonths(m - 1),
                    EndDate = currentStart.AddMonths(m),
                    CourseId = course.Id
                };

                /*demoDocuments = GenerateDocuments(3, module, teachers[0]);

                foreach(Document doc in demoDocuments){
                    module.Documents.Add(doc);
                }
                */
                modules.Add(module);

                foreach (var type in activityTypes)
                {
                    var activity = new Activity
                    {
                        Id = Guid.NewGuid(),
                        Name = $"{type.Name} Activity - Module {m}",
                        Description = $"Demo {type.Name} for module {m} in course {c}",
                        StartDate = module.StartDate.AddDays(1),
                        EndDate = module.StartDate.AddDays(2),
                        ModuleId = module.Id,
                        ActivityTypeId = type.Id
                    };
                  //  demoDocuments = GenerateDocuments(2, activity, teachers[0]);

                   // foreach (Document doc in demoDocuments)
                    //{
                   //     activity.Documents.Add(doc);
                  //  }
                 activities.Add(activity);
                }

            }

            currentStart = course.EndDate.AddDays(10);
        }

        await context.AddRangeAsync(activityTypes);
        await context.AddRangeAsync(courses);
        await context.AddRangeAsync(modules);
        await context.AddRangeAsync(activities);

        await context.SaveChangesAsync();

        foreach (var student in students)
            await userManager.UpdateAsync(student);
    }



    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

}
