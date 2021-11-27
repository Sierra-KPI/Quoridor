namespace Quoridor.Model
{
    public class Player : IPlayer
    {
        #region Properties

        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; set; }
        public Cell[] EndCells { get; set; }

        #endregion Properties

        #region Constructor

        public Player(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }

        #endregion Constructor

        #region Methods

        public (string, IElement) DoMove(QuoridorGame game) {
            return ("player", game.CurrentPlayer.CurrentCell);
        }

        public Cell ChangeCoordinates(Cell newCell)
        {
            CurrentCell = newCell;
            return CurrentCell;
        }

        public void DecreaseWallCount() => WallsCount--;

        public void IncreaseWallCount() => WallsCount++;

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

        #endregion Methods
    }
}
