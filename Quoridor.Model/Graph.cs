using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {
    class Graph {
        int size;
        int[,] edges;
        LinkedList<int>[] adjacencyList;

        public Graph(int _size) {
            this.size = _size * _size;
            this.edges = new int[2 * _size * (_size - 1), 2];

            int idW = 0;
            for (int v = 0; v < size; v++) {
                if (v - 1 >= 0 && (v % _size != 0)) {
                    edges[idW, 0] = v - 1;
                    edges[idW, 1] = v;
                    idW++;
                }
                if (v - 3 >= 0) {
                    edges[idW, 0] = v - 3;
                    edges[idW, 1] = v;
                    idW++;
                }
            }

            makeAdjacencyList();
        }

        public Graph(int[,] edges, int size) {
            this.size = size;
            this.edges = edges;

            makeAdjacencyList();
        }

        void makeAdjacencyList() {
            adjacencyList = new LinkedList<int>[this.size];

            for (int i = 0; i < adjacencyList.Length; ++i) {
                adjacencyList[i] = new LinkedList<int>();
            }

            for (int i = 0; i < edges.GetLength(0); i++) {
                addEdge(edges[i, 0], edges[i, 1]);
                addEdge(edges[i, 1], edges[i, 0]);
            }

        }

        void addEdge(int vertex1, int vertex2) {
            adjacencyList[vertex1].AddLast(vertex2);
        }

        bool removeEdge(int vertex1, int vertex2) {
            return adjacencyList[vertex1].Remove(vertex2);
        }

        public int[] getEdgesForVertex(int vertex) {
            LinkedList<int> list = adjacencyList[vertex];
            int[] edgs = new int[list.Count];
            list.CopyTo(edgs, 0);
            return edgs;
        }

        public int[] dijkstraAlgorithm(int startVertex) {
            int[] distances = new int[size];
            bool[] used = new bool[size];

            for (int i = 0; i < size; i++) {
                distances[i] = int.MaxValue;
            }
            distances[startVertex] = 0;

            for (int i = 0; i < size; i++) {
                int curr = -1;
                for (int j = 0; j < size; j++)
                    if (!used[j] && (curr == -1 || distances[j] < distances[curr]))
                        curr = j;

                if (distances[curr] == int.MaxValue) break;
                used[curr] = true;

                LinkedList<int> list = adjacencyList[curr];
                foreach (int vertex in list)
                    if (distances[curr] + 1 < distances[vertex])
                        distances[vertex] = distances[curr] + 1;

            }
            return distances;
        }




    }
}
