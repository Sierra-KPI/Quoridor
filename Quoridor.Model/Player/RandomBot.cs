using System;

namespace Quoridor.Model
{
    public class RandomBot : Bot
    {
        #region Constructor

        public RandomBot(Cell currentCell, Cell[] endCells) :
            base(currentCell, endCells)
        { }

        #endregion Constructor

        #region Methods

        public override (string, IElement) DoMove(QuoridorGame game)
        {
            var cellFrom = game.CurrentPlayer.CurrentCell;
            game.SwapPlayer();

            var cellThrough = game.CurrentPlayer.CurrentCell;
            game.SwapPlayer();

            var jumps = game.CurrentBoard.
                GetPossiblePlayersJumps(cellFrom, cellThrough);
            var moves = game.CurrentBoard.
                GetPossiblePlayersMoves(cellFrom, cellThrough);
            var walls = game.CurrentBoard.GetPossibleWallsPlaces();


            var random = new Random();
            int choice = random.Next(2);
            IElement moveResult;
            string command;
            if (jumps.GetLength(0) > 0)
            {
                moveResult = ChooseRandomCell(jumps);
                command = "jump";
            }
            else if (WallsCount == 0)
            {
                moveResult = ChooseRandomCell(moves);
                command = "move";
            }
            else if (choice % 2 == 0)
            {
                moveResult = ChooseRandomCell(moves);
                command = "move";
            }
            else
            {
                moveResult = ChooseRandomWall(walls);
                command = "wall";
            }

            return (command, moveResult);
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

        #endregion Methods
    }
}
