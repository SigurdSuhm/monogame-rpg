#region Using Statements

using System;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Describes dimensions in 2D by width and height.
    /// </summary>
    public struct Dimensions2
    {
        #region Fields

        public int X;
        public int Y;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new Dimensions2 with both X and Y set to the same value.
        /// </summary>
        /// <param name="value">X and Y value.</param>
        public Dimensions2(int value)
        {
            X = value;
            Y = value;
        }

        /// <summary>
        /// Creates a new Dimensions2 with specified X and Y values.
        /// </summary>
        /// <param name="x">Dimensions X value.</param>
        /// <param name="y">Dimensions Y value.</param>
        public Dimensions2(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Static Properties

        /// <summary>
        /// Returns a Dimensions2 with both X and Y set to 1.
        /// </summary>
        public static Dimensions2 One
        {
            get { return new Dimensions2(1); }
        }

        /// <summary>
        /// Returns a Dimensions2 with both X and Y set to 0.
        /// </summary>
        public static Dimensions2 Zero
        {
            get { return new Dimensions2(0); }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Gets or sets the X or Y value based on index. 0 for X, 1 for Y.
        /// </summary>
        /// <param name="index">Index</param>
        /// <returns>Dimensions X or Y value.</returns>
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion
    }
}
