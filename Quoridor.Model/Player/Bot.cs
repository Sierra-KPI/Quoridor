using System;

namespace Quoridor.Model
{
    internal class Bot : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Coordinates[] EndPosition { get; private set; }
        public Cell ChangePosition()
        {
            var random = new Random();
            int index = random.Next(EndPosition.Length);
            int id = random.Next();
            Coordinates randomPosition = EndPosition[index];
            Cell newCell = new(randomPosition, id);
            Cell currentCell = newCell;
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

        public Bot(Cell currentCell, Coordinates[] endPosition)
        {
            CurrentCell = currentCell;
            EndPosition = endPosition;
        }
    }
}
