namespace Quoridor.Model
{
    internal class Cell : IElement
    {
        public int Id { get; private set; }
        public bool HasPlayer { get; private set; }
        public Coordinates Position { get; private set; }

        private const int DefaultId = 500;
        private static readonly Coordinates s_defaultPosition = new(0, 0);

        public static Cell Default => new(s_defaultPosition, DefaultId)
        {
            Id = DefaultId,
            Position = s_defaultPosition,
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
