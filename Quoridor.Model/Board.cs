namespace Quoridor.Model 
{
    public class Board 
    {
        private Cell[,] _cells;
        private Graph _graph;

        public int Size => _cells.GetLength(0);

        public Board(Cell[,] cells, Graph graph) 
        {
            _cells = cells;
            _graph = graph;
        }

        public Cell[] GetPossiblePlayersMoves(Cell cell) 
        {
            var edges = _graph.GetEdgesForVertex(cell.id);
            Cell[] possibleCells = new Cell[edges.Length];
            for (int i = 0; i < edgs.GetLength(0); i++) 
            {
                possibleCells[i] = getCellById(edgs[i]);
            }
            return possibleCells;
        }

        public void GetPossibleWallsPlaces() 
        {

        }

        Cell GetCellById(int id) 
        {
            foreach (Cell elem in _cells) 
                if (elem.id == id) return elem;
            return Cell.Default;
        }

    }
}
