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

        public bool MakeMove(Cell from, Cell to)
        {
            var moves = GetPossiblePlayersMoves(from);
            return System.Array.Exists(moves, element => element == to);
        }

        public bool PlaceWall(Cell cell1, Cell cell2)
        {

            var from1 = cell1.Id;
            var to1 = cell2.Id;
            var from2 = cell1.Id + Size;
            var to2 = cell2.Id + Size;

            _graph.RemoveEdge(from1, to1);
            _graph.RemoveEdge(from2, to2);

            return true;
        }

        public bool HasPath(Cell from, Cell to)
        {
            return _graph.HasPath(from.Id, to.Id);
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
