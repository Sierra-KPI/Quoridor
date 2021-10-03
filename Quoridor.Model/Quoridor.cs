namespace Quoridor.Model
{
    internal class Quoridor
    {
        public IPlayer FirstPlayer { get; set; }
        public IPlayer SecondPlayer { get; set; }
        public IPlayer CurrentPlayer { get; private set; }
        public Board CurrentBoard { get; private set; }

        public Quoridor(IPlayer firstPlayer, IPlayer secondPlayer,
            Board board)
        {
            CurrentPlayer = FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            CurrentBoard = board;
        }

        public bool CheckGameEnd()
        {
            if (CurrentPlayer.HasWon())
            {
                EndGame();
                return true;
            }
            return false;
        }

        public bool MakeMove(Cell from, Cell to)
        {
            if (CurrentBoard.MakeMove(from, to))
            {
                CurrentPlayer.ChangeCoordinates(to);
                CheckGameEnd();
                SwapPlayer();
                return true;
            }
            return false;
        }

        public bool PlaceWall(Cell cell1, Cell cell2)
        {
            if (CurrentBoard.RemoveWall(cell1, cell2))
            {
                bool resPlayer1 = CurrentBoard.CheckPaths
                    (FirstPlayer.CurrentCell, FirstPlayer.EndCells);
                bool resPlayer2 = CurrentBoard.CheckPaths
                    (SecondPlayer.CurrentCell, SecondPlayer.EndCells);

                if (resPlayer1 && resPlayer2)
                {
                    if (CurrentBoard.PlaceWall(cell1, cell2))
                    {
                        CurrentPlayer.DecreaseWallCount();
                        SwapPlayer();
                        return true;
                    }
                }
                else
                {
                    CurrentBoard.AddWall(cell1, cell2);
                }
            }
            return false;
        }

        private void EndGame() => CurrentPlayer.HasWon();

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
