using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Model
{
    public class Board
    {
        private readonly Graph _graph;
        private readonly Cell[,] _cells;
        private List<Wall> _walls;
        private readonly List<Wall> _placedWalls;

        public int Size { get; }

        public Board(Cell[,] cells, List<Wall> walls, Graph graph)
        {
            Size = cells.GetLength(0);
            _cells = cells;
            _graph = graph;
            _walls = walls;
            _placedWalls = new List<Wall>();
        }

        public bool MakeMove(Cell cellFrom, Cell cellTo, Cell cellThrough)
        {
            Cell[] moves = GetPossiblePlayersMoves(cellFrom, cellThrough);
            return Array.Exists(moves, element => element == cellTo);
        }

        public bool PlaceWall(Wall wall)
        {
            int cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
            int cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1ID, cell2ID);

            _walls = _walls.Where(elem =>
            {
                int wallCell1 = GetIdOfCellByCoordinates(elem.Coordinates);
                int wallCell2 = GetIdOfCellByCoordinates(elem.EndCoordinates);
                return (wallCell1 != cell1ID || wallCell2 != cell2ID) &&
                (wallCell1 != cell1ID - diff || wallCell2 != cell2ID - diff) &&
                (wallCell1 != cell1ID + diff || wallCell2 != cell2ID + diff) &&
                (wallCell1 != cell1ID || wallCell2 != cell1ID + diff);
            }).ToList();

            _placedWalls.Add(wall);
            return true;
        }

        private int GetDiffId(int cell1, int cell2)
        {
            if (cell2 - cell1 == 1)
            {
                return Size;
            }
            else if (cell2 - cell1 == Size)
            {
                return 1;
            }
            else
            {
                throw new Exception("Wrong coordinates for walls");
            }
        }

        public bool RemoveWall(Wall wall)
        {
            int cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
            int cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1ID, cell2ID);

            int from1 = cell1ID;
            int to1 = cell2ID;
            int from2 = cell1ID + diff;
            int to2 = cell2ID + diff;

            return _graph.RemoveEdge(from1, to1) && _graph.RemoveEdge(from2, to2);
        }

        public bool AddWall(Wall wall)
        {
            int cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
            int cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffId(cell1ID, cell2ID);

            int from1 = cell1ID;
            int to1 = cell2ID;
            int from2 = cell1ID + diff;
            int to2 = cell2ID + diff;

            return _graph.AddEdge(from1, to1) && _graph.AddEdge(from2, to2);
        }

        public bool CheckPaths(Cell cellFrom, Cell[] cellsTo)
        {
            int[] idOfCells = new int[cellsTo.GetLength(0)];
            for (int i = 0; i < cellsTo.GetLength(0); i++)
            {
                idOfCells[i] = cellsTo[i].Id;
            }

            return _graph.CheckPaths(cellFrom.Id, idOfCells);
        }

        public bool HasPath(Cell cellFrom, Cell cellTo) =>
            _graph.HasPath(cellFrom.Id, cellTo.Id);

        public Cell GetStartCellForPlayer(int playerId)
        {
            return playerId switch
            {
                (int)PlayerID.First => _cells[Size / 2, 0],
                (int)PlayerID.Second => _cells[Size / 2, Size - 1],
                _ => Cell.Default,
            };
        }

        public Cell[] GetEndCellsForPlayer(Cell cell)
        {
            var endCells = new Cell[Size];
            var endY = cell.Coordinates.Y == 0 ? Size - 1 : 0;
            for (var i = 0; i < Size; i++)
            {
                endCells[i] = _cells[i, endY];
            }

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
                int[] edges = _graph.GetEdgesForVertex(through.Id);

                if (Array.Exists(edges, element => element == toCell1.Id))
                {
                    to.Add(toCell1);
                }
                else
                {
                    var coordinates2 = new Coordinates(through.Coordinates.X + diffY,
                                                    through.Coordinates.Y + diffX);
                    var toCell2 = GetCellByCoordinates(coordinates2);

                    if (Array.Exists(edges, element => element == toCell2.Id))
                    {
                        to.Add(toCell2);
                    }

                    var coordinates3 = new Coordinates(through.Coordinates.X - diffY,
                                                    through.Coordinates.Y - diffX);
                    var toCell3 = GetCellByCoordinates(coordinates3);

                    if (Array.Exists(edges, element => element == toCell3.Id))
                    {
                        to.Add(toCell3);
                    }
                }
            }
            return to.ToArray();
        }

        public Cell[] GetPossiblePlayersMoves(Cell from, Cell through)
        {
            int[] edges = _graph.GetEdgesForVertex(from.Id);
            var possibleCells = new Cell[edges.GetLength(0)];

            for (var i = 0; i < edges.GetLength(0); i++)
            {
                possibleCells[i] = GetCellById(edges[i]);
            }

            Cell[] jumps = CheckJump(from, through);

            return possibleCells.Concat(jumps).ToArray();
        }

        public Wall[] GetPossibleWallsPlaces() => _walls.ToArray();

        public Wall[] GetPlacedWalls() => _placedWalls.ToArray();

        private Cell GetCellById(int id) => _cells[id / Size, id % Size];

        private int GetIdOfCellByCoordinates(Coordinates coordinates) =>
            GetCellByCoordinates(coordinates).Id;

        public Cell GetCellByCoordinates(Coordinates coordinates)
        {
            if (coordinates.X < 0 || coordinates.X >= Size ||
                coordinates.Y < 0 || coordinates.Y >= Size)
            {
                return Cell.Default;
            }

            return _cells[coordinates.X, coordinates.Y];
        }

        public Wall GetWallByCoordinates(Coordinates coordinates,
            Coordinates endCoordinates) => _walls.Single(element =>
            element.Coordinates == coordinates &&
            element.EndCoordinates == endCoordinates);
    }
}
