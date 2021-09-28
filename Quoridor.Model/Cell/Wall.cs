namespace Quoridor.Model
{
    internal class Wall : IElement
    {
        private readonly Orientation _orientation;
        public Coordinates Position { get; private set; }
        public bool HasWall { get; private set; }

        public Wall(Coordinates position, Orientation orientation)
        {
            Position = position;
            _orientation = orientation;
        }
    }
}
