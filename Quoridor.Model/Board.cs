using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {
    public class Board {

        private Cell[,] cells;
        private Cell[] validPlayersSpots;
        private Cell[,] validPaths;

        public int width => cells.GetLength(0);
        public int height => cells.GetLength(1);

        public Board(Cell[,] cells, Cell[,] walls) {
            this.cells = cells;
            this.validPaths = walls;

        }

    }
}
