#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Gameplay map consisting of graphical tiles.
    /// </summary>
    public class TileMap : IDrawable
    {
        #region Fields

        // Tile map dimensions
        private int[] dimensions = new int[2];
        // Size (height and width in pixels) of individual tiles
        private int tileImageSize = 32;

        // Array of tiles
        private Tile[,] tileArray;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the dimensions of the tilemap.
        /// </summary>
        public int[] Dimensions
        {
            get { return dimensions; }
        }

        /// <summary>
        /// Gets the tile array associated with the tile map.
        /// </summary>
        public Tile[,] TileArray
        {
            get { return tileArray; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TileMap(int width, int height)
        {
            dimensions[0] = width;
            dimensions[1] = height;

            tileArray = new Tile[width, height];
        }

        /// <summary>
        /// Creates a new tile map with a custom tile image size.
        /// </summary>
        public TileMap(int width, int height, int tileImageSize)
        {
            dimensions[0] = width;
            dimensions[1] = height;

            tileArray = new Tile[width, height];

            this.tileImageSize = tileImageSize;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for all individual tiles in the tile map.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            for (int i = 0; i < dimensions[0]; i++)
            {
                for (int j = 0; j < dimensions[1]; j++)
                {
                    // Sets the correct position for all tiles in the tile map
                    tileArray[i, j].Position = new Vector2(tileImageSize * i, tileImageSize * j);
                }
            }
        }

        /// <summary>
        /// Draws the entire tile map to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the entire tile array
            foreach (Tile tile in tileArray)
                tile.Draw(spriteBatch);
        }

        #endregion
    }
}