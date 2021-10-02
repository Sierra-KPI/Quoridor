using System;

namespace Quoridor.Model
{
    internal class Bot : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }
        public Cell MoveToRandomPosition(Cell[] possibleMoves)
        {
            var random = new Random();
            int index = random.Next(possibleMoves.Length);
            Cell randomCell = possibleMoves[index];
            Cell currentCell = randomCell;
            return currentCell;
        }
        public void DecreaseWallCounter()
        {
            WallsCount--;
        }
        public bool HasWon()
        {
            return true;
        }

        public Bot(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }
    }
}
