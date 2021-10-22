using System;

namespace Quoridor.Model
{
    public class Bot : IPlayer
    {
        #region Properties

        public int WallsCount { get; private set; }
        public Cell CurrentCell { get; private set; }
        public Cell[] EndCells { get; private set; }

        #endregion Properties

        #region Constructor

        public Bot(Cell currentCell, Cell[] endCells)
        {
            CurrentCell = currentCell;
            WallsCount = 10;
            EndCells = endCells;
        }

        #endregion Constructor

        #region Methods

        public Cell ChangeCoordinates(Cell newCell)
        {
            CurrentCell = newCell;
            return CurrentCell;
        }

        public IElement DoMove(QuoridorGame game)
        {


            int bestScore = int.MinValue;
            int currentScore = 0;


        }

        private (IElement, int) Minimax(QuoridorGame game, int depth,
            bool isMaximisePlayer)
        {
            //if (depth == 0)
            //{
            //    return 0;
            //}
            //if (game.FirstPlayer.HasWon())
            //{
            //    return int.MinValue;
            //}
            //if (HasWon())
            //{
            //    return int.MaxValue;
            //}

            Cell[] possibleCells = game.
                CurrentBoard.GetPossiblePlayersMoves(game.SecondPlayer.CurrentCell,
                game.FirstPlayer.CurrentCell);
            Wall[] possibleWalls = game.
                CurrentBoard.GetPossibleWallsPlaces();

            IElement[] possibleMoves = new
                IElement[possibleCells.Length + possibleWalls.Length];

            possibleCells.CopyTo(possibleMoves, 0);
            possibleWalls.CopyTo(possibleMoves, possibleCells.Length);

            if (isMaximisePlayer)
            {
                int bestScore = int.MinValue;
                IElement bestPositon;

                foreach (IElement element in possibleMoves)
                {
                    (IElement elementForScore, int score) =
                        Minimax(game, depth, isMaximisePlayer,
                        possibleMoves);
                }
            }

        }

        public IElement DoRandomMove(Cell[] possibleCells,
            Wall[] possibleWalls)
        {
            var random = new Random();
            int choice = random.Next(2);
            if (choice % 2 == 0)
            {
                return ChooseRandomCell(possibleCells);
            }
            return ChooseRandomWall(possibleWalls);
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

        public void DecreaseWallCount() => WallsCount--;

        public void IncreaseWallCount() => WallsCount++;

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

        #endregion Methods
    }
}
