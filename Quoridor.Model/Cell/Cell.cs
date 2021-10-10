﻿namespace Quoridor.Model
{
    public class Cell : IElement
    {
        #region Properties

        public int Id { get; private set; }
        public IPlayer Player { get; set; }
        public Coordinates Coordinates { get; private set; }
        public static Cell Default => new(s_defaultCoordinates, DefaultId)
        {
            Id = DefaultId,
            Coordinates = s_defaultCoordinates,
        };

        #endregion Properties

        #region Constructor

        public Cell(Coordinates coordinates, int id)
        {
            Coordinates = coordinates;
            Id = id;
        }

        #endregion Constructor

        #region Fields

        private const int DefaultId = 500;
        private static readonly Coordinates
            s_defaultCoordinates = new(0, 0);

        #endregion Fields
    }
}
