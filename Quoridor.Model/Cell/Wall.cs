namespace Quoridor.Model
{
    public class Wall : IElement
    {
        public Orientation _orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Coordinates EndCoordinates { get; private set; }
        public bool HasWall { get; private set; }

        public Wall(Coordinates coordinates, Coordinates endCoordinates,
            Orientation orientation)
        {
            Coordinates = coordinates;
            EndCoordinates = endCoordinates;
            _orientation = orientation;
        }
    }
}
