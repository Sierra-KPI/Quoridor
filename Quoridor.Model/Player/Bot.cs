using System;

namespace Quoridor.Model
{
    public class Bot : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }

        public Cell ChangeCoordinates(Cell newCell)
        {
            Cell currentCell = newCell;
            return currentCell;
        }

        // TO-DO add method with random action
        public void DoRandomMove(Cell[] possibleCells,
            Wall[] possibleWalls)
        {
            var random = new Random();
            int choice = random.Next(2);
            if (choice % 2 == 0)
            {
                ChooseRandomCell(possibleCells);
            }
            else
            {
                ChooseRandomWall(possibleWalls);
            }
        }

        public Cell ChooseRandomCell(Cell[] possibleCells)
        {
            var random = new Random();
            int index = random.Next(possibleCells.GetLength(0));
            Cell randomCell = possibleCells[index];
            Cell chosenCell = randomCell;
            return chosenCell;
        }

        public Wall ChooseRandomWall(Wall[] possibleWalls)
        {
            var random = new Random();
            int index = random.Next(possibleWalls.GetLength(0));
            Wall randomWall = possibleWalls[index];
            Wall chosenWall = randomWall;
            return chosenWall;
        }

        public void DecreaseWallCount()
        {
            WallsCount--;
        }

        public bool HasWon()
        {
            foreach (Cell cell in EndCells)
            {
                if (CurrentCell.Coordinates == cell.Coordinates)
                {
                    return true;
                }
            }
            return false;
        }

        public Bot(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }
    }
}
