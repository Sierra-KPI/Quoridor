using System;

namespace Quoridor.Model
{
    public class Coordinates
    {
        #region Properties

        public int X { get; }
        public int Y { get; }

        #endregion Properties

        #region Constructor

        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion Constructor

        #region Methods

        public Coordinates Up()
        {
            Coordinates newCoordinates = new Coordinates(X - 1, Y);
            return newCoordinates;
        }

        public Coordinates Down()
        {
            Coordinates newCoordinates = new Coordinates(X + 1, Y);
            return newCoordinates;
        }

        public Coordinates Left()
        {
            Coordinates newCoordinates = new Coordinates(X, Y - 1);
            return newCoordinates;
        }

        public Coordinates Right()
        {
            Coordinates newCoordinates = new Coordinates(X, Y + 1);
            return newCoordinates;
        }

        #endregion Methods

        #region Operators

        public static bool operator == (Coordinates coordinates1,
            Coordinates coordinates2)
        {
            if (coordinates1.X == coordinates2.X &&
                coordinates1.Y == coordinates2.Y)
            {
                return true;
            }

            return false;
        }

        public static bool operator != (Coordinates coordinates1,
            Coordinates coordinates2)
        {
            if (coordinates1.X != coordinates2.X ||
                coordinates1.Y != coordinates2.Y)
            {
                return true;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            if (X == ((Coordinates)obj).X &&
                Y == ((Coordinates)obj).Y)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);

        #endregion Operators
    }
}
