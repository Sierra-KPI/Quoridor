namespace Quoridor.Model
{
    internal class Wall : IElement
    {
        private readonly Orientation _orientation;
        public Coordinates Position { get; private set; }
        public Coordinates EndPosition { get; private set; }
        public bool HasWall { get; private set; }

        public Wall(Coordinates position, Coordinates endPosition,
            Orientation orientation)
        {
            Position = position;
            EndPosition = endPosition;
            _orientation = orientation;
        }
    }
}
