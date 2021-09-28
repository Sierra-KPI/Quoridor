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
