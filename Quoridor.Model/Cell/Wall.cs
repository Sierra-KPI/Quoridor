namespace Quoridor.Model
{
    public class Wall : IElement
    {
        #region Properties

        public Orientation Orientation { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public Coordinates EndCoordinates { get; private set; }
        public bool HasWall { get; set; }

        #endregion Properties

        #region Constructor

        public Wall(Coordinates coordinates, Coordinates endCoordinates,
            Orientation orientation)
        {
            Coordinates = coordinates;
            EndCoordinates = endCoordinates;
            Orientation = orientation;
        }

        #endregion Constructor
    }
}
