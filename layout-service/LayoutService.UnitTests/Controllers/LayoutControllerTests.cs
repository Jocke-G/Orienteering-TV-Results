using LayoutService.Logic.Services;
using LayoutService.Model.Contracts;
using LayoutService.Rest.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LayoutService.UnitTests.Controllers
{
    public class LayoutControllerTests
    {
        private readonly Mock<ILayoutService> _layoutServiceMock;

        public LayoutControllerTests()
        {
            _layoutServiceMock = new Mock<ILayoutService>();
        }

        [Fact]
        public void TestGetLayouts_LayoutsExists_ListOfLayouts()
        {
            _layoutServiceMock.Setup(x => x.GetLayouts()).Returns(
                new List<Layout> {
                    new Layout { Name = "TV1", },
                    new Layout{ Name = "TV1", },
                }
            );
            var target = CreateTarget();

            var actual = target.Get();

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Layout>>(result.Value);
            Assert.Equal(2, value.Count());
        }

        [Fact]
        public void TestGetLayouts_NoLayoutsExists_ReturnEmptyList()
        {
            _layoutServiceMock.Setup(x => x.GetLayouts()).Returns(
                new List<Layout> {
                }
            );
            var target = CreateTarget();

            var actual = target.Get();

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsAssignableFrom<IEnumerable<Layout>>(result.Value);
            Assert.Empty(value);
        }

        [Fact]
        public void TestGetLayout_LayoutExists_ReturnLayout()
        {
            _layoutServiceMock.Setup(x => x.GetLayoutByName(It.IsAny<string>())).Returns<string>(name => new Layout { Name = name, });
            var target = CreateTarget();

            var actual = target.GetByName("TV1");

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsType<Layout>(result.Value);
            Assert.Equal("TV1", value.Name);
        }

        [Fact]
        public void TestGetLayout_LayoutDontExists_ReturnNotFound()
        {
            _layoutServiceMock.Setup(x => x.GetLayoutByName(It.IsAny<string>())).Returns<string>(name => null);
            var target = CreateTarget();

            var actual = target.GetByName("TV0");

            Assert.IsType<NotFoundResult>(actual.Result);
        }

        [Fact]
        public void TestPutLayout_LayoutIsOk_ReturnOk()
        {
            _layoutServiceMock
                .Setup(x => x.SaveOrUpdate(It.IsAny<Layout>()))
                .Returns<Layout>(x => x);

            var target = CreateTarget();

            Layout layout = new Layout();
            var actual = target.PutByName("TV1", layout);

            var result = Assert.IsType<OkObjectResult>(actual.Result);
            var value = Assert.IsType<Layout>(result.Value);
            Assert.Equal("TV1", value.Name);
        }

        private LayoutController CreateTarget()
        {
            var mockLogger = new Mock<ILogger<LayoutController>>();
            return new LayoutController(mockLogger.Object, _layoutServiceMock.Object);
        }
    }
}
