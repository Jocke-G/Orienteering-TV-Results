using LayoutService.DataAccess.Entities;
using LayoutService.DataAccess.RepositoryInterfaces;
using LayoutService.Logic.Services;
using LayoutService.Model.Contracts;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LayoutService.UnitTests.Services
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
                                        CellType = "Class",
                                        ClassName = "D10",
                                    },
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 2,
                                        CellType = "Class",
                                        ClassName = "D12",
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
                                        CellType = "Class",
                                        ClassName = "H10",
                                    },
                                    new LayoutCellEntity
                                    {
                                        Ordinal = 2,
                                        CellType = "Class",
                                        ClassName = "H12",
                                    },
                                },
                            },
                        },
                    }
                );
            var target = CreateTarget();

            var actual = target.GetLayoutByName("TV1");

            Assert.Equal("TV1", actual.Name);
            Assert.Equal("Class", actual.Rows[0].Cells[0].CellType);
            Assert.Equal("D10", actual.Rows[0].Cells[0].ClassName);
            Assert.Equal("Class", actual.Rows[0].Cells[1].CellType);
            Assert.Equal("D12", actual.Rows[0].Cells[1].ClassName);
            Assert.Equal("Class", actual.Rows[1].Cells[0].CellType);
            Assert.Equal("H10", actual.Rows[1].Cells[0].ClassName);
            Assert.Equal("Class", actual.Rows[1].Cells[1].CellType);
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

        [Fact]
        public void TestSaveOrUpdate_LayoutDontExists_SaveNewLayout()
        {
            _layoutRepositoryMock
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns<string>(name => null)
                .Verifiable("Did not get old entity from database");

            _layoutRepositoryMock
                .Setup(x => x.Create(It.IsAny<LayoutEntity>()))
                .Returns<LayoutEntity>(entity => entity)
                .Verifiable("Did not create entity in database");

            var target = CreateTarget();

            Layout layout = new Layout("TV2");
            var actual = target.SaveOrUpdate(layout);

            _layoutRepositoryMock.Verify();
        }

        [Fact]
        public void TestSaveOrUpdate_LayoutExists_UpdateLayout()
        {
            _layoutRepositoryMock
                .Setup(x => x.GetByName(It.IsAny<string>()))
                .Returns<string>(name => new LayoutEntity { Name = name, })
                .Verifiable("Did not get old entity from database");

            _layoutRepositoryMock
                .Setup(x => x.Update(It.IsAny<LayoutEntity>()))
                .Returns<LayoutEntity>(entity => entity)
                .Verifiable("Did not update entity in database");

            var target = CreateTarget();

            Layout layout = new Layout("TV1");

            target.SaveOrUpdate(layout);

            _layoutRepositoryMock.Verify();
        }

        private ILayoutService CreateTarget()
        {
            return new LayoutProviderService(_layoutRepositoryMock.Object);
        }
    }
}
