using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Model
{
    public class Board
    {
        private Graph _graph;
        private Cell[,] _cells;
        private List<Wall> _walls;
        private List<Wall> _placedWalls;

        public int Size => _cells.GetLength(0);

        public Board(Cell[,] cells, List<Wall> walls, Graph graph)
        {
            _cells = cells;
            _graph = graph;
            _walls = walls;
            _placedWalls = new List<Wall>();
        }

        public bool MakeMove(Cell from, Cell to, Cell through)
        {
            var moves = GetPossiblePlayersMoves(from, through);
            return Array.Exists(moves, element => element == to);
        }

        public bool PlaceWall(Wall wall)
        {
            // rename cell1 to cell1ID 
            var cell1 = GetIdOfCellByCoordinates(wall.Coordinates);
            var cell2 = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1, cell2);

            
            _walls = _walls.Where(elem =>
            {
                //replace to GetIdOfCellByCoordinates
                var wallCell1 = GetCellByCoordinates(elem.Coordinates);
                var wallCell2 = GetCellByCoordinates(elem.EndCoordinates);
                return (wallCell1.Id != cell1 || wallCell2.Id != cell2) &&
                (wallCell1.Id != cell1 - diff || wallCell2.Id != cell2 - diff) &&
                (wallCell1.Id != cell1 + diff || wallCell2.Id != cell2 + diff) &&
                (wallCell1.Id != cell1 || wallCell2.Id != cell1 + diff);
            }).ToList();

            //var orientation = diff == 1 ? Orientation.Horizontal : Orientation.Vertical;
            //var wall = new Wall(cell1.Coordinates, cell2.Coordinates, orientation);
            _placedWalls.Add(wall);

            return true;
        }

        int GetDiffId(int cell1, int cell2)
        {
            if (cell2 - cell1 == 1) return Size;
            else if (cell2 - cell1 == Size) return 1;
            else throw new Exception("Wrong coordinates for walls");
        }

        public bool RemoveWall(Wall wall)
        {
            // rename cell1 to cell1ID 
            var cell1 = GetIdOfCellByCoordinates(wall.Coordinates);
            var cell2 = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1, cell2);

            var from1 = cell1;
            var to1 = cell2;
            var from2 = cell1 + diff;
            var to2 = cell2 + diff;

            return _graph.RemoveEdge(from1, to1) && _graph.RemoveEdge(from2, to2);
        }

        public bool AddWall(Wall wall)
        {
            // rename cell1 to cell1ID 
            var cell1 = GetIdOfCellByCoordinates(wall.Coordinates);
            var cell2 = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1, cell2);

            var from1 = cell1;
            var to1 = cell2;
            var from2 = cell1 + diff;
            var to2 = cell2 + diff;

            return _graph.AddEdge(from1, to1) && _graph.AddEdge(from2, to2);
        }

        public bool CheckPaths(Cell from, Cell[] to)
        {
            var idOfCells = new int[to.GetLength(0)];
            for (int i = 0; i < to.GetLength(0); i++)
                idOfCells[i] = to[i].Id;
            return _graph.CheckPaths(from.Id, idOfCells);
        }

        public bool HasPath(Cell from, Cell to) => _graph.HasPath(from.Id, to.Id);

        public Cell GetStartCellForPlayer(int id)
        {
            switch (id)
            {
                case (int)PlayerID.First: return _cells[Size / 2, 0];
                case (int)PlayerID.Second: return _cells[Size / 2, Size - 1];
                default: return Cell.Default;
            }
        }

        public Cell[] GetEndCellsForPlayer(Cell cell)
        {
            var endCells = new Cell[Size];
            var endY = cell.Coordinates.Y == 0 ? Size - 1 : 0;
            for (var i = 0; i < Size; i++)
                endCells[i] = _cells[i, endY];
            return endCells;
        }

        private Cell[] CheckJump(Cell from, Cell through)
        {
            var to = new List<Cell>();
            var diffX = through.Coordinates.X - from.Coordinates.X;
            var diffY = through.Coordinates.Y - from.Coordinates.Y;
            if ((diffX == 0 || diffX == 1 || diffX == -1) &&
                (diffY == 0 || diffY == 1 || diffY == -1))
            {
                var coordinates1 = new Coordinates(from.Coordinates.X + diffX * 2,
                                                    from.Coordinates.Y + diffY * 2);
                var toCell1 = GetCellByCoordinates(coordinates1);
                var edges = _graph.GetEdgesForVertex(through.Id);
                if (Array.Exists(edges, element => element == toCell1.Id)) to.Add(toCell1);
                else
                {
                    var coordinates2 = new Coordinates(through.Coordinates.X + diffY,
                                                    through.Coordinates.Y + diffX);
                    var toCell2 = GetCellByCoordinates(coordinates2);
                    if (Array.Exists(edges, element => element == toCell2.Id)) to.Add(toCell2);

                    var coordinates3 = new Coordinates(through.Coordinates.X - diffY,
                                                    through.Coordinates.Y - diffX);
                    var toCell3 = GetCellByCoordinates(coordinates3);
                    if (Array.Exists(edges, element => element == toCell3.Id)) to.Add(toCell3);
                }
            }
            return to.ToArray();
        }

        public Cell[] GetPossiblePlayersMoves(Cell from, Cell through)
        {
            var edges = _graph.GetEdgesForVertex(from.Id);
            Cell[] possibleCells = new Cell[edges.GetLength(0)];
            for (int i = 0; i < edges.GetLength(0); i++)
                possibleCells[i] = GetCellById(edges[i]);
            var jumps = CheckJump(from, through);
            return possibleCells.Concat(jumps).ToArray();
        }

        public Wall[] GetPossibleWallsPlaces() => _walls.ToArray();

        public Wall[] GetPlacedWalls() => _placedWalls.ToArray();

        Cell GetCellById(int id) => _cells[id / Size, id % Size];

        int GetIdOfCellByCoordinates(Coordinates coordinates) => GetCellByCoordinates(coordinates).Id;

        public Cell GetCellByCoordinates(Coordinates coordinates) => _cells[coordinates.X, coordinates.Y];

        // rewrite
        public Wall GetWallByCoordinates(Coordinates coordinates, Coordinates endCoordinates)
        {
            //return _walls.Single(element => element.Coordinates == coordinates && element.EndCoordinates == endCoordinates);
            
            foreach (Wall element in _walls)
            {
                //Console.WriteLine(element.Coordinates.X + " " + element.Coordinates.Y + " " + element.EndCoordinates.X + " " + element.EndCoordinates.Y);
                if (element.Coordinates.X == coordinates.X && element.Coordinates.Y == coordinates.Y &&
                    element.EndCoordinates.X == endCoordinates.X && element.EndCoordinates.Y == endCoordinates.Y) return element;
            }
            throw new Exception("In GetWallByCoordinates");
        }
    }
}
