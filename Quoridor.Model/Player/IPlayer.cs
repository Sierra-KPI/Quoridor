namespace Quoridor.Model
{
    interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell[] EndCells { get; }
        bool HasWon();
    }
}
