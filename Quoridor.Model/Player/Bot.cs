using System;
using System.Collections.Generic;

namespace Quoridor.Model
{
    internal class Bot : IPlayer
    {
        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }
        public Cell ChangeCoordinates(Cell newCell)
        {
            Cell currentCell = newCell;
            return currentCell;
        }
        public Cell ChooseRandomCell(Cell[] possibleCells)
        {
            var random = new Random();
            int index = random.Next(possibleCells.Length);
            Cell randomCell = possibleCells[index];
            Cell chosenCell = randomCell;
            return chosenCell;
        }
        public Wall ChooseRandomWall(List<Wall> possibleWalls)
        {
            var random = new Random();
            int index = random.Next(possibleWalls.Count);
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
