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


        

    }
}
