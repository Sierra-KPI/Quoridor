using System;
using System.Collections.Generic;
using System.Linq;

namespace Quoridor.Model
{
    public class Board
    {
        #region Properties

        public int Size { get; }

        #endregion Properties

        #region Fields

        private readonly Graph _graph;
        private readonly Cell[,] _cells;
        private List<Wall> _walls;
        private readonly List<Wall> _placedWalls;

        #endregion Fields

        #region Constructor

        public Board(Cell[,] cells, List<Wall> walls, Graph graph)
        {
            Size = cells.GetLength(0);
            _cells = cells;
            _graph = graph;
            _walls = walls;
            _placedWalls = new List<Wall>();
        }

        #endregion Constructor

        #region Methods

        public bool MakeMove(Cell cellFrom, Cell cellTo, Cell cellThrough)
        {
            //Cell[] moves = GetPossiblePlayersMoves(cellFrom, cellThrough);
            Cell[] moves = GetPossiblePlayersMovesWithJumps(cellFrom, cellThrough);
            return Array.Exists(moves, element => element == cellTo);
        }

        public bool MakeJump(Cell cellFrom, Cell cellTo, Cell cellThrough)
        {
            Cell[] moves = CheckJump(cellFrom, cellThrough);
            return Array.Exists(moves, element => element == cellTo);
        }

        public bool PlaceWall(Wall wall)
        {
            var connectedWalls = GetConnectedWalls(wall);
            _walls.RemoveAll(elem => connectedWalls.Contains(elem));
            if (!_placedWalls.Contains(wall)) _placedWalls.Add(wall);
            return true;

        }

        public bool UnplaceWall(Wall wall)
        {
            if (_placedWalls.Contains(wall))
            {
                _placedWalls.Remove(wall);
                AddWall(wall);
                var connectedWalls = GetConnectedWalls(wall);
                _walls.AddRange(connectedWalls);
                RenewPlacedWall();
                return true;
            }
            return false;
        }

        private List<Wall> GetConnectedWalls(Wall wall)
        {
            List<Wall> connectedWalls = new List<Wall>();
            int cell1ID = GetIdOfCellByCoordinates(wall.Coordinates);
            int cell2ID = GetIdOfCellByCoordinates(wall.EndCoordinates);
            int diff = GetDiffCellId(cell1ID, cell2ID);

            int[,] wallsId = new int[,]
                {
                    { cell1ID, cell2ID },
                    { cell1ID - diff, cell2ID - diff },
                    { cell1ID + diff, cell2ID + diff },
                    { cell1ID, cell1ID + diff }
                };

            for (int i = 0; i < wallsId.GetLength(0); i++)
            {
                if (!CheckCellId(wallsId[i, 0]) || !CheckCellId(wallsId[i, 1])) continue;
                Orientation orientation = wallsId[i, 0] - wallsId[i, 1] == 1 ? Orientation.Horizontal : Orientation.Vertical;
                Coordinates coordinates = GetCellById(wallsId[i, 0]).Coordinates;
                Coordinates endCoordinates = GetCellById(wallsId[i, 1]).Coordinates;
                if (!CheckCoordinatesForWall(coordinates, endCoordinates)) continue;
                Wall tempWall = GetWallByCoordinates(coordinates, endCoordinates);
                if (tempWall == null) tempWall = new Wall(coordinates, endCoordinates, orientation);
                if (!connectedWalls.Contains(tempWall)) connectedWalls.Add(tempWall);
            }
            return connectedWalls;
        }

        private void RenewPlacedWall()
        {
            foreach (Wall wall in _placedWalls.ToList())
            {
                PlaceWall(wall);
            }
        }

        private bool CheckCoordinatesForWall(Coordinates c1, Coordinates c2)
        {
            if ((c1.Y == 0 && c1.X == Size - 1) ||
                (c2.X == 0 && c2.Y == Size - 1) ||
                (c1.X == Size - 1 && c2.X == Size - 1) ||
                (c1.Y == Size - 1 && c2.Y == Size - 1))
            {
                return false;
            }
            return true;
        }

        private bool CheckCellId(int id)
        {
            if (id < 0 || id >= Size * Size)
            {
                return false;
            }
            return true;
        }

        private int GetDiffCellId(int cell1, int cell2)
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
            int diff = GetDiffCellId(cell1ID, cell2ID);

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
            int diff = GetDiffCellId(cell1ID, cell2ID);

