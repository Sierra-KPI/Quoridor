namespace Quoridor
{
	interface IElement
	{
		Position Position { get; }

		bool Place();
	}
}
