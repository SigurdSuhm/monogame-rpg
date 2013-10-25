#region Using Statements

using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Gameplay map consisting of graphical tiles.
    /// </summary>
    public class TileMap : IDrawable, IUpdateable
    {
        #region Fields

        // Tile map dimensions
        private Dimensions2 dimensions;
        // Size (height and width in pixels) of individual tiles
        private Dimensions2 tileDimensions;

        // Array of tiles
        private Tile[,] tileArray;
        // Dictionary of tile set images
        TileSetImage[] tileSetImageArray;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the dimensions of the tile map.
        /// </summary>
        public Dimensions2 Dimensions
        {
            get { return dimensions; }
        }

        /// <summary>
        /// Gets the dimensions of individual tile elements.
        /// </summary>
        public Dimensions2 TileDimensions
        {
            get { return tileDimensions; }
        }

        /// <summary>
        /// Gets the tile array associated with the tile map.
        /// </summary>
        public Tile[,] TileArray
        {
            get { return tileArray; }
        }

        /// <summary>
        /// Gets the dictionary containing tile sets used in the tile map.
        /// </summary>
        public TileSetImage[] TileSetImageArray
        {
            get { return tileSetImageArray; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="dimensions">Dimensions of the tile map.</param>
        /// <param name="tileDimensions">Dimensions of tile elements.</param>
        /// <param name="tileArray">Array of tiles.</param>
        /// <param name="tileSetImageArray">Array of tile set images.</param>
        public TileMap(Dimensions2 dimensions, Dimensions2 tileDimensions, Tile[,] tileArray, TileSetImage[] tileSetImageArray)
        {
            this.dimensions = dimensions;
            this.tileDimensions = tileDimensions;

            this.tileArray = tileArray;

            this.tileSetImageArray = tileSetImageArray;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for all individual tiles in the tile map.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            foreach (TileSetImage tileSet in tileSetImageArray)
                tileSet.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the tile map.
        /// </summary>
        public void UnloadContent()
        {
            foreach (TileSetImage image in tileSetImageArray)
                image.UnloadContent();
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing value.</param>
        public void Update(GameTime gameTime)
        {
            // Update all tile set images
            foreach (TileSetImage image in tileSetImageArray)
                image.Update(gameTime);
        }

        /// <summary>
        /// Draws the entire tile map to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the entire tile array
            for (int i = 0; i < dimensions.X; i++)
            {
                for (int j = 0; j < dimensions.Y; j++)
                {
                    tileArray[i, j].Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Draws the entire tile map to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        /// <param name="scenePosition">Position of the scene on screen.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 scenePosition)
        {
            // Draw the entire tile array
            for (int i = 0; i < dimensions.X; i++)
            {
                for (int j = 0; j < dimensions.Y; j++)
                {
                    tileArray[i, j].Draw(spriteBatch, scenePosition);
                }
            }
        }

        #endregion
    }
}