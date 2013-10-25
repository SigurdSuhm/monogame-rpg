#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Class for individual tiles in a tile map.
    /// </summary>
    public class Tile : IDrawable
    {
        #region Fields

        // Index of the tile in the tile set
        private int tileIndex;
        // Index of the tile set used according to the tile map
        private int tileSetIndex;

        // Image for the tile set
        private TileSetImage tileSetImage;
        // Source rectangle for drawing the tile
        private Rectangle sourceRect;

        // Tile position
        private Vector2 position;
        // Dimensions of the tile
        private Dimensions2 dimensions;
        // Collision value of the tile
        private TileCollisionValue collisionValue;
        // Bounding box for the tile
        // TODO: Might need fixing
        private BoundingBoxAA boundingBox;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the index of the tile in the tile set.
        /// </summary>
        public int TileIndex
        {
            get { return tileIndex; }
            set 
            { 
                tileIndex = value;
                sourceRect = tileSetImage.GetSourceRectangle(tileIndex);
            }
        }

        /// <summary>
        /// Gets the index of the used tile set according to the tile map.
        /// </summary>
        public int TileSetIndex
        {
            get { return tileSetIndex; }
        }

        /// <summary>
        /// Gets or sets the position of the tile.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set 
            { 
                position = value;

                // Create new bounding box
                boundingBox = new BoundingBoxAA(position, new Vector2(dimensions.X, dimensions.Y) + position);
            }
        }

        /// <summary>
        /// Gets the dimensions of the tile.
        /// </summary>
        public Dimensions2 Dimensions
        {
            get { return dimensions; }
        }

        /// <summary>
        /// Gets the collision value of the tile.
        /// </summary>
        public TileCollisionValue CollisionValue
        {
            get { return collisionValue; }
        }

        /// <summary>
        /// Gets the bounding box for the tile.
        /// </summary>
        public BoundingBoxAA BoundingBox
        {
            get { return boundingBox; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source rectangle for the tile.
        /// </summary>
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set { sourceRect = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor creating the tile from an existing tile set image.
        /// </summary>
        /// <param name="tileSetImage">Tile set image.</param>
        public Tile(TileSetImage tileSetImage, int tileIndex, int tileSetIndex, TileCollisionValue collisionValue)
        {
            this.tileIndex = tileIndex;
            this.tileSetIndex = tileSetIndex;
            this.tileSetImage = tileSetImage;
            dimensions = tileSetImage.TileDimensions;
            sourceRect = tileSetImage.GetSourceRectangle(tileIndex);

            this.collisionValue = collisionValue;

            boundingBox = new BoundingBoxAA(Vector2.Zero, new Vector2(dimensions.X, dimensions.Y));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Draws the tile to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileSetImage.Draw(spriteBatch, position, sourceRect);
        }

        /// <summary>
        /// Draws the tile to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        /// <param name="scenePosition">Position of the scene on screen.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 scenePosition)
        {
            tileSetImage.Draw(spriteBatch, position + scenePosition, sourceRect);
        }

        #endregion
    }
}