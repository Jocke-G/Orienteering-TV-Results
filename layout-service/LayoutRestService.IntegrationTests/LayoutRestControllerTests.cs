using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace LayoutRestService.IntegrationTests
{
    public class LayoutRestControllerTests
         : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public LayoutRestControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task TestGetLayouts_LayoutsExists_TeturnJsonArrayOfLayouts()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/layouts");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var stringResponse = await response.Content.ReadAsStringAsync();

            var obj = JToken.Parse(stringResponse);

            Assert.Equal(2, obj.Count());
            Assert.Equal("TV1", obj[0]["Name"]);
            Assert.Equal("TV2", obj[1]["Name"]);
        }

        [Fact]
        public async Task TestGetLayout_LayoutExists_ReturnJsonLayout()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/layouts/TV1");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var stringResponse = await response.Content.ReadAsStringAsync();

            var obj = JToken.Parse(stringResponse);

            Assert.Equal("TV1", obj["Name"]);
        }

        [Fact]
        public async Task TestGetLayout_LayoutDontExists_ReturnNotFound()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/layouts/TV0");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
