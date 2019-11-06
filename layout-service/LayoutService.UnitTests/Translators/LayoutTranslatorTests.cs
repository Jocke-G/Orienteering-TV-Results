using LayoutService.DataAccess.Entities;
using LayoutService.Logic.Translators;
using LayoutService.Model.Contracts;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LayoutService.UnitTests.Translators
{
    public class LayoutTranslatorTests
    {
        [Fact]
        public void TestToContract()
        {
            var entity = new LayoutEntity {
                Name = "TV1",
                Rows = new List<LayoutRowEntity>
                {
                    new LayoutRowEntity
                    {
                        Ordinal = 1,
                        Cells = new List<LayoutCellEntity>
                        {
                            new LayoutCellEntity
                            {
                                Ordinal = 2,
                                CellType = "Class",
                                ClassName = "D10",
                            },
                            new LayoutCellEntity
                            {
                                Ordinal = 1,
                                CellType = "Finish",
                            },
                        },
                    },
                    new LayoutRowEntity
                    {
                        Ordinal = 2,
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

                },
            };

            var actual = entity.ToContract();

            Assert.Equal("TV1", actual.Name);
            Assert.Equal(2, actual.Rows.Count);
            Assert.Equal(2, actual.Rows[0].Cells.Count);

            Assert.Equal("Finish", actual.Rows[0].Cells[0].CellType);
            Assert.Null(actual.Rows[0].Cells[0].ClassName);

            Assert.Equal("Class", actual.Rows[0].Cells[1].CellType);
            Assert.Equal("D10", actual.Rows[0].Cells[1].ClassName);

            Assert.Equal(2, actual.Rows[1].Cells.Count);

            Assert.Equal("Class", actual.Rows[1].Cells[0].CellType);
            Assert.Equal("D10", actual.Rows[1].Cells[0].ClassName);

            Assert.Equal("Class", actual.Rows[1].Cells[1].CellType);
            Assert.Equal("D12", actual.Rows[1].Cells[1].ClassName);
        }

        [Fact]
        public void TestToEntity()
        {
            var contract = new Layout
            {
                Name = "TV1",
                Rows = new List<LayoutRow>
                {
                    new LayoutRow
                    {
                        Cells = new List<LayoutCell>
                        {
                            new LayoutCell
                            {
                                CellType = "Finish",
                            },
                            new LayoutCell
                            {
                                CellType = "Class",
                                ClassName = "D10",
                            },
                        },
                    },
                    new LayoutRow
                    {
                        Cells = new List<LayoutCell>
                        {
                            new LayoutCell
                            {
                                CellType = "Class",
                                ClassName = "D10",
                            },
                            new LayoutCell
                            {
                                CellType = "Class",
                                ClassName = "D12",
                            },
                        },
                    },

                },
            };

            var actual = contract.ToEntity();

            Assert.Equal("TV1", actual.Name);
            Assert.Equal(2, actual.Rows.Count());

            //Row 1
            Assert.Equal(1, actual.Rows.ElementAt(0).Ordinal);
            Assert.Equal(2, actual.Rows.ElementAt(0).Cells.Count());

            Assert.Equal(1, actual.Rows.ElementAt(0).Cells.ElementAt(0).Ordinal);
            Assert.Equal("Finish", actual.Rows.ElementAt(0).Cells.ElementAt(0).CellType);
            Assert.Null(actual.Rows.ElementAt(0).Cells.ElementAt(0).ClassName);

            Assert.Equal(2, actual.Rows.ElementAt(0).Cells.ElementAt(1).Ordinal);
            Assert.Equal("Class", actual.Rows.ElementAt(0).Cells.ElementAt(1).CellType);
            Assert.Equal("D10", actual.Rows.ElementAt(0).Cells.ElementAt(1).ClassName);

            //Row 2
            Assert.Equal(2, actual.Rows.ElementAt(1).Ordinal);
            Assert.Equal(2, actual.Rows.ElementAt(1).Cells.Count());

            Assert.Equal(1, actual.Rows.ElementAt(1).Cells.ElementAt(0).Ordinal);
            Assert.Equal("Class", actual.Rows.ElementAt(1).Cells.ElementAt(0).CellType);
            Assert.Equal("D10", actual.Rows.ElementAt(1).Cells.ElementAt(0).ClassName);

            Assert.Equal(2, actual.Rows.ElementAt(1).Cells.ElementAt(1).Ordinal);
            Assert.Equal("Class", actual.Rows.ElementAt(1).Cells.ElementAt(1).CellType);
            Assert.Equal("D12", actual.Rows.ElementAt(1).Cells.ElementAt(1).ClassName);
        }
    }
}
