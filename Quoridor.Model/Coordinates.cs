namespace Quoridor.Model
{
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }
        public int x;
        public int y;
        public Coordinates Left()
        {
            Coordinates newCoordinates = new Coordinates(x--, y);
            return newCoordinates;
        }
        public Coordinates Right()
        {
            Coordinates newCoordinates = new Coordinates(x++, y);
            return newCoordinates;
        }
        public Coordinates Up()
        {
            Coordinates newCoordinates = new Coordinates(x, y++);
            return newCoordinates;
        }
        public Coordinates Down()
        {
            Coordinates newCoordinates = new Coordinates(x, y--);
            return newCoordinates;
        }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
