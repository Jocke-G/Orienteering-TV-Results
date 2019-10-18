using LayoutRestService.Contracts;
using LayoutRestService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LayoutRestService.Tests.Controllers
{
    public class LayoutControllerTests
    {
        [Fact]
        public void TestGetLayouts_LayoutsExists_ListOfLayouts()
        {
            var mockLogger = new Mock<ILogger<LayoutController>>();
            var controller = new LayoutController(mockLogger.Object);

            var actual = controller.Get();

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Layout>>(result.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public void TestGetLayout_LayoutExists_ReturnLayout()
        {
            var mockLogger = new Mock<ILogger<LayoutController>>();
            var controller = new LayoutController(mockLogger.Object);

            var actual = controller.GetByName("TV1");

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsType<Layout>(result.Value);
            Assert.Equal("TV1", value.Name);
        }

        [Fact]
        public void TestGetLayout_LayoutDontExists_ReturnNotFound()
        {
            var mockLogger = new Mock<ILogger<LayoutController>>();
            var controller = new LayoutController(mockLogger.Object);

            var actual = controller.GetByName("TV0");

            Assert.IsType<NotFoundResult>(actual.Result);
        }
    }
}
