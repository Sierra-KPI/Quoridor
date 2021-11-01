namespace Quoridor.Model
{
    public class MinimaxBot : Bot
    {
        #region Constructor

        public MinimaxBot(Cell currentCell, Cell[] endCells) :
            base(currentCell, endCells) {}

        #endregion Constructor

        #region Methods

        public override IElement DoMove(QuoridorGame game)
        {
            MinimaxAlgorithm minimax = new MinimaxAlgorithm(game, out Coordinates coordinate);

            IPlayer bot = game.BotPlayer;
            Cell[] possibleCells = game.
                    CurrentBoard.GetPossiblePlayersMoves(bot.CurrentCell,
                    game.FirstPlayer.CurrentCell);

            IElement moveResult = minimax.GetMove(possibleCells, out coordinate);
            coordinate = moveResult.Coordinates;
            return moveResult;
        }

        #endregion Methods
    }
}