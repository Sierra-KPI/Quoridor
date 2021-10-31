using System;

namespace Quoridor.Model
{
    public class MinimaxAlgorithm
    {
        private readonly QuoridorGame _game;
        private readonly int _timeout;
        //private int _count;

        public MinimaxAlgorithm(QuoridorGame game)
        {
            _game = game;
        }

        public IElement GetMove(Cell[] possibleSteps)
        {
            WriteWallsInfo();

            DateTime timemark = DateTime.Now;

            int bestScore = int.MinValue;
            Cell step = Cell.Default;

            foreach (var move in possibleSteps)
            {
                //_count = 0;
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                int score = Minimax(2, int.MinValue, int.MaxValue, false);
                _game.UnmakeMove(beforeMove);

                WriteScoreMove(move, score);

                if (score >= bestScore)
                {
                    bestScore = score;
                    step = move;
                }
                //Console.WriteLine("Count Minimax " + _count);
            }

            (int pathLenPlayer1, int pathLenPlayer2) = GetPlayersPathes();

            Console.WriteLine(pathLenPlayer1);
            Console.WriteLine(pathLenPlayer2);

            WriteResults(timemark, step);

            return step;
        }

        private static void WriteScoreMove(Cell move, int score)
        {
            Console.WriteLine("Score for move " +
                move.Coordinates.X + " " +
                move.Coordinates.Y + " -> " + score);
        }

        private void WriteWallsInfo()
        {
            Console.WriteLine("PlacedWallls -> " +
                _game.CurrentBoard.GetPlacedWalls().GetLength(0));
            Console.WriteLine("PossibleWallsPlaces -> " +
                _game.CurrentBoard.GetPossibleWallsPlaces().GetLength(0));
        }

        private void WriteResults(DateTime timemark, Cell step)
        {
            Console.WriteLine("Timemark for Step: " +
                (DateTime.Now - timemark));
            Console.WriteLine("Step: " + step.Coordinates.X +
                " " + step.Coordinates.Y);
            Console.WriteLine("PlacedWallls -> " +
                _game.CurrentBoard.GetPlacedWalls().GetLength(0));
            Console.WriteLine("PossibleWallsPlaces -> " +
                _game.CurrentBoard.GetPossibleWallsPlaces().GetLength(0));
        }

        private int Sev()
        {
            (int pathLenPlayer1, int pathLenPlayer2) = GetPlayersPathes();

            if (pathLenPlayer2 > pathLenPlayer1)
            {
                return 1;
            }

            return 0;
        }

        private (int, int) GetPlayersPathes()
        {
            Cell cellThrough1 = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom1 = _game.CurrentPlayer.CurrentCell;

            int firstPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellFrom1, cellThrough1,
                _game.CurrentPlayer.EndCells);
            //int wallCountPlayer1 = _game.CurrentPlayer.WallsCount;

            _game.SwapPlayer();

            Cell cellThrough2 = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom2 = _game.CurrentPlayer.CurrentCell;

            int secondPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellFrom2, cellThrough2,
                _game.CurrentPlayer.EndCells);
            //int wallCountPlayer2 = _game.CurrentPlayer.WallsCount;

            _game.SwapPlayer();

            return (firstPlayerPathLength, secondPlayerPathLength);
        }

        private int Minimax(int depth, int alpha,
            int beta, bool maximizingPlayer)
        {
            //_count++;
            if (depth == 0 || _game.CheckGameEnd())
            {
                var res_sev = Sev();
                //Console.WriteLine("SEV: " + res_sev);
                return res_sev;
            }
            //Console.WriteLine("New Minimax");

            (Cell[] moves, Wall[] walls) = GetMovesAndWalls();

            //Console.WriteLine("Possible Walls length " + walls.GetLength(0));

            int eval;
            foreach (Cell move in moves)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                //Console.WriteLine("Make move from: " + beforeMove.Coordinates.X + " " + beforeMove.Coordinates.Y +
                //    " to: " + move.Coordinates.X + " " + move.Coordinates.Y);
                _game.MakeMove(move);
                eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);
                //Console.WriteLine("Unmake move from: " + beforeMove.Coordinates.X + " " + beforeMove.Coordinates.Y +
                //    " to: " + move.Coordinates.X + " " + move.Coordinates.Y);
                _game.UnmakeMove(beforeMove);

                if (maximizingPlayer)
                {
                    alpha = Math.Max(alpha, eval);
                }
                else
                {
                    beta = Math.Min(beta, eval);
                }
                if (beta <= alpha)
                {
                    return maximizingPlayer ? alpha : beta;
                }
            }

            foreach (Wall wall in walls)
            {
                if (_game.CurrentPlayer.WallsCount == 0)
                {
                    return maximizingPlayer ? alpha : beta;
                }
                //Console.WriteLine("Place Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
                _game.PlaceWall(wall);
                eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);
                //Console.WriteLine("Unplace Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
                _game.UnplaceWall(wall);
                
                if (maximizingPlayer)
                {
                    alpha = Math.Max(alpha, eval);
                }
                else
                {
                    beta = Math.Min(beta, eval);
                }
                if (beta <= alpha)
                {
                    return maximizingPlayer ? alpha : beta;
                }
            }

            //Console.WriteLine("Return from Minimax");
            return maximizingPlayer ? alpha : beta;
        }

        private (Cell[], Wall[]) GetMovesAndWalls()
        {
            var cellFrom = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();

            var cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();

            var moves = _game.CurrentBoard.
                GetPossiblePlayersMoves(cellFrom, cellThrough);
            var walls = _game.CurrentBoard.GetPossibleWallsPlaces();

            return (moves, walls);
        }
    }
}
