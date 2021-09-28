namespace Quoridor.Model
{
	interface IElement
	{
		Coordinates Coordinate { get; }

		bool Place();
	}
}
