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

        public IElement GetMove()
        {
            WriteWallsInfo();

            DateTime timemark = DateTime.Now;

            (Cell[] moves, Wall[] walls) = GetMovesAndWalls();

            int bestScore = int.MinValue;
            IElement step = moves[0];

            foreach (var wall in walls)
            {
                if (!_game.PlaceWallForMinimax(wall)) continue;
                //Console.WriteLine("Place Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
                int score = Minimax(1, int.MinValue, int.MaxValue, false);
                //Console.WriteLine("Unplace Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
                _game.UnplaceWall(wall);
                //Console.WriteLine("Score for wall " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y + " -> " + score);
                if (score >= bestScore)
                {
                    bestScore = score;
                    step = wall;
                }
            }

            foreach (var move in moves)
            {
                //_count = 0;
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                int score = Minimax(1, int.MinValue, int.MaxValue, false);
                _game.UnmakeMove(beforeMove);

                WriteScoreMove(move, score);

                if (score >= bestScore)
                {
                    bestScore = score;
                    step = move;
                }
                //Console.WriteLine("Count Minimax " + _count);
            }

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

        private void WriteResults(DateTime timemark, IElement step)
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

            int res2 = _game.CurrentPlayer is Bot ?
                pathLenPlayer1 : -pathLenPlayer1;
            //return res2;

            int res3 = _game.CurrentPlayer is MinimaxBot ?
                pathLenPlayer1 - pathLenPlayer2 :
                pathLenPlayer2 - pathLenPlayer1;

            return res3;
        }

        private (int, int) GetPlayersPathes()
        {
            Cell cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom = _game.CurrentPlayer.CurrentCell;

            int firstPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellFrom, cellThrough,
                _game.CurrentPlayer.EndCells);
            //int wallCountPlayer1 = _game.CurrentPlayer.WallsCount;

            _game.SwapPlayer();

            int secondPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellThrough, cellFrom,
                _game.CurrentPlayer.EndCells);
            //int wallCountPlayer2 = _game.CurrentPlayer.WallsCount;

            return (firstPlayerPathLength, secondPlayerPathLength);
        }

        private int Minimax(int depth, int alpha,
            int beta, bool maximizingPlayer)
        {
            //_count++;
            if (depth == 0 || _game.CheckGameEnd())
            {
                var res_sev = Sev();
                return res_sev;
            }

            (Cell[] moves, Wall[] walls) = GetMovesAndWalls();

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
                if (!_game.PlaceWallForMinimax(wall)) continue;
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
