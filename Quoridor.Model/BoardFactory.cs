namespace Quoridor.Model {

    public interface IBoardFactory {
        Board CreateBoard();
    }

    public class BoardFactory : IBoardFactory {
        private int size = 9;

        public Board CreateBoard() {
            var cells = new Cell[size, size];
            var walls = new Cell[2 * size * (size - 1), 2];

            var idC = 0;
            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    var position = new Position(i, j);
                    cells[i, j] = new Cell(position, idC++);
                }
            }

            var graph = new Graph(size);
            var board = new Board(cells, graph);
            return board;
        }
    }
}
