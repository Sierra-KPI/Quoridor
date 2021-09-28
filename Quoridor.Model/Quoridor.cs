﻿namespace Quoridor.Model
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
            FirstPlayer = firstPlayer;
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

        public bool PlaceWall()
        { }

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
