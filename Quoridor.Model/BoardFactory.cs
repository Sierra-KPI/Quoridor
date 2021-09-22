using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model {

    public interface IBoardFactory {
        Board CreateBoard();
    }

    public class BoardFactory : IBoardFactory {
        public Board CreateBoard() {
            Board board = new Board();
            return board;
        }
    }
}
