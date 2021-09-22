using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {

    public interface IBoardFactory {

        Board CreateBoard();

    }

    public class BoardFactory : IBoardFactory {

        private int size = 9;

        public Board CreateBoard() {

            var cells = new Cell[this.size, this.size];
            var walls = new Cell[2 * this.size * (this.size - 1), 2];

            var idC = 0;
            var idW = 0;
            for (int i = 0; i < this.size; i++) {
                for (int j = 0; j < this.size; j++) {
                    var position = new Position(i, j);
                    cells[i, j] = new Cell(position, idC++);

                    if (i != 0) {
                        walls[idW, 0] = cells[i - 1, j];
                        walls[idW, 1] = cells[i, j];
                        idW++;
                    }
                    if (j != 0) {
                        walls[idW, 0] = cells[i, j - 1];
                        walls[idW, 1] = cells[i, j];
                        idW++;
                    }


                }
            }

            var board = new Board(cells, walls);
            return board;
        }
    }
}
