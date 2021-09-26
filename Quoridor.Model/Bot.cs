namespace Quoridor.Model
{
    internal class Bot : IPlayer
    {
        public Wall[] Walls { get; private set; }
        public Position currentCell { get; private set; }
        public Position move(Position currentCell, Position start, Position end, bool HasWall) 
        {
            currentCell = start;
            if (HasWall == false)
            {
                currentCell = end;
            }
            return currentCell;
        }
        public void placeWall() { }
        public bool hasWon() { return true; }
        public Position startPosition { get; private set; }

        public Position endPosition { get; private set; }
    }
}
