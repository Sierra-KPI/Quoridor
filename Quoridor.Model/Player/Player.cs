namespace Quoridor.Model
{
    public class Player : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }

        public Cell ChangeCoordinates(Cell newCell)
        {
            CurrentCell = newCell;
            return CurrentCell;
        }

        public void DecreaseWallCount()
        {
            WallsCount--;
        }

        public bool HasWon()
        {
            foreach (Cell cell in EndCells)
            {
                if (CurrentCell.Coordinates == cell.Coordinates)
                {
                    return true;
                }
            }
            return false;
        }

        public Player(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }
    }
}
