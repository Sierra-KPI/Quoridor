namespace Quoridor.Model
{
    interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell[] EndCells { get; }
        Cell ChangeCoordinates(int x, int y);
        bool HasWon();
    }
}
