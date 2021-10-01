namespace Quoridor.Model
{
    internal class Player : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell ChangePosition(Cell currentCell, Coordinates coordinate)
        {
            Cell newCell = new Cell(coordinate, 50);
            currentCell = newCell;
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
        public Player(Cell currentCell)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
        }
    }
}
