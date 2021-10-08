namespace Quoridor.Model
{
    public class BoardFactory : IBoardFactory
    {
        private readonly int _size = 9;

        public Board CreateBoard()
        {
            var cells = new Cell[_size, _size];
            var walls = new System.Collections.Generic.List<Wall>();

            var cellId = 0;
            for (var i = 0; i < _size; i++)
            {
                for (var j = 0; j < _size; j++)
                {
                    var coordinates = new Coordinates(i, j);
                    cells[i, j] = new Cell(coordinates, cellId);
                    cellId++;

                    if (i != 0 && j != _size - 1)
                    {
                        walls.Add(new Wall(coordinates.Left(),
                            coordinates, Orientation.Vertical));
                    }

                    if (j != 0 && i != _size - 1)
                    {
                        walls.Add(new Wall(coordinates.Up(),
                            coordinates, Orientation.Horizontal));
                    }
                }
            }

            var graph = new Graph(_size);
            var board = new Board(cells, walls, graph);

            return board;
        }
    }
}
