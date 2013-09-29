#region Using Statements

using System;
using System.Xml;

using Microsoft.Xna.Framework;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Loader class for importing a tile map from an Xml file.
    /// </summary>
    public static class TileMapLoader
    {
        #region Methods

        /// <summary>
        /// Loads a tile map from an Xml file.
        /// </summary>
        /// <param name="tileMapFilePath">Path to the tile map Xml file.</param>
        /// <returns>Tile map created from the specified Xml file.</returns>
        public static TileMap LoadTileMap(string tileMapFilePath)
        {
            // Import Xml file as XmlDocument
            XmlDocument tileSetFile = new XmlDocument();
            tileSetFile.Load("Content/Maps/" + tileMapFilePath);

            XmlNode tileMapParentNode = tileSetFile.DocumentElement;

            // Array of tile set images
            TileSetImage[] tileSetArray = new TileSetImage[int.Parse(tileMapParentNode["TileSetCount"].InnerText)];
            
            // Dimensions of the tile map
            Dimensions2 tileMapDimensions;
            string[] dimensionsSplitString = tileMapParentNode["Dimensions"].InnerText.Split(',');
            tileMapDimensions.X = int.Parse(dimensionsSplitString[0]);
            tileMapDimensions.Y = int.Parse(dimensionsSplitString[1]);

            // Parse tile dimensions
            string[] tileDimensionsSplitString = tileMapParentNode["TileDimensions"].InnerText.Split(',');
            Dimensions2 tileDimensions;
            tileDimensions.X = int.Parse(tileDimensionsSplitString[0]);
            tileDimensions.Y = int.Parse(tileDimensionsSplitString[1]);

            // Create individual tile set images
            foreach (XmlNode currentTileSetNote in tileMapParentNode.SelectNodes("TileSet"))
            {
                int tileSetIndex = int.Parse(currentTileSetNote["Index"].InnerText);
                string fileName = currentTileSetNote["FileName"].InnerText;

                // Parse tile set dimensions
                string[] tileSetDimensionsSplitString = currentTileSetNote["Dimensions"].InnerText.Split(',');
                Dimensions2 dimensions;
                dimensions.X = int.Parse(tileSetDimensionsSplitString[0]);
                dimensions.Y = int.Parse(tileSetDimensionsSplitString[1]);

                tileSetArray[tileSetIndex] = new TileSetImage(fileName, dimensions, tileDimensions);
            }

            // Array of tiles for the tile map
            Tile[,] tileArray = new Tile[tileMapDimensions.X, tileMapDimensions.Y];

            // Parse tile map data
            XmlNodeList tileMapRowNodeList = tileMapParentNode["Data"].SelectNodes("Row");
            for (int y = 0; y < tileMapDimensions.Y; y++)
            {
                string[] tileRowSplitString = tileMapRowNodeList[y].InnerText.Split(';');

                for (int x = 0; x < tileMapDimensions.X; x++)
                {
                    string tileDataString = tileRowSplitString[x];
                    tileDataString = tileDataString.Replace("[", String.Empty);
                    tileDataString = tileDataString.Replace("]", String.Empty);

                    string[] tileDataSplitString = tileDataString.Split(':');

                    int tileIndex = int.Parse(tileDataSplitString[0]);
                    int tileSetIndex = int.Parse(tileDataSplitString[1]);

                    // Create tile object
                    tileArray[x, y] = new Tile(tileSetArray[tileSetIndex], tileIndex);
                    tileArray[x, y].Position = new Vector2(x * tileSetArray[tileSetIndex].TileDimensions.X, y * tileSetArray[tileSetIndex].TileDimensions.Y);
                }
            }

            // Create tile map object
            TileMap tileMap = new TileMap(tileMapDimensions, tileDimensions, tileArray, tileSetArray);

            return tileMap;
        }

        #endregion
    }
}