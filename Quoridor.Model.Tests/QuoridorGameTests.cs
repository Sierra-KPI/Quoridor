using System;
using Xunit;

namespace Quoridor.Model.Tests
{
    public class QuoridorGameTests
    {
        public QuoridorGame CreateGame()
        {
            Board board = new BoardFactory().CreateBoard();

            (Cell firstPlayerCell, Cell[] firstPlayerEndCells) =
               board.GetPlayerCells(PlayerID.First);

            Player firstPlayer = new(firstPlayerCell, firstPlayerEndCells);

            (Cell secondPlayerCell, Cell[] secondPlayerEndCells) =
                board.GetPlayerCells(PlayerID.Second);

            Player secondPlayer = new(secondPlayerCell, secondPlayerEndCells);

            QuoridorGame game = new QuoridorGame(firstPlayer, secondPlayer, board);

            return game;
        }

        #region CheckGameEnd

        [Fact]
        public void CheckGameEnd_GameIsNotEnded_False()
        {
            QuoridorGame game = CreateGame();

            Assert.False(game.CheckGameEnd());
        }

        #endregion CheckGameEnd

        #region MakeMove

        [Theory]
        [InlineData(7, 4)]
        [InlineData(8, 3)]
        [InlineData(8, 5)]
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

        #region GetStartCellForPlayer

        // maybe this should be in Board tests
        [Theory]
        [InlineData(0, 4, PlayerID.Second)]
        [InlineData(8, 4, PlayerID.First)]
        public void GetStartCellForPlayer_Player_Equals(int x,
            int y, PlayerID playerID)
        {
            Board board = new BoardFactory().CreateBoard();

            Cell expected = board.GetStartCellForPlayer
                (playerID);

            Coordinates coordinates = new(x, y);
            Cell actual = new(coordinates, 5);

            Assert.Equal(expected.Coordinates, actual.Coordinates);
        }

        #endregion GetStartCellForPlayer

        #region GetEndCellForPlayer

        // maybe this should be in Board tests
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GetEndCellForPlayer_FirstPlayer_Equals(int y)
        {
            int x = 0;
            Board board = new BoardFactory().CreateBoard();

            Cell firstPlayerCell = board.GetStartCellForPlayer
                (PlayerID.First);
            Cell[] actual =
                board.GetEndCellsForPlayer(firstPlayerCell);

            Coordinates expected = new Coordinates(x, y);

            Assert.Equal(actual[y].Coordinates, expected);
        }

        // maybe this should be in Board tests
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GetEndCellForPlayer_SecondPlayer_Equals(int y)
        {
            int x = 8;
            Board board = new BoardFactory().CreateBoard();

            Cell secondPlayerCell = board.GetStartCellForPlayer
                (PlayerID.Second);
            Cell[] actual =
                board.GetEndCellsForPlayer(secondPlayerCell);

            Coordinates expected = new Coordinates(x, y);

            Assert.Equal(actual[y].Coordinates, expected);
        }

        #endregion GetEndCellForPlayer
    }
}
