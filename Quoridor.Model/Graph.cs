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

        }

        

        



    }
}
