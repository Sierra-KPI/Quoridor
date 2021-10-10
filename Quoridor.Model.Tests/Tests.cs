using System;
using Xunit;

namespace Quoridor.Model.Tests
{
    public class Tests
    {
        #region CheckGameEnd

        public QuoridorGame CreateGame()
        {
            Board board = new BoardFactory().CreateBoard();

            Cell firstPlayerCell = board.GetStartCellForPlayer
                ((int)PlayerID.First);
            Cell[] firstPlayerEndCells = board.GetEndCellsForPlayer
                (board.GetStartCellForPlayer((int)PlayerID.First));

            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);

            Cell secondPlayerCell = board.GetStartCellForPlayer
                ((int)PlayerID.Second);
            Cell[] secondPlayerEndCells = board.GetEndCellsForPlayer
                (board.GetStartCellForPlayer((int)PlayerID.Second));

            Player secondPlayer = new(secondPlayerCell, secondPlayerEndCells);

            QuoridorGame game = new QuoridorGame(firstPlayer, secondPlayer, board);

            return game;
        }

        [Fact]
        public void CheckGameEnd_GameIsNotEnded_False()
        {
            QuoridorGame game = CreateGame();

            Assert.False(game.CheckGameEnd());
        }

        #endregion CheckGameEnd

        #region MakeMove

        [Theory]
        [InlineData(4, 1)]
        [InlineData(3, 0)]
        [InlineData(5, 0)]
        public void MakeMove_FirstPlayerPossibleMove_True(int x, int y)
        {
            QuoridorGame game = CreateGame();

            Coordinates coordinates = new(x, y);
            Cell to = game.CurrentBoard.
                GetCellByCoordinates(coordinates);

            Assert.True(game.MakeMove(to));
        }

        [Theory]
        [InlineData(5, 1)]
        [InlineData(4, 2)]
        [InlineData(3, 3)]
        [InlineData(6, 7)]
        [InlineData(8, 9)]
        [InlineData(0, 9)]
        public void MakeMove_FirstPlayerImpossibleMove_Exception(int x, int y)
        {
            QuoridorGame game = CreateGame();

            Coordinates coordinates = new(x, y);
            Cell to = game.CurrentBoard.
                GetCellByCoordinates(coordinates);

            Assert.Throws<Exception>(() => game.MakeMove(to));
        }

        #endregion MakeMove
    }
}
