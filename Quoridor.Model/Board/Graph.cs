using System;
using System.Collections.Generic;

namespace Quoridor.Model
{
    public class Graph
    {
        #region Fields

        private readonly int _size;
        private readonly int[,] _edges;
        private LinkedList<int>[] _adjacencyList;

        #endregion Fields

        #region Constructor

        public Graph(int size)
        {
            _size = size * size;
            _edges = new int[2 * size * (size - 1), 2];

            int edgeId = 0;
            for (var v = 0; v < _size; v++)
            {
                if (v - 1 >= 0 && (v % size != 0))
                {
                    _edges[edgeId, 0] = v - 1;
                    _edges[edgeId, 1] = v;
                    edgeId++;
                }
                if (v - size >= 0)
                {
                    _edges[edgeId, 0] = v - size;
                    _edges[edgeId, 1] = v;
                    edgeId++;
                }
            }
            MakeAdjacencyList();
        }

        #endregion Constructor

        #region Methods

        private void MakeAdjacencyList()
        {
            _adjacencyList = new LinkedList<int>[_size];

            for (int i = 0; i < _adjacencyList.GetLength(0); ++i)
            {
                _adjacencyList[i] = new LinkedList<int>();
            }

            for (int i = 0; i < _edges.GetLength(0); i++)
            {
                AddEdge(_edges[i, 0], _edges[i, 1]);
            }
        }

        public bool AddEdge(int vertex1, int vertex2)
        {
            if (_adjacencyList[vertex1].AddLast(vertex2) != null
                && _adjacencyList[vertex2].AddLast(vertex1) != null)
            {
                return true;
            }
            return false;
        }

        public bool RemoveEdge(int vertex1, int vertex2) =>
            _adjacencyList[vertex1].Remove(vertex2) &&
            _adjacencyList[vertex2].Remove(vertex1);

        private bool HasEdge(int vertex1, int vertex2) =>
            _adjacencyList[vertex1].Contains(vertex2);

        public int[] GetEdgesForVertex(int vertex)
        {
            LinkedList<int> list = _adjacencyList[vertex];
            int[] edgs = new int[list.Count];
            list.CopyTo(edgs, 0);
            return edgs;
        }

        public bool CheckPaths(int from, int[] to)
        {
            var dists = DijkstraAlgorithm(from);
            for (int i = 0; i < to.GetLength(0); i++)
            {
                if (dists[to[i]] != int.MaxValue)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasPath(int from, int to)
        {
            var dists = DijkstraAlgorithm(from);
            return dists[to] != int.MaxValue;
        }

        private int[] DijkstraAlgorithm(int startVertex)
        {
            int[] distances = new int[_size];
            bool[] used = new bool[_size];

            for (int i = 0; i < _size; i++)
            {
                distances[i] = int.MaxValue;
            }

            distances[startVertex] = 0;
            for (var i = 0; i < _size; i++)
            {
                int curr = -1;
                for (var j = 0; j < _size; j++)
                {
                    if (!used[j] && (curr == -1 || distances[j] < distances[curr]))
                    {
                        curr = j;
                    }
                }

                if (distances[curr] == int.MaxValue)
                {
                    break;
                }

                used[curr] = true;

                LinkedList<int> list = _adjacencyList[curr];
                foreach (int vertex in list)
                {
                    if (distances[curr] + 1 < distances[vertex])
                    {
                        distances[vertex] = distances[curr] + 1;
                    }
                }
            }
            return distances;
        }

        public int GetMinPathLength(int from, int through, int[] to)
        {
            int size = (int)Math.Sqrt(_size);
            // add jumps
            int diff = through - from;
            int revertDiff = Math.Abs(diff) == 1 ? -diff * size : -diff / size;
            if (Math.Abs(diff) == 1 || Math.Abs(diff) == size)
            {
                if (HasEdge(through, through + diff))
                {
                    AddEdge(from, through + diff);
                }
                else
                {

                    if (HasEdge(through, through + revertDiff))
                    {
                        AddEdge(from, through + revertDiff);
                    }
                    if (HasEdge(through, through - revertDiff))
                    {
                        AddEdge(from, through - revertDiff);
                    }
                }
            }

            int min = int.MaxValue;
            var dists = DijkstraAlgorithm(from);
            for (int i = 0; i < to.GetLength(0); i++)
            {
                // rewrite to Math.Min
                if (dists[to[i]] <= min)
                {
                    min = dists[to[i]];
                }
            }

            // remove jumps
            if (Math.Abs(diff) == 1 || Math.Abs(diff) == size)
            {
                RemoveEdge(from, through + diff);
                RemoveEdge(from, through + revertDiff);
                RemoveEdge(from, through - revertDiff);
            }

            return min;
        }


        #endregion Methods
    }
}
