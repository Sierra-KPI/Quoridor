using System.Collections.Generic;

namespace Quoridor.Model 
{
    class Graph 
    {
        private int _size;
        private int[,] _edges;
        private LinkedList<int>[] _adjacencyList;

        public Graph(int size) 
        {
            _size = size * size;
            _edges = new int[2 * size * (size - 1), 2];

            int edgeId = 0;
            for (var v = 0; v < size; v++) 
            {
                if (v - 1 >= 0 && (v % size != 0)) 
                {
                    _edges[edgeId, 0] = v - 1;
                    _edges[edgeId, 1] = v;
                    edgeId++;
                }
                if (v - 3 >= 0) 
                {
                    _edges[edgeId, 0] = v - 3;
                    _edges[edgeId, 1] = v;
                    edgeId++;
                }
            }

            MakeAdjacencyList();
        }

        public Graph(int[,] edges, int size) 
        {
            _size = size;
            _edges = edges;

            MakeAdjacencyList();
        }

        void MakeAdjacencyList() 
        {
            _adjacencyList = new LinkedList<int>[_size];

            for (var i = 0; i < _adjacencyList.GetLength(0); ++i) 
            {
                _adjacencyList[i] = new LinkedList<int>();
            }

            for (var i = 0; i < _edges.GetLength(0); i++) 
            {
                AddEdge(_edges[i, 0], _edges[i, 1]);
                AddEdge(_edges[i, 1], _edges[i, 0]);
            }

        }

        bool AddEdge(int vertex1, int vertex2) 
        {
            if (_adjacencyList[vertex1].AddLast(vertex2) != null) 
                return true;
            return false;
        }

        bool RemoveEdge(int vertex1, int vertex2) 
        {
            return _adjacencyList[vertex1].Remove(vertex2);
        }

        public int[] GetEdgesForVertex(int vertex) 
        {
            LinkedList<int> list = _adjacencyList[vertex];
            int[] edgs = new int[list.Count];
            list.CopyTo(edgs, 0);
            return edgs;
        }

        public int[] DijkstraAlgorithm(int startVertex) 
        {
            int[] distances = new int[_size];
            bool[] used = new bool[_size];

            for (var i = 0; i < _size; i++) 
            {
                distances[i] = int.MaxValue;
            }
            distances[startVertex] = 0;

            for (var i = 0; i < _size; i++) 
            {
                int curr = -1;
                for (var j = 0; j < _size; j++)
                    if (!used[j] && (curr == -1 || distances[j] < distances[curr]))
                        curr = j;

                if (distances[curr] == int.MaxValue) break;
                used[curr] = true;

                LinkedList<int> list = _adjacencyList[curr];
                foreach (int vertex in list)
                    if (distances[curr] + 1 < distances[vertex])
                        distances[vertex] = distances[curr] + 1;

            }
            return distances;
        }


    }
}
