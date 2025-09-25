using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;
using LMS.Services;

namespace LMS.API.Extensions
{
    public static class CourseFeatureRegistration
    {
        public static IServiceCollection AddCourseFeature(this IServiceCollection services)
        {
            services.AddScoped<ICourseService, CourseService>();
            return services;
        }
    }
}
