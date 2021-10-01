namespace Quoridor.Model
{
    internal class Bot : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Position Move(Position currentCell, Position start, Position end, bool HasWall) 
        {
            currentCell = start;
            if (HasWall == false)
            {
                currentCell = end;
            }
            return currentCell;
        }
        public void PlaceWall() { }
        public bool HasWon() { return true; }
        public Cell StartPosition { get; private set; }
        public Cell[] EndPosition { get; private set; }
    }
}
