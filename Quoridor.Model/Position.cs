namespace Quoridor.Model
{
    class Position
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
        public Position(int x, int y) { }
    }
}
