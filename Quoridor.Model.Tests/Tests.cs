using System;
using Xunit;

namespace Quoridor.Model.Tests
{
    public class Tests
    {
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

        
    }
}
