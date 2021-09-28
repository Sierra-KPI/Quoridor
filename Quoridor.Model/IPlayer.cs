namespace Quoridor.Model
{
    interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell ChangePosition(Cell currentCell, int x, int y);
        bool HasWon();
    }
}
