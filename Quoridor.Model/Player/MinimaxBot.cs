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
            MinimaxAlgorithm minimax = new MinimaxAlgorithm(game);

            IPlayer bot = game.SecondPlayer;
            Cell[] possibleCells = game.
                    CurrentBoard.GetPossiblePlayersMoves(bot.CurrentCell,
                    game.FirstPlayer.CurrentCell);

            return minimax.GetMove();
        }

        #endregion Methods
    }
}
