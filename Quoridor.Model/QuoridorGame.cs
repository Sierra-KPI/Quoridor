﻿using System;

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

            if (through == to) return false;

            if (CurrentBoard.MakeMove(from, to, through))
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
            if (CurrentPlayer.WallsCount == 0) return false;

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
