namespace Quoridor.Model
{
    internal class Player : IPlayer
    {
        public Player(Wall[] walls, Position start, Position end, Position currentPlace)
        {
            currentCell = currentPlace;
            endPosition = end;
            Walls = walls;
            startPosition = start;
        }
        public Wall[] Walls { get; private set; }
        public Position currentCell { get; private set; }    
        public Position startPosition { get; private set; }
        public Position endPosition { get; private set; }
        public bool HasWall { get; private set; }
        public Position move(Position currentPlace, Position start, Position end, bool HasWall)
        {
            currentPlace = start;
            if( HasWall == false)
            {
                currentPlace = end;
            }
            return currentPlace;
        }
        public void placeWall() 
        {
            if (HasWall == false)
            {
                HasWall = true;
            }
        }
         public bool hasWon() 
        {
            return true;
        }
    }
}
