﻿namespace Quoridor.Model
{
    public class Coordinates
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Coordinates Left()
        {
            Coordinates newCoordinates = new Coordinates(X - 1, Y);
            return newCoordinates;
        }
        public Coordinates Right()
        {
            Coordinates newCoordinates = new Coordinates(X + 1, Y);
            return newCoordinates;
        }
        public Coordinates Up()
        {
            Coordinates newCoordinates = new Coordinates(X, Y - 1);
            return newCoordinates;
        }
        public Coordinates Down()
        {
            Coordinates newCoordinates = new Coordinates(X, Y + 1);
            return newCoordinates;
        }
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}