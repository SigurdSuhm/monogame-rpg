#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace MonoGameRPG.Graphics
{
    public class TileSetImage : Image
    {
        #region Fields

        // Name of the tile set
        private string fileName;

        // Dimensions (in tiles) of the tile set
        private Dimensions2 tileSetDimensions;
        // Dimensions of individual tiles in pixels
        private Dimensions2 tileDimensions;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the tile set.
        /// </summary>
        public string FileName
        {
            get { return fileName; }
        }

        /// <summary>
        /// Gets the dimensions of the tile set image in tiles.
        /// </summary>
        public Dimensions2 TileSetDimensions
        {
            get { return tileSetDimensions; }
        }

        /// <summary>
        /// Gets the dimensions of the tiles in the tile set image in pixels.
        /// </summary>
        public Dimensions2 TileDimensions
        {
            get { return tileDimensions; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="texturePath">Path to the image texture.</param>
        public TileSetImage(string texturePath, Dimensions2 tileSetDimensions, Dimensions2 tileDimensions)
            : base("Textures/TileSets/" + texturePath)
        {
            this.fileName = texturePath;
            this.tileSetDimensions = tileSetDimensions;
            this.tileDimensions = tileDimensions;

            screenDimensions = tileDimensions;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a source rectangle for a tile given the index of the tile.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        /// <returns>Source rectangle for drawing the tile.</returns>
        public Rectangle GetSourceRectangle(int tileIndex)
        {
            Rectangle sourceRect;

            sourceRect.X = (tileIndex % tileSetDimensions.X) * tileDimensions.X;
            sourceRect.Y = (tileIndex / tileSetDimensions.X) * tileDimensions.Y;
            sourceRect.Width = tileDimensions.X;
            sourceRect.Height = tileDimensions.Y;

            return sourceRect;
        }

        #endregion
    }
}
