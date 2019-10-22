using LayoutRestService.Data;
using LayoutRestService.Models;
using LayoutRestService.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LayoutRestService.UnitTests.Services
{
    public class LayoutServiceTests
    {
        private Mock<ILayoutRepository> _layoutRepositoryMock;

        public LayoutServiceTests ()
        {
            _layoutRepositoryMock = new Mock<ILayoutRepository>();
        }

        [Fact]
        public void TestGetLayouts_LayoutsExists_ReturnLayoutList()
        {
            _layoutRepositoryMock.Setup(x => x.GetAll()).Returns(
                new List<LayoutEntity> {
                    new LayoutEntity {
                        Name = "TV1",
                        Rows = new List<LayoutRowEntity>(),
                    },
                    new LayoutEntity{
                        Name = "TV2",
                        Rows = new List<LayoutRowEntity>(),
                    },
                }
            );

            var target = CreateTarget();

            var actual = target.GetLayouts();

            Assert.Equal(2, actual.Count());
        }

        [Fact]
        public void TestGetLayouts_NoLayoutsExists_ReturnEmptyList()
        {
            _layoutRepositoryMock.Setup(x => x.GetAll()).Returns(
                new List<LayoutEntity> {
                }
            );

            var target = CreateTarget();

            var actual = target.GetLayouts();

            Assert.Empty(actual);
        }

        [Fact]
        public void TestGetLayout_LayoutExists_ReturnLayout()
        {
            _layoutRepositoryMock
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns<string>(name =>
                    new LayoutEntity
                    {
                        Name = name,
                        Rows = new List<LayoutRowEntity> {
                            new LayoutRowEntity
                            {
                                Ordinal= 1,
                                Cells = new List<LayoutCellEntity>
                                {
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 1,
                                        ClassName = "D10"
                                    },
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 2,
                                        ClassName = "D12"
                                    },
                                },
                            },
                            new LayoutRowEntity
                            {
                                Ordinal= 1,
                                Cells = new List<LayoutCellEntity>
                                {
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 1,
                                        ClassName = "H10"
                                    },
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 2,
                                        ClassName = "H12"
                                    },
                                },
                            },
                        },
                    }
                );
            var target = CreateTarget();

            var actual = target.GetLayoutByName("TV1");

            Assert.Equal("TV1", actual.Name);
            Assert.Equal("D10", actual.Rows[0].Cells[0].ClassName);
            Assert.Equal("D12", actual.Rows[0].Cells[1].ClassName);
            Assert.Equal("H10", actual.Rows[1].Cells[0].ClassName);
            Assert.Equal("H12", actual.Rows[1].Cells[1].ClassName);
        }

        [Fact]
        public void TestGetLayout_LayoutDontExists_ReturnNull()
        {
            _layoutRepositoryMock.Setup(x => x.GetByName(It.IsAny<string>())).Returns<string>(name => null);
            var target = CreateTarget();

            var actual = target.GetLayoutByName("TV0");

            Assert.Null(actual);
        }

        private ILayoutService CreateTarget()
        {
            return new LayoutService(_layoutRepositoryMock.Object);
        }
    }
}
