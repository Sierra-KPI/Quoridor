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

        int min = int.MaxValue;
        int[] distances;

        public Graph(int[,] edges, int size) {
            this.size = size;
            this.edges = edges;

            makeAdjacencyList();
        }


        void makeAdjacencyList() {
            this.adjacencyList = new LinkedList<int>[this.size];

            for (int i = 0; i < adjacencyList.Length; ++i) {
                this.adjacencyList[i] = new LinkedList<int>();
            }

            

        }





    }
}
