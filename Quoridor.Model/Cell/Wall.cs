namespace Quoridor.Model
{
	internal class Wall: IElement
	{
		private readonly Orientation _orientation;
		public Coordinates Coordinate { get; private set; }
		public bool HasWall { get; private set; }

		public bool Place()
		{
			// Bool, because, maybe, it will return true, if placed correctly
			// and false, if otherwise
			HasWall = true;
			return true;
		}

		public Wall(Coordinates coordinate, Orientation orientation)
		{
			Coordinate = coordinate;
			_orientation = orientation;
		}
	}
}
