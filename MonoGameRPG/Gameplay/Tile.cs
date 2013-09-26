#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Class for individual tiles in a tile map.
    /// </summary>
    public class Tile : IDrawable
    {
        #region Fields

        // Path to the tile image
        private string tileImagePath;
        // Image for the tile
        private Image tileImage;

        // Tile position
        private Vector2 position;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position of the tile.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                tileImage.Position = position;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Tile(string tileImagePath)
        {
            this.tileImagePath = tileImagePath;
            tileImage = new Image(tileImagePath);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for the tile.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            tileImage.LoadContent(contentManager);
        }

        /// <summary>
        /// Draws the tile to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileImage.Draw(spriteBatch);
        }

        #endregion
    }
}