namespace Quoridor.Model
{
    public class MinimaxBot : Bot
    {
        #region Constructor

        public MinimaxBot(Cell currentCell, Cell[] endCells) :
            base(currentCell, endCells) {}

        #endregion Constructor

        #region Methods

        public override (string, IElement) DoMove(QuoridorGame game)
        {
            MinimaxAlgorithm minimax = new MinimaxAlgorithm(game);
            (string command, IElement moveResult) = minimax.GetMove();
            return (command, moveResult);
        }

        #endregion Methods
    }
}
