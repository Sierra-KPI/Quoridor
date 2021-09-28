namespace Quoridor.Model
{
    internal interface IElement
    {
        Coordinates Position { get; }

        bool Place();
    }
}
