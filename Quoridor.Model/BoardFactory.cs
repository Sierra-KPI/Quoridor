namespace Quoridor.Model 
{
    public class BoardFactory : IBoardFactory 
    {
        private int _size = 9;

        public Board CreateBoard() 
        {
            var cells = new Cell[_size, _size];

            var cellId = 0;
            for (var i = 0; i < _size; i++) 
            {
                for (var j = 0; j < _size; j++) 
                {
                    var position = new Position(i, j);
                    cells[i, j] = new Cell(position, cellId++);
                }
            }

            var graph = new Graph(_size);
            var board = new Board(cells, graph);
            return board;
        }
    }
}
