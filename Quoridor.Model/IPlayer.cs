namespace Quoridor.Model
{
    interface IPlayer
    {
        Position currentCell { get; }
        Wall[] Walls { get; }
        Position move(Position currentCell, Position start, Position end, bool HasWall);
        void placeWall();
        bool hasWon();
        Position startPosition { get; }
        Position endPosition { get; }
    }
}
