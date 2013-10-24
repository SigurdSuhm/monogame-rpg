#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// Collision grid used for narrowing down what scene nodes to check collisions for in a scene.
    /// </summary>
    public class CollisionGrid
    {
        #region Fields

        // Collision grid cells
        private CollisionCell[,] cells;
        // Dimensions of individual cells
        private Dimensions2 cellDimensions;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the array of collision cells in the grid.
        /// </summary>
        public CollisionCell[,] Cells
        {
            get { return cells; }
        }

        /// <summary>
        /// Gets the dimensions of individual grid cells.
        /// </summary>
        public Dimensions2 CellDimensions
        {
            get { return cellDimensions; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="sceneDimensions">Dimensions (in pixels) of the scene.</param>
        /// <param name="gridDimensions">Dimensions (in cells) of the collision grid.</param>
        public CollisionGrid(Dimensions2 sceneDimensions, Dimensions2 gridDimensions)
        {
            cells = new CollisionCell[gridDimensions.X, gridDimensions.Y];

            cellDimensions = new Dimensions2(sceneDimensions.X / gridDimensions.X,
                sceneDimensions.Y / gridDimensions.Y);

            for (int i = 0; i < gridDimensions.X; i++)
            {
                for (int j = 0; j < gridDimensions.Y; j++)
                {
                    cells[i, j] = new CollisionCell(i, j);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the collision grid cell at a specified position.
        /// </summary>
        /// <param name="position">Position in the scene.</param>
        /// <returns>Cell at the specified position.</returns>
        public CollisionCell GetCellAtPosition(Vector2 position)
        {
            int[] cellIndex = new int[2];

            cellIndex[0] = (int)position.X / cellDimensions.X;
            cellIndex[1] = (int)position.Y / cellDimensions.Y;

            return cells[cellIndex[0], cellIndex[1]];
        }

        #endregion
    }
}