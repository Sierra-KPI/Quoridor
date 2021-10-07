using System;

namespace Quoridor.Model
{
    public class QuoridorGame
    {
        public IPlayer FirstPlayer { get; set; }
        public IPlayer SecondPlayer { get; set; }
        public IPlayer CurrentPlayer { get; private set; }
        public Board CurrentBoard { get; private set; }

        public QuoridorGame(IPlayer firstPlayer, IPlayer secondPlayer,
            Board board)
        {
            CurrentPlayer = FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            CurrentBoard = board;
        }

        public bool CheckGameEnd()
        {
            return CurrentPlayer.HasWon();
        }

        public bool MakeMove(Cell to)
        {
            var from = CurrentPlayer.CurrentCell;
            SwapPlayer();
            var through = CurrentPlayer.CurrentCell;
            SwapPlayer();

            if (through == to) throw new Exception("Cell is taken");

            if (CurrentBoard.MakeMove(from, to, through))
            {
                CurrentPlayer.ChangeCoordinates(to);
                CheckGameEnd();
                SwapPlayer();
                return true;
            }
            throw new Exception("Wrong Move");
        }

        public bool PlaceWall(Wall wall)
        {
            if (CurrentPlayer.WallsCount == 0) throw new Exception("Current player has no walls");

            if (CurrentBoard.RemoveWall(wall))
            {
                bool resPlayer1 = CurrentBoard.CheckPaths
                    (FirstPlayer.CurrentCell, FirstPlayer.EndCells);
                bool resPlayer2 = CurrentBoard.CheckPaths
                    (SecondPlayer.CurrentCell, SecondPlayer.EndCells);

                if (resPlayer1 && resPlayer2)
                {
                    if (CurrentBoard.PlaceWall(wall))
                    {
                        CurrentPlayer.DecreaseWallCount();
                        SwapPlayer();
                        return true;
                    }
                }
                else
                {
                    CurrentBoard.AddWall(wall);
                }
            }
            throw new Exception("Wrong Wall");
        }

        public void SwapPlayer()
        {
            if (CurrentPlayer == FirstPlayer)
            {
                CurrentPlayer = SecondPlayer;
            }
            else
            {
                CurrentPlayer = FirstPlayer;
            }
        }
    }
}
