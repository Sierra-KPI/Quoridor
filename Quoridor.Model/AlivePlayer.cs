namespace Quoridor.Model
{
    internal class Player : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }    
        public Cell StartPosition { get; private set; }
        public Cell[] EndPosition { get; private set; }
        public bool HasWall { get; private set; }
        public Position Move(Position currentPosition, Position startPosition, Position endPosition, bool HasWall)
        {
            currentPosition = startPosition;
            if( HasWall == false)
            {
                currentPosition = endPosition;
            }
            return currentPosition;
        }
        public void PlaceWall() 
        {
            if (HasWall == false)
            {
                HasWall = true;
                WallsCount--;
            }
        }
         public bool HasWon() 
        {
            return true;
        }        
        public Player(Cell startposition, Cell[] endPosition, Cell currentcell)
        {
            CurrentCell = currentcell;
            EndPosition = endPosition;
            WallsCount = 10;
            StartPosition = startposition;
        }
    }
}
