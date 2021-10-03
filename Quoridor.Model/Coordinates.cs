namespace Quoridor.Model
{
    public class Coordinates
    {
        public int X { get; }
        public int Y { get; }
        public int Left(int x)
        {
            return x--;
        }
        public int Right(int x)
        {
            return x++;
        }
        public int Up(int y)
        {
            return y++;
        }
        public int Down(int y)
        {
            return y--;
        }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
