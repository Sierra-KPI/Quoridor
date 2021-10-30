using System;

namespace Quoridor.Model
{
    public class Minimax
    {
        private QuoridorGame _game;
        private int _timeout;
        //private int _count;

        public Minimax(QuoridorGame game)
        {
            _game = game;
        }

        public IElement GetMove(Cell[] possibleSteps)
        {
            Console.WriteLine("PlacedWallls -> " + _game.CurrentBoard.GetPlacedWalls().GetLength(0));
            Console.WriteLine("PossibleWallsPlaces -> " + _game.CurrentBoard.GetPossibleWallsPlaces().GetLength(0));
            DateTime timemark = DateTime.Now;
            int bestScore = int.MinValue;
            Cell step = Cell.Default;
            foreach (var move in possibleSteps)
            {
                //_count = 0;
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                int score = minimax(2, int.MinValue, int.MaxValue, false);
                _game.UnmakeMove(beforeMove);
                Console.WriteLine("Score for move " + move.Coordinates.X + " " + move.Coordinates.Y + " -> " + score);
                if (score >= bestScore)
                {
                    bestScore = score;
                    step = move;
                }
                //Console.WriteLine("Count minimax " + _count);
            }

            Console.WriteLine("Timemark for Step: " + (DateTime.Now - timemark));
            Console.WriteLine("Step: " + step.Coordinates.X + " " + step.Coordinates.Y);
            Console.WriteLine("PlacedWallls -> " + _game.CurrentBoard.GetPlacedWalls().GetLength(0));
            Console.WriteLine("PossibleWallsPlaces -> " + _game.CurrentBoard.GetPossibleWallsPlaces().GetLength(0));
            return step;
        }

        private int sev()
        {
            Cell cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom = _game.CurrentPlayer.CurrentCell;
            int pathLenPlayer1 = _game.CurrentBoard
                .GetMinPathLength(cellFrom, cellThrough, _game.CurrentPlayer.EndCells);
            //int wallCountPlayer1 = _game.CurrentPlayer.WallsCount;
            _game.SwapPlayer();
            int pathLenPlayer2 = _game.CurrentBoard
                .GetMinPathLength(cellFrom, cellThrough, _game.CurrentPlayer.EndCells);
            //int wallCountPlayer2 = _game.CurrentPlayer.WallsCount;

            
            int res2 = _game.CurrentPlayer is Bot ? pathLenPlayer1 : -pathLenPlayer1;
            //return res2;

            int res3 = _game.CurrentPlayer is MinimaxBot ?
                pathLenPlayer1 - pathLenPlayer2 :
                pathLenPlayer2 - pathLenPlayer1;
            return res3;

        }

        private int minimax(int depth,
            int alpha, int beta, bool maximizingPlayer)
        {
            //_count++;
            if (depth == 0 || _game.CheckGameEnd())
            {
                var res_sev = sev();
                //Console.WriteLine("SEV: " + res_sev);
                return res_sev;
            }
            //Console.WriteLine("New minimax");
            var cellFrom = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            var cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            var moves = _game.CurrentBoard.GetPossiblePlayersMoves(cellFrom, cellThrough);
            var walls = _game.CurrentBoard.GetPossibleWallsPlaces();
            //Console.WriteLine("Possible Walls length " + walls.GetLength(0));
            

            int eval;
            foreach (var move in moves)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                //Console.WriteLine("Make move from: " + beforeMove.Coordinates.X + " " + beforeMove.Coordinates.Y +
                //    " to: " + move.Coordinates.X + " " + move.Coordinates.Y);
                _game.MakeMove(move);
                eval = minimax(depth - 1, alpha, beta, !maximizingPlayer);
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
                if (beta <= alpha) return maximizingPlayer ? alpha : beta;
            }

            foreach (var wall in walls)
            {
                if (_game.CurrentPlayer.WallsCount == 0) return maximizingPlayer ? alpha : beta;
                //Console.WriteLine("Place Wall: " + wall.Coordinates.X + " " + wall.Coordinates.Y + " " + wall.EndCoordinates.X + " " + wall.EndCoordinates.Y);
                _game.PlaceWall(wall);
                eval = minimax(depth - 1, alpha, beta, !maximizingPlayer);
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
                if (beta <= alpha) return maximizingPlayer ? alpha : beta;
            }

            //Console.WriteLine("Return from minimax");
            return maximizingPlayer ? alpha : beta;

        }

    }
}
