using System;
using Xunit;

namespace Quoridor.Model.Tests
{
    public class BoardTests
    {
        #region CheckBoardFactory

        [Fact]
        public void CheckBoardFactory()
        {
            Board board = new BoardFactory().CreateBoard();
            Assert.Equal(9, board.Size);
            Assert.Equal(0, board.GetPlacedWalls().GetLength(0));
        }

        #endregion CheckBoardFactory

        #region GetPossiGetPossiblePlayersMoves

        [Fact]
        public void GetPossiGetPossiblePlayersMoves()
        {
            Board board = new BoardFactory().CreateBoard();
            Cell from = board.GetCellByCoordinates(new Coordinates(4, 0));
            Cell through = Cell.Default;
            Cell[] actual = board.GetPossiblePlayersMoves(from, through);
            Cell[] expected = new Cell[] {
                board.GetCellByCoordinates(new Coordinates(3, 0)),
            board.GetCellByCoordinates(new Coordinates(4, 1)),
            board.GetCellByCoordinates(new Coordinates(5, 0))
            };
            Assert.Equal(actual.GetLength(0), expected.GetLength(0));
            foreach (Cell elem in actual)
            {
                Assert.Contains<IElement>(elem, expected);
            }
        }

        [Fact]
        public void GetPossiGetPossiblePlayersMoves_WithJump()
        {
            Board board = new BoardFactory().CreateBoard();
            Cell from = board.GetCellByCoordinates(new Coordinates(4, 0));
            Cell through = board.GetCellByCoordinates(new Coordinates(4, 1));
            Cell[] actual = board.GetPossiblePlayersMoves(from, through);
            Cell[] expected = new Cell[] {
                board.GetCellByCoordinates(new Coordinates(3, 0)),
            board.GetCellByCoordinates(new Coordinates(4, 1)),
            board.GetCellByCoordinates(new Coordinates(4, 2)),
            board.GetCellByCoordinates(new Coordinates(5, 0))
            };
            Assert.Equal(actual.GetLength(0), expected.GetLength(0));
            foreach (Cell elem in actual)
            {
                Assert.Contains<IElement>(elem, expected);
            }
        }

        #endregion GetPossiGetPossiblePlayersMoves
    }
}
