namespace Quoridor.Model
{
    internal class Player : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }
        public Cell ChangeCoordinates(int x, int y)
        {
            Coordinates coordinate = new Coordinates(x, y);
            Cell newCell = new(coordinate, 50);
            Cell currentCell = newCell;
            return currentCell;
        }
        public void DecreaseWallCounter()
        {
            WallsCount--;
        }
        public bool HasWon()
        {
            return true;
        }
        public Player(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }
    }
}
