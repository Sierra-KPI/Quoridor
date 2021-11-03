using System;

namespace Quoridor.Model
{
    public class MinimaxAlgorithm
    {
        private readonly QuoridorGame _game;

        private const string MoveCommand = "move";
        private const string JumpCommand = "jump";
        private const string WallCommand = "wall";
        private const int Depth = 1;

        public MinimaxAlgorithm(QuoridorGame game)
        {
            _game = game;
        }

        public (string, IElement) GetMove()
        {
            (Cell[] jumps, Cell[] moves, Wall[] walls) = GetMovesAndWalls();

            int bestScore = int.MinValue;
            IElement step = moves[0];
            var command = "";

            foreach (Wall wall in walls)
            {
                if (!_game.PlaceWallForMinimax(wall))
                {
                    continue;
                }

                int score = Minimax(Depth, int.MinValue, int.MaxValue, false);

                _game.UnplaceWall(wall);

                if (score >= bestScore)
                {
                    bestScore = score;
                    step = wall;
                    command = WallCommand;
                }
            }

            foreach (Cell move in moves)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                int score = Minimax(Depth, int.MinValue, int.MaxValue, false);
                _game.UnmakeMove(beforeMove);

                if (score >= bestScore)
                {
                    bestScore = score;
                    step = move;
                    command = MoveCommand;
                }
            }

            foreach (Cell jump in jumps)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(jump, true);
                int score = Minimax(Depth, int.MinValue, int.MaxValue, false);
                _game.UnmakeMove(beforeMove);

                if (score >= bestScore)
                {
                    bestScore = score;
                    step = jump;
                    command = JumpCommand;
                }
            }

            return (command, step);
        }

        private int Sev(bool maximizingPlayer)
        {
            (int pathLenPlayer1, int pathLenPlayer2) = GetPlayersPathes();
            return maximizingPlayer ?
                pathLenPlayer1 - pathLenPlayer2 :
                pathLenPlayer2 - pathLenPlayer1;
        }

        private (int, int) GetPlayersPathes()
        {
            Cell cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();
            Cell cellFrom = _game.CurrentPlayer.CurrentCell;

            int firstPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellFrom, cellThrough,
                _game.CurrentPlayer.EndCells);

            _game.SwapPlayer();
            int secondPlayerPathLength = _game.CurrentBoard
                .GetMinPathLength(cellThrough, cellFrom,
                _game.CurrentPlayer.EndCells);

            return (firstPlayerPathLength, secondPlayerPathLength);
        }

        private int Minimax(int depth, int alpha,
            int beta, bool maximizingPlayer)
        {
            if (depth == 0 || _game.CheckGameEnd())
            {
                return Sev(maximizingPlayer);
            }

            (Cell[] jumps, Cell[] moves, Wall[] walls) = GetMovesAndWalls();

            int eval;

            foreach (Cell jump in jumps)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(jump, true);
                eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);
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

            foreach (Cell move in moves)
            {
                var beforeMove = _game.CurrentPlayer.CurrentCell;
                _game.MakeMove(move);
                eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);
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

                if (!_game.PlaceWallForMinimax(wall))
                {
                    continue;
                }

                eval = Minimax(depth - 1, alpha, beta, !maximizingPlayer);

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

        private (Cell[], Cell[], Wall[]) GetMovesAndWalls()
        {
            var cellFrom = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();

            var cellThrough = _game.CurrentPlayer.CurrentCell;
            _game.SwapPlayer();

            var jumps = _game.CurrentBoard.
                GetPossiblePlayersJumps(cellFrom, cellThrough);
            var moves = _game.CurrentBoard.
                GetPossiblePlayersMoves(cellFrom, cellThrough);
            var walls = _game.CurrentBoard.GetPossibleWallsPlaces();

            return (jumps, moves, walls);
        }
    }
}
