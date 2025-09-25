using LMS.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Contracts;

namespace LMS.API.Extensions
{
    public static class ModulesFeatureRegistration
    {
        /// <summary>
        /// Registers services related to the Modules feature.
        /// Call this from Program.cs.
        /// </summary>
        public static IServiceCollection AddModulesFeature(this IServiceCollection services)
        {
            services.AddScoped<IModuleService, ModuleService>();
            return services;
        }
    }
}
