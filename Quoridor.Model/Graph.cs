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

            for (int i = 0; i < this.edges.GetLength(0); i++) {
                addEdge(edges[i, 0], edges[i, 1]);
                addEdge(edges[i, 1], edges[i, 0]);
            }

        }

        void addEdge(int v1, int v2) {
            adjacencyList[v1].AddLast(v2);
        }

        bool removeEdge(int v1, int v2) {
            return adjacencyList[v1].Remove(v2);
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
