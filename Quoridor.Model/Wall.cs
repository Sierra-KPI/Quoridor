using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quoridor.Model
{
	internal class Wall: IElement
	{
		private Orientation _orientation;
		public Position Position { get; set; }

		public bool Place();
	}
}
