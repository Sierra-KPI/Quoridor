namespace Quoridor.Model
{
	internal class Cell : IElement
	{
	    public int Id { get; private set; }
		public bool HasPlayer { get; private set; }
		public Position Position { get; set; }
		public static Cell Default => new Cell()
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

		public Cell(Position position, int id)
		{
			Position = position;
			Id = id;
		}
	}
}
