using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace LayoutRestService.IntegrationTests
{
    public class LayoutRestControllerTests
         : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public LayoutRestControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task TestGetLayouts_LayoutsExists_TeturnJsonArrayOfLayouts()
        {
            // Act
            var response = await _client.GetAsync("/layouts");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var stringResponse = await response.Content.ReadAsStringAsync();

            var obj = JToken.Parse(stringResponse);

            Assert.Equal(3, obj.Count());
            Assert.Equal("TestTV1", obj[0]["Name"]);
            Assert.Equal("TestTV2", obj[1]["Name"]);
            Assert.Equal("TestTV3", obj[2]["Name"]);
        }

        [Fact]
        public async Task TestGetLayout_LayoutExists_ReturnJsonLayout()
        {
            // Act
            var response = await _client.GetAsync("/layouts/TestTV1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var stringResponse = await response.Content.ReadAsStringAsync();

            var obj = JToken.Parse(stringResponse);

            Assert.Equal("TestTV1", obj["Name"]);
        }

        [Fact]
        public async Task TestGetLayout_LayoutDontExists_ReturnNotFound()
        {
            // Act
            var response = await _client.GetAsync("/layouts/TV0");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
