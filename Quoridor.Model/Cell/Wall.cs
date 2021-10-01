namespace Quoridor.Model
{
    public class Wall : IElement
    {
        public Orientation Orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Coordinates EndCoordinates { get; private set; }
        public bool HasWall { get; set; }

        public Wall(Coordinates coordinates, Coordinates endCoordinates,
            Orientation orientation)
        {
            Coordinates = coordinates;
            EndCoordinates = endCoordinates;
            Orientation = orientation;
        }
    }
}
