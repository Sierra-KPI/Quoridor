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

            var cells = new Cell[size, size];

            var id = 0;
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    var position = new Position(i, j);
                    cells[i, j] = new Cell(position);
                    cells[i, j].id = id++;

                }
            }

            var board = new Board(cells);
            return board;
        }
    }
}
