using LMS.API.Services;
using LMS.Infractructure.Data;
using LMS.Infractructure.Repositories;
using LMS.Presentation;
using LMS.Services;
using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Service.Contracts;

namespace LMS.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            //ToDo: Restrict access to your BlazorApp only!
            options.AddDefaultPolicy(policy =>
            {
                //..
                //..
                //..
            });

            //Can be used during development
            options.AddPolicy("AllowAll", p =>
               p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
        });
    }

    public static void ConfigureOpenApi(this IServiceCollection services) =>
       services.AddEndpointsApiExplorer()
               .AddSwaggerGen(setup =>
               {
                   setup.EnableAnnotations();

                   setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                   {
                       In = ParameterLocation.Header,
                       Description = "Place to add JWT with Bearer",
                       Name = "Authorization",
                       Type = SecuritySchemeType.Http,
                       Scheme = "Bearer"
                   });

                   setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new List<string>()
                        }
                   });
               });

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            opt.ReturnHttpNotAcceptable = true;
            opt.Filters.Add(new ProducesAttribute("application/json"));

        })
                .AddNewtonsoftJson()
                .AddApplicationPart(typeof(AssemblyReference).Assembly);
    }

    public static void ConfigureSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContext' not found.")));
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<IDocumentRepository, DocumentRepository>();

        services.AddScoped(provider => new Lazy<IUserRepository>(() => provider.GetRequiredService<IUserRepository>()));
        services.AddScoped(provider => new Lazy<ICourseRepository>(() => provider.GetRequiredService<ICourseRepository>()));
        services.AddScoped(provider => new Lazy<IStudentRepository>(() => provider.GetRequiredService<IStudentRepository>()));
        services.AddScoped(provider => new Lazy<IModuleRepository>(() => provider.GetRequiredService<IModuleRepository>()));
        services.AddScoped(provider => new Lazy<IActivityRepository>(() => provider.GetRequiredService<IActivityRepository>()));
        services.AddScoped(provider => new Lazy<IDocumentRepository>(() => provider.GetRequiredService<IDocumentRepository>()));

        services.AddScoped<IUnitOfWork>(provider => new UnitOfWork(
            provider.GetRequiredService<ApplicationDbContext>(),
            provider.GetRequiredService<Lazy<IUserRepository>>(),
            provider.GetRequiredService<Lazy<ICourseRepository>>(),
            provider.GetRequiredService<Lazy<IStudentRepository>>(),
            provider.GetRequiredService<Lazy<IModuleRepository>>(),
            provider.GetRequiredService<Lazy<IActivityRepository>>(),
            provider.GetRequiredService<Lazy<IDocumentRepository>>()
        ));
    }

    public static void AddServiceLayer(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped(provider => new Lazy<IAuthService>(() => provider.GetRequiredService<IAuthService>()));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped(provider => new Lazy<IUserService>(() => provider.GetRequiredService<IUserService>()));
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped(provider => new Lazy<ICourseService>(() => provider.GetRequiredService<ICourseService>()));
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped(provider => new Lazy<IStudentService>(() => provider.GetRequiredService<IStudentService>()));
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped(provider => new Lazy<IModuleService>(() => provider.GetRequiredService<IModuleService>()));
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped(provider => new Lazy<IActivityService>(() => provider.GetRequiredService<IActivityService>()));
        services.AddScoped<IDocumentService, DocumentService>();
        services.AddScoped(provider => new Lazy<IDocumentService>(() => provider.GetRequiredService<IDocumentService>()));
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped(provider => new Lazy<IFileStorageService>(() => provider.GetRequiredService<IFileStorageService>()));
    }
}