            int from1 = cell1ID;
            int to1 = cell2ID;
            int from2 = cell1ID + diff;
            int to2 = cell2ID + diff;

            return _graph.AddEdge(from1, to1) && _graph.AddEdge(from2, to2);
        }

        public bool CheckPaths(Cell cellFrom, Cell[] cellsTo)
        {
            int[] idOfCells = new int[cellsTo.GetLength(0)];
            for (var i = 0; i < cellsTo.GetLength(0); i++)
            {
                idOfCells[i] = cellsTo[i].Id;
            }

            return _graph.CheckPaths(cellFrom.Id, idOfCells);
        }

        public bool HasPath(Cell cellFrom, Cell cellTo) =>
            _graph.HasPath(cellFrom.Id, cellTo.Id);

        // rewrite
        public int GetMinPathLength(Cell cellFrom, Cell cellThrough, Cell[] cellsTo)
        {
            int[] idOfCells = new int[cellsTo.GetLength(0)];
            for (var i = 0; i < cellsTo.GetLength(0); i++)
            {
                idOfCells[i] = cellsTo[i].Id;
            }

            return _graph.GetMinPathLength(cellFrom.Id, cellThrough.Id, idOfCells);
        }

        public Cell GetStartCellForPlayer(int playerId)
        {
            return playerId switch
            {
                (int)PlayerID.First => _cells[Size - 1, Size / 2],
                (int)PlayerID.Second => _cells[0, Size / 2],
                _ => Cell.Default,
            };
        }

        public Cell[] GetEndCellsForPlayer(Cell cell)
        {
            Cell[] endCells = new Cell[Size];

            int endX = cell.Coordinates.X == 0 ? Size - 1 : 0;
            for (var i = 0; i < Size; i++)
            {
                endCells[i] = _cells[endX, i];
            }

            return endCells;
        }

        private Cell[] CheckJump(Cell cellFrom, Cell cellThrough)
        {
            List<Cell> cellsTo = new List<Cell>();
            int diffX = cellThrough.Coordinates.X - cellFrom.Coordinates.X;
            int diffY = cellThrough.Coordinates.Y - cellFrom.Coordinates.Y;

            if ((diffX == 0 || diffX == 1 || diffX == -1) &&
                (diffY == 0 || diffY == 1 || diffY == -1))
            {
                Coordinates coordinates1 = new Coordinates
                    (cellFrom.Coordinates.X + diffX * 2,
                        cellFrom.Coordinates.Y + diffY * 2);
                Cell toCell1 = GetCellByCoordinates(coordinates1);
                int[] edges = _graph.GetEdgesForVertex(cellThrough.Id);

                if (Array.Exists(edges, element => element == toCell1.Id))
                {
                    cellsTo.Add(toCell1);
                }
                else
                {
                    Coordinates coordinates2 = new Coordinates
                        (cellThrough.Coordinates.X + diffY,
                            cellThrough.Coordinates.Y + diffX);
                    Cell toCell2 = GetCellByCoordinates(coordinates2);

                    if (Array.Exists(edges, element => element == toCell2.Id))
                    {
                        cellsTo.Add(toCell2);
                    }

                    Coordinates coordinates3 = new Coordinates
                        (cellThrough.Coordinates.X - diffY,
                            cellThrough.Coordinates.Y - diffX);
                    Cell toCell3 = GetCellByCoordinates(coordinates3);

                    if (Array.Exists(edges, element => element == toCell3.Id))
                    {
                        cellsTo.Add(toCell3);
                    }
                }
            }
            return cellsTo.ToArray();
        }

        public Cell[] GetPossiblePlayersMoves(Cell cellFrom, Cell cellThrough)
        {
            int[] edges = _graph.GetEdgesForVertex(cellFrom.Id);
            List<Cell> possibleCells = new List<Cell>();

            for (var i = 0; i < edges.GetLength(0); i++)
            {
                if (edges[i] == cellThrough.Id) continue;
                possibleCells.Add(GetCellById(edges[i]));
            }
            //return possibleCells;
            Cell[] jumps = CheckJump(cellFrom, cellThrough);
            return possibleCells.Concat(jumps).ToArray();
        }

        public Cell[] GetPossiblePlayersMovesWithJumps(Cell cellFrom, Cell cellThrough)
        {
            Cell[] possibleCells = GetPossiblePlayersMoves(cellFrom, cellThrough);
            Cell[] jumps = CheckJump(cellFrom, cellThrough);
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

    #endregion Methods
}
