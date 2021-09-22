﻿using System;

namespace Quoridor
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

		public bool MakeMove()
		{
			return true;
		}

		private void EndGame()
		{
			Console.WriteLine($"The game is over and winner is {0}!",
				CurrentPlayer);
		}

		private void SwapPlayer()
		{
			CurrentPlayer = CurrentPlayer == FirstPlayer ?
				SecondPlayer : FirstPlayer;
		}
	}
}
