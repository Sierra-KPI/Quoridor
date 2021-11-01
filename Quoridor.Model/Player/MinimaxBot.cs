namespace Quoridor.Model
{
    public class MinimaxBot : Bot
    {
        #region Constructor

        public MinimaxBot(Cell currentCell, Cell[] endCells) :
            base(currentCell, endCells) {}

        #endregion Constructor

        #region Methods

        public override IElement DoMove(QuoridorGame game, out Coordinates coordinates)
        {
            MinimaxAlgorithm minimax = new MinimaxAlgorithm(game);
            IElement moveResult = minimax.GetMove(out coordinates);
            coordinates = moveResult.Coordinates;
            return moveResult;
        }

        #endregion Methods
    }
}
