namespace Quoridor.Model 
{
    internal class BoardFactory : IBoardFactory 
    {
        private int _size = 9;

        public Board CreateBoard() 
        {
            var cells = new Cell[_size, _size];
            var walls = new System.Collections.Generic.List<Wall>();

            var cellId = 0;
            for (var i = 0; i < _size; i++) 
            {
                for (var j = 0; j < _size; j++) 
                {
                    var сoordinate = new Coordinates(i, j);
                    cells[i, j] = new Cell(сoordinate, cellId++);

                    if (i != 0 && j != this.size - 1)
                    {
                        walls.Add(new Wall(cells[i - 1, j], cells[i, j]));
                    }

                    if (j != 0 && i != this.size - 1)
                    {
                        walls.Add(new Wall(cells[i, j - 1], cells[i, j]));
                    }
                }
            }

            var graph = new Graph(_size);
            var board = new Board(cells, walls, graph);
            return board;
        }
    }
}
