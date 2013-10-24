#region Using Statements

using System.Collections.Generic;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// Individual cells in a scene collision grid.
    /// </summary>
    public class CollisionCell
    {
        #region Fields

        // List of scene nodes in the collision cell
        private List<SceneNode> sceneNodeList;
        // List of tiles in the collision cell
        private List<Tile> tileList;

        // X and Y index of the cell
        int xIndex, yIndex;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of scene nodes in the collision cell.
        /// </summary>
        public List<SceneNode> SceneNodeList
        {
            get { return sceneNodeList; }
        }

        /// <summary>
        /// Gets the list of tiles in the collision cell.
        /// </summary>
        public List<Tile> TileList
        {
            get { return tileList; }
        }

        /// <summary>
        /// Gets the X index of the cell.
        /// </summary>
        public int XIndex
        {
            get { return xIndex; }
        }

        /// <summary>
        /// Gets the X index of the cell.
        /// </summary>
        public int YIndex
        {
            get { return yIndex; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CollisionCell(int xIndex, int yIndex)
        {
            sceneNodeList = new List<SceneNode>();
            tileList = new List<Tile>();

            this.xIndex = xIndex;
            this.yIndex = yIndex;
        }

        #endregion
    }
}