namespace Quoridor.Model
{
    public interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell[] EndCells { get; }
        Cell ChangeCoordinates(Cell cell);
        void DecreaseWallCount();
        bool HasWon();
    }
}
