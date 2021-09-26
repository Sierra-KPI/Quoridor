namespace Quoridor.Model
{
    class Position
    {
        public Position(int x, int y) { }

        public int left (int x)
        {
            return x--;
        }
        public int right(int x)
        {
            return x++;
        }
        public int up(int y)
        {
            return y++;
        }
        public int down(int y)
        {
            return y--;
        }
    }
}
