using LayoutRestService.Services;
using System.Linq;
using Xunit;

namespace LayoutRestService.Tests.Services
{
    public class LayoutServiceTests
    {
        [Fact]
        public void TestGetLayouts_LayoutsExists_ReturnLayoutList()
        {
            var target = new LayoutService();

            var actual = target.GetLayouts();

            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public void TestGetLayout_LayoutExists_ReturnLayout()
        {
            var target = new LayoutService();

            var actual = target.GetLayoutByName("TV1");

            Assert.Equal("TV1", actual.Name);
        }

        [Fact]
        public void TestGetLayout_LayoutDontExists_ReturnNull()
        {
            var target = new LayoutService();

            var actual = target.GetLayoutByName("TV0");

            Assert.Null(actual);
        }
    }
}
