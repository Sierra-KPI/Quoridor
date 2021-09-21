using System;

namespace Quoridor
{
	interface IElement
	{
		Position Position { get; set; }

		bool Place();
	}
}
