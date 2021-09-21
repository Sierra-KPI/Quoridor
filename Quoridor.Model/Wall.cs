using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model
{
	internal class Wall: IElement
	{
		public Position Position { get; set; }

		public bool Place(Position position);
	}
}
