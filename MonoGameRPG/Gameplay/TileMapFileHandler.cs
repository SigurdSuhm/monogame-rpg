#region Using Statements

using System;
using System.Xml;

using Microsoft.Xna.Framework;

using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Loader class for importing a tile map from an Xml file.
    /// </summary>
    public static class TileMapFileHandler
    {
        #region Constants

        private const string MAPS_BASE_PATH = "Content/Maps/";

        #endregion

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
            tileSetFile.Load(MAPS_BASE_PATH + tileMapFilePath);

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

                    TileCollisionValue tileCollision = TileCollisionValue.None;
                    if (tileDataSplitString[2] == "1")
                        tileCollision = TileCollisionValue.Solid;

                    // Create tile object
                    tileArray[x, y] = new Tile(tileSetArray[tileSetIndex], tileIndex, tileSetIndex, tileCollision);
                    tileArray[x, y].Position = new Vector2(x * tileSetArray[tileSetIndex].TileDimensions.X, y * tileSetArray[tileSetIndex].TileDimensions.Y);
                }
            }

            // Create tile map object
            TileMap tileMap = new TileMap(tileMapDimensions, tileDimensions, tileArray, tileSetArray);

            return tileMap;
        }

        /// <summary>
        /// Saves an existing tile map to an Xml file.
        /// </summary>
        /// <param name="tileMap">Tile map to be saved.</param>
        /// <param name="fileName">Xml file name.</param>
        public static void SaveTileMap(TileMap tileMap, string fileName)
        {
            // Create XmlWriter object for writing to Xml
            using (XmlWriter writer = XmlWriter.Create(MAPS_BASE_PATH + fileName))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("TileMap");

                // Write tile map dimensions
                writer.WriteElementString("Dimensions", String.Format("{0},{1}", tileMap.Dimensions.X, tileMap.Dimensions.Y));
                // Write tile element dimensions
                writer.WriteElementString("TileDimensions", String.Format("{0},{1}", tileMap.TileDimensions.X,
                    tileMap.TileDimensions.Y));

                // Write number of tile sets
                int tileSetCount = tileMap.TileSetImageArray.Length;
                writer.WriteElementString("TileSetCount", tileSetCount.ToString());

                // Write information on individual tile sets
                for (int i = 0; i < tileSetCount; i++)
                {
                    writer.WriteStartElement("TileSet");

                    // Index of the tile map
                    writer.WriteElementString("Index", i.ToString());
                    // File name of the tile set
                    writer.WriteElementString("FileName", tileMap.TileSetImageArray[i].FileName);
                    // Dimensions of the tile set
                    writer.WriteElementString("Dimensions", String.Format("{0},{1}", tileMap.TileSetImageArray[i].TileSetDimensions.X,
                        tileMap.TileSetImageArray[i].TileSetDimensions.Y));

                    writer.WriteEndElement();
                }

                // Write rows of tiles
                for (int y = 0; y < tileMap.TileArray.GetLength(1); y++)
                {
                    writer.WriteStartElement("Row");

                    for (int x = 0; x < tileMap.TileArray.GetLength(0); x++)
                    {
                        string tileElementString = "[";
                        tileElementString += tileMap.TileArray[x, y].TileIndex;
                        tileElementString += ":";
                        tileElementString += tileMap.TileArray[x, y].TileSetIndex;
                        tileElementString += ":";

                        switch (tileMap.TileArray[x, y].CollisionValue)
                        {
                            case TileCollisionValue.Solid:
                                tileElementString += "1";
                                break;
                            default:
                                tileElementString += "0";
                                break;
                        }

                        tileElementString += "]";

                        // If this is not the last line in the row add a ';'
                        if (x != (tileMap.TileArray.GetLength(0) - 1))
                            tileElementString += ";";

                        writer.WriteString(tileElementString);
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        #endregion
    }
}