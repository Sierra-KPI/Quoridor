namespace Quoridor.Model
{
	internal class Cell : IElement
	{
	    public int Id { get; private set; }
		public bool HasPlayer { get; private set; }
		public Coordinates Position { get; private set; }

		const int _defaultId = 500;
		private static readonly Coordinates _defaultPosition = new(0, 0);

		public static Cell Default => new(_defaultPosition, _defaultId)
		{
			Id = _defaultId,
			Position = _defaultPosition,
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
