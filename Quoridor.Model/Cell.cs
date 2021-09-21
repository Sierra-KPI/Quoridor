using System;

namespace Quoridor.Model
{
	internal class Cell : IElement
	{
		public bool HasPlayer { get; private set; }
		public Position Position { get; set; }

		public bool Place(Position position);

		public Cell()
		{ }
	}
}
