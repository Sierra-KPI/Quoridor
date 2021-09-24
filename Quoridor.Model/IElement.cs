namespace Quoridor.Model
{
	interface IElement
	{
		Position Position { get; }

		bool Place();
	}
}
