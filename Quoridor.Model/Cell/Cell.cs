namespace Quoridor.Model
{
	internal class Cell : IElement
	{
	    public int Id { get; private set; }
		public bool HasPlayer { get; private set; }
		public Coordinates Coordinate { get; set; }
		public static Cell Default => new Cell()
		{
			Id = 500,
			Coordinate = new Coordinates(),
			HasPlayer = false
		};

		public bool Place()
		{
			// Bool, because, maybe, it will return true, if placed correctly
			// and false, if otherwise
			HasPlayer = true;
			return true;
		}

		public Cell(Coordinates coordinate, int id)
		{
			Coordinate = coordinate;
			Id = id;
		}
	}
}
