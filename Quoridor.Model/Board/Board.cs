namespace Quoridor.Model 
{
    internal class Board 
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
            var edges = _graph.GetEdgesForVertex(cell.Id);
            Cell[] possibleCells = new Cell[edges.GetLength(0)];
            for (int i = 0; i < edges.GetLength(0); i++) 
            {
                possibleCells[i] = GetCellById(edges[i]);
            }
            return possibleCells;
        }

        public void GetPossibleWallsPlaces() 
        {

        }

        Cell GetCellById(int id) 
        {
            foreach (Cell elem in _cells) 
                if (elem.Id == id) return elem;
            return Cell.Default;
        }

    }
}
