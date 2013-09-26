#region Using Statements

using System;
using System.Xml;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Loader class for importing a tile map from an Xml file.
    /// </summary>
    public class TileMapLoader
    {
        #region Constructors

        /// <summary>
        /// Default constructors
        /// </summary>
        public TileMapLoader()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads a tile map from an Xml file.
        /// </summary>
        /// <param name="tileMapFilePath">Path to the tile map Xml file.</param>
        /// <returns>Tile map created from the specified Xml file.</returns>
        public TileMap LoadTileMap(string tileMapFilePath)
        {
            XmlReader reader;

            // Check for file name ending and create Xml reader
            if (tileMapFilePath.EndsWith(".xml"))
                reader = XmlReader.Create(tileMapFilePath);
            else
                reader = XmlReader.Create(tileMapFilePath + ".xml");

            // TODO: IMPLEMENT

            return null;
        }

        #endregion
    }
}