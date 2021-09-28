namespace Quoridor.Model
{
    internal class Cell : IElement
    {
        public int Id { get; private set; }
        public bool HasPlayer { get; private set; }
        public Coordinates Coordinate { get; private set; }

        private const int DefaultId = 500;
        private static readonly Coordinates s_defaultPosition = new(0, 0);

        public static Cell Default => new Cell()
        {
            Id = 500,
            Coordinate = new Coordinates(),
            HasPlayer = false
        };

        public Cell(Coordinates coordinate, int id)
        {
            Coordinate = coordinate;
            Id = id;
        }
    }
}
