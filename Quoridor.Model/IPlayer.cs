namespace Quoridor.Model
{
    interface IPlayer
    {
        Cell CurrentCell { get; }
        int WallsCount { get; }
        Position Move(Position currentCell, Position start, Position end, bool HasWall);
        void PlaceWall();
        bool HasWon();
        Cell StartPosition { get; }
        Cell[] EndPosition { get; }
    }
}
