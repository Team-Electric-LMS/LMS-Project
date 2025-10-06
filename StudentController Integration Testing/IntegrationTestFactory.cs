using LMS.API;
using LMS.Infractructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;


namespace StudentController_Integration_Testing
{
    internal class IntegrationTestFactory : WebApplicationFactory<Program>
    {
        public ApplicationDbContext Context { get; private set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var dbContextOptions = services
                    .SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (dbContextOptions != null)
                {
                    services.Remove(dbContextOptions);
                }
                    // Here you can customize the services for testing, e.g., use an in-memory database
                    services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                });
                var app = services.BuildServiceProvider();
                var scope = app.CreateScope();
                Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var list = new List<Domain.Models.Entities.Course>();
                Context.Courses.AddRange(); 
                
                
                {
                });
        }
    }
}
