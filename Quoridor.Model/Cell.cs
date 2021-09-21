using System;

namespace Quoridor.Model
{
	internal class Cell : IElement
	{
		public bool HasPlayer { get; set; }
		public Position Position { get; set; }

		public bool Place()
		{}

		public Cell(Position position)
		{
			Position = position;
		}
	}
}
