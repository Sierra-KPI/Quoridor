namespace Quoridor.Model
{
    interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Cell ChangePosition(Cell currentCell, Coordinates coordinate);
        bool HasWon();
    }
}
