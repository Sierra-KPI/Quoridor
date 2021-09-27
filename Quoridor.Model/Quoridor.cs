using System;

namespace Quoridor.Model
{
	internal class Quoridor
	{
		public Player FirstPlayer { get; set; }
		public Player SecondPlayer { get; set; }
		public Player CurrentPlayer { get; private set; }
		public Board CurrentBoard { get; private set; }

		public Quoridor(Player firstPlayer, Player secondPlayer,
			Board board)
		{
			FirstPlayer = firstPlayer;
			SecondPlayer = secondPlayer;
			CurrentBoard = board;
		}

		private void CheckGameEnd()
		{
			if (true)
			{
				EndGame();
			}
		}

		public bool MakeMove(int x, int y)
		{
			return true;
		}

		private void EndGame()
		{
			CurrentPlayer.HasWon();
		}

		private void SwapPlayer()
		{
			CurrentPlayer = CurrentPlayer == FirstPlayer ?
				SecondPlayer : FirstPlayer;
		}
	}
}
