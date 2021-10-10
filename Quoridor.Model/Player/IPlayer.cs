namespace Quoridor.Model
{
    public interface IPlayer
    {
        #region Properties

        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell[] EndCells { get; }

        #endregion Properties

        #region Methods

        Cell ChangeCoordinates(Cell cell);
        void DecreaseWallCount();
        bool HasWon();

        #endregion Methods
    }
}
