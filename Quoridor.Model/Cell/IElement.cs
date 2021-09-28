namespace Quoridor.Model
{
	interface IElement
	{
		Coordinates Position { get; }

		bool Place();
	}
}
