using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {
    public class Board {

        private Cell[,] cells;

        public Graph graph;

        public int width => cells.GetLength(0);
        public int height => cells.GetLength(1);

        public Board(Cell[,] cells, Graph graph) {
            this.cells = cells;
            this.graph = graph;

        }

        public Cell[] getPossiblePlayersMoves(Cell cell) {
            var edges = graph.getEdges(cell.id);
            Cell[] possibleCells = new Cell[edges.Length];
            for (int i = 0; i < edgs.Length; i++) {
                possibleCells[i] = getCellById(edgs[i]);
            }
            return possibleCells;
        }

        Cell getCellById(int id) {
            foreach (Cell elem in cells) {
                if (elem.id == id) return elem;
            }
            return null;
        }

    }
}
