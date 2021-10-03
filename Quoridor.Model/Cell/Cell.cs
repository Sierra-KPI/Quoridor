namespace Quoridor.Model
{
    public class Cell : IElement
    {
        public int Id { get; private set; }
        public IPlayer Player { get; set; }
        public Coordinates Coordinates { get; private set; }

        private const int DefaultId = 500;
        private static readonly Coordinates s_defaultCoordinates = new(0, 0);

        public static Cell Default => new(s_defaultCoordinates, DefaultId)
        {
            Id = DefaultId,
            Coordinates = s_defaultCoordinates,
        };

        public Cell(Coordinates coordinates, int id)
        {
            Coordinates = coordinates;
            Id = id;
        }
    }
}
