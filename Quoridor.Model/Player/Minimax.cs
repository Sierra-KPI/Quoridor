using System;

namespace Quoridor.Model
{
    public class Minimax
    {
        private QuoridorGame _game;

        public Minimax(QuoridorGame game)
        {
            _game = game;
        }

        public IElement GetMove(Cell[] possibleSteps)
        {
            int bestScore = int.MinValue;
            Cell step = Cell.Default;
            foreach (var move in possibleSteps)
            {
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
            }

            Console.WriteLine("Step: " + step.Coordinates.X + " " + step.Coordinates.Y);
            return step;
        }

        private int sev()
        {
            Cell cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom = _game.CurrentPlayer.CurrentCell;
            int temp = _game.CurrentBoard
                .GetMinPathLength(cellFrom, cellThrough, _game.CurrentPlayer.EndCells);
            _game.SwapPlayer();

            int res = _game.CurrentPlayer is Bot ? temp : -temp;
            return res;
        }

        private int minimax(int depth,
            int alpha, int beta, bool maximizingPlayer)
        {
            if (depth == 0 || _game.CheckGameEnd())
            {
                return sev();
            }

            var cellFrom = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            var cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            var moves = _game.CurrentBoard.GetPossiblePlayersMoves(cellFrom, cellThrough);
            var walls = _game.CurrentBoard.GetPossibleWallsPlaces();

            int eval;
            foreach (var move in moves)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                eval = minimax(depth - 1, alpha, beta, !maximizingPlayer);
                _game.UnmakeMove(beforeMove);

                if (maximizingPlayer)
                {
                    alpha = Math.Max(alpha, eval);
                }
                else
                {
                    beta = Math.Min(beta, eval);
                }
                if (beta <= alpha) break;
            }

            // temp version of minimax for walls
            /*foreach (var wall in walls)
            {
                if (_game.CurrentPlayer.WallsCount == 0) continue;
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
                if (beta <= alpha) break;
            }*/

            return maximizingPlayer ? alpha : beta;

        }

    }
}
