using System;

namespace Quoridor.Model
{
    public class QuoridorGame
    {
        #region Properties

        public IPlayer FirstPlayer { get; set; }
        public IPlayer SecondPlayer { get; set; }
        public IPlayer CurrentPlayer { get; private set; }
        public Board CurrentBoard { get; private set; }

        private IPlayer botPlayer;
        public IPlayer BotPlayer
        {
            get
            {
                if (botPlayer == null)
                {
                    botPlayer = FirstPlayer is Bot ? FirstPlayer : SecondPlayer;
                }

                return botPlayer;
            }
        }

        #endregion Properties

        #region Constructor

        public QuoridorGame(IPlayer firstPlayer, IPlayer secondPlayer,
            Board board)
        {
            CurrentPlayer = FirstPlayer = firstPlayer;
            SecondPlayer = secondPlayer;
            CurrentBoard = board;
        }

        #endregion Constructor

        #region Methods

        public bool CheckGameEnd() => FirstPlayer.HasWon() || SecondPlayer.HasWon();

        public bool MakeMove(Cell cellTo, bool isJump = false)
        {
            var cellFrom = CurrentPlayer.CurrentCell;
            SwapPlayer();
            var cellThrough = CurrentPlayer.CurrentCell;
            SwapPlayer();

            if (cellThrough == cellTo)
            {
                throw new Exception("Cell is taken by another Player");
            }

            //if ((!isJump && CurrentBoard.MakeMove(cellFrom, cellTo, cellThrough)) ||
            //(isJump && CurrentBoard.MakeJump(cellFrom, cellTo, cellThrough)))
            if (CurrentBoard.MakeMove(cellFrom, cellTo, cellThrough))
            {
                CurrentPlayer.ChangeCoordinates(cellTo);
                CheckGameEnd();
                SwapPlayer();
                return true;
            }
            throw new Exception("Wrong Player Move");
        }

        public bool UnmakeMove(Cell cell)
        {
            SwapPlayer();
            CurrentPlayer.ChangeCoordinates(cell);
            return true;
        }

        public bool PlaceWall(Wall wall)
        {
            if (CurrentPlayer.WallsCount == 0)
            {
                throw new Exception("Current player has no walls left");
            }

            if (CurrentBoard.RemoveWall(wall))
            {
                bool Player1HasPath = CurrentBoard.CheckPaths
                    (FirstPlayer.CurrentCell, FirstPlayer.EndCells);
                bool Player2HasPath = CurrentBoard.CheckPaths
                    (SecondPlayer.CurrentCell, SecondPlayer.EndCells);

                if (Player1HasPath && Player2HasPath)
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
            throw new Exception("Wrong place for Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
        }

        //rewrite
        public bool PlaceWallForMinimax(Wall wall)
        {
            if (CurrentPlayer.WallsCount == 0)
            {
                return false;
            }

            if (CurrentBoard.RemoveWall(wall))
            {
                bool Player1HasPath = CurrentBoard.CheckPaths
                    (FirstPlayer.CurrentCell, FirstPlayer.EndCells);
                bool Player2HasPath = CurrentBoard.CheckPaths
                    (SecondPlayer.CurrentCell, SecondPlayer.EndCells);

                if (Player1HasPath && Player2HasPath)
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
            return false;
        }

        public bool UnplaceWall(Wall wall)
        {
            if (CurrentBoard.UnplaceWall(wall))
            {
                SwapPlayer();
                CurrentPlayer.IncreaseWallCount();
                return true;
            }
            else
            {
                throw new Exception("Unable to unplace the Wall");
            }
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

        #endregion Methods
    }
}
