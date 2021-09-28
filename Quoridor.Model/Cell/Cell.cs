namespace Quoridor.Model
{
	internal class Cell : IElement
	{
	    public int Id { get; private set; }
		public bool HasPlayer { get; private set; }
		public static Cell Default => new Cell()
		public Coordinates Position { get; private set; }
		{
			Id = 500,
			Position = new Position(),
			HasPlayer = false
		};

		public bool Place()
		{
			// Bool, because, maybe, it will return true, if placed correctly
			// and false, if otherwise
			HasPlayer = true;
			return true;
		}

		public Cell(Coordinates position, int id)
		{
			Position = position;
			Id = id;
		}
	}
}
