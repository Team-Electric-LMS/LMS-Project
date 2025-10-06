using LMS.Infractructure.Data;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ForFrontEndTemplate;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace StudentController_Integration_Testing
{
    internal class IntegrationTests : IClassFixture<IntegrationTestFactory>
    {
        private readonly HttpClient client;
        private readonly ApplicationDbContext context;

        public IntegrationTests(IntegrationTestFactory factory)
        {
            var baseAddress = new Uri("https://localhost:7280/api/");
            factory.ClientOptions.BaseAddress = baseAddress;
            client = factory.CreateClient();
            context = factory.Context;
            var courses = client.GetFromJsonAsync<CourseDto>("courses");    //await for async client.getasync
                                                        //börja med att seeda ngt enkelt, bygg bara det som ska testas i db
        }

        [Fact]
        public async Task Index_ShouldRetrurnExpectedMediType3()

        {

            var expectedCount = context.Companies.Count();

            var dtos = await httpClient.GetFromJsonAsync<IEnumerable<CompanyDto>>("demo/getall");


            Assert.Equal(expectedCount, dtos?.Count());

        }
        [Fact]
        public async Task Index_ShouldRetrurnExpectedMediType()
        {
            var response = await httpClient.GetAsync("demo/dto");

            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            var dto = JsonSerializer.Deserialize<CompanyDto>(result, options);

            Assert.Equal("Working", dto?.Name);
            Assert.Equal("application/json", response.Content.Headers.ContentType!.MediaType); //kollar att fått json tillbaks
            //skapa testcontroller med endpoints?
        }

    }
}
