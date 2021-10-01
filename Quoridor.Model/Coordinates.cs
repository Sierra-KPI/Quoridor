namespace Quoridor.Model
{
    class Coordinates
    {
        public int x { get; }
        public int y { get; }
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
            this.x = x;
            this.y = y;
        }
    }
}
