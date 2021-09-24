namespace Quoridor.Model {
    public class Board {

        private Cell[,] cells;
        private Graph graph;

        public int Size => cells.GetLength(0);

        public Board(Cell[,] cells, Graph graph) {
            this.cells = cells;
            this.graph = graph;
        }

        public Cell[] getPossiblePlayersMoves(Cell cell) {
            var edges = graph.getEdgesForVertex(cell.id);
            Cell[] possibleCells = new Cell[edges.Length];
            for (int i = 0; i < edgs.Length; i++) {
                possibleCells[i] = getCellById(edgs[i]);
            }
            return possibleCells;
        }

        public void getPossibleWallsPlaces() {

        }

        Cell getCellById(int id) {
            foreach (Cell elem in cells) {
                if (elem.id == id) return elem;
            }
            return null;
        }

    }
}
