using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {
    class Graph {
        int size;
        int[,] edges;

        int min = int.MaxValue;
        int[] distances;

        public Graph(int[,] edges, int size) {
            this.size = size;
            this.edges = edges;

            this.distances = new int[this.size];
            for (int i = 0; i < this.size; i++) {
                distances[i] = int.MaxValue;
            }
        }

        public Graph(Cell[,] cells, int size) {
            this.size = size;
            this.edges = new int[cells.GetLength(0), 2];
            for (int i = 0; i < cells.GetLength(0); i++) {
                for (int j = 0; j < 2; j++) {
                    this.edges[i, j] = cells[i, j].id;
                }
            }

            this.distances = new int[this.size];
            for (int i = 0; i < this.size; i++) {
                distances[i] = int.MaxValue;
            }
        }

        



    }
}
