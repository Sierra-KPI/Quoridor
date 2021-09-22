namespace Quoridor.Model
{
	internal class Cell : IElement
	{
	    public int Id { get; private set; }
		public bool HasPlayer { get; private set; }
		public Position Position { get; private set; }

		public bool Place()
		{
			// Bool, because, maybe, it will return true, if placed correctly
			// and false, if otherwise
			HasPlayer = true;
			return true;
		}

		public Cell(Position position)
		{
			Position = position;
		}
	}
}
