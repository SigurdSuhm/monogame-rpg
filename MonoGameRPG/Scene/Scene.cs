#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Gameplay;
using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;
using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// A scene in the gameworld. Scenes are handled by the SceneManager class.
    /// </summary>
    public class Scene : IDrawable, IUpdateable
    {
        #region Fields

        // Path to the tile map file
        private string tileMapPath;
        // Tile map used for the scene
        private TileMap tileMap;
        // Dictionary of entities in the scene
        private Dictionary<string, SceneNode> sceneNodeDictionary;
        // Contains snapshots of entity positions from last frame for collisions handling
        private Dictionary<string, Vector2> prevEntityPositions;

        // Position of the scene on screen
        private Vector2 position;
        // Position dimensions of the scene
        private Dimensions2 dimensions;

        // Collision grid used for narrowing down collision detection
        private CollisionGrid collisionGrid;

        // Game screen viewport
        private Viewport viewport;
        // Scene position offset from the player
        private Vector2 positionOffset;
        // Scene position bounds
        private Vector2 positionMinBounds, positionMaxBounds;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of entities in the scene.
        /// </summary>
        public Dictionary<string, SceneNode> SceneNodeDictionary
        {
            get { return sceneNodeDictionary; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="tileMapPath">Path to the tile map file.</param>
        public Scene(string tileMapPath)
        {
            collisionGrid = null;
            this.tileMapPath = tileMapPath;
            position = Vector2.Zero;
            dimensions = Dimensions2.Zero;

            viewport = BaseGame.Instance.GraphicsDevice.Viewport;
            positionOffset = new Vector2((viewport.Width / 2) - 16, (viewport.Height / 2) - 16);

            positionMinBounds = Vector2.Zero;
            positionMaxBounds = Vector2.Zero;

            sceneNodeDictionary = new Dictionary<string, SceneNode>();
            prevEntityPositions = new Dictionary<string, Vector2>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the scene.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            // Load tile map and related content
            tileMap = TileMapFileHandler.LoadTileMap(tileMapPath);
            tileMap.LoadContent(contentManager);

            // Set scene dimensions
            dimensions = tileMap.Dimensions * tileMap.TileDimensions;

            // Load content for all entities
            foreach (SceneNode node in sceneNodeDictionary.Values)
            {
                node.LoadContent(contentManager);

                if (node is Entity)
                {
                    Entity entity = node as Entity;
                    // Create previous position for entity
                    prevEntityPositions.Add(entity.Name, entity.Position);
                }
            }

            positionMinBounds = new Vector2(0 - (dimensions.X - viewport.Width),
                0 - (dimensions.Y - viewport.Height));

            position = -sceneNodeDictionary["Player"].Position;
            position += positionOffset;
            position.X = MathHelper.Clamp(position.X, positionMinBounds.X, positionMaxBounds.X);
            position.Y = MathHelper.Clamp(position.Y, positionMinBounds.Y, positionMaxBounds.Y);
        }

        /// <summary>
        /// Unloads all content associated with the scene object.
        /// </summary>
        public void UnloadContent()
        {
            tileMap.UnloadContent();

            // Unload content for all active entities
            foreach (SceneNode node in sceneNodeDictionary.Values)
            {
                node.UnloadContent();
            }
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            tileMap.Update(gameTime);

            foreach (SceneNode node in sceneNodeDictionary.Values)
            {
                if (node is Entity)
                {
                    Entity entity = node as Entity;
                    prevEntityPositions[entity.Name] = entity.Position;
                }

                node.Update(gameTime);

                handleNodeCollisions();

                // Check if the collision cell should be changed
                if (!node.IsStatic)
                {
                    CollisionCell newCell = collisionGrid.GetCellAtPosition(node.Position);

                    if (newCell != node.CollisionCell)
                    {
                        // Remove scene node from old cell and add to new
                        node.CollisionCell.SceneNodeList.Remove(node);
                        newCell.SceneNodeList.Add(node);
                        node.CollisionCell = newCell;
                        node.CollisionCellsToCheck = getCellsToCheck(node);
                    }
                }
            }

            // Update scene position
            position = -sceneNodeDictionary["Player"].Position;
            position += positionOffset;
            position.X = MathHelper.Clamp(position.X, positionMinBounds.X, positionMaxBounds.X);
            position.Y = MathHelper.Clamp(position.Y, positionMinBounds.Y, positionMaxBounds.Y);
        }

        /// <summary>
        /// Draws the entire scene to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch, position);

            // Draw all active entities
            foreach (SceneNode node in sceneNodeDictionary.Values)
                node.Draw(spriteBatch, position);
        }

        /// <summary>
        /// Creates a collision grid for the scene.
        /// </summary>
        /// <param name="gridDimensions">Dimensions (in cells) of the collision grid.</param>
        public void CreateCollisionGrid(Dimensions2 gridDimensions)
        {
            Dimensions2 sceneDimensions = tileMap.Dimensions * tileMap.TileDimensions;

            collisionGrid = new CollisionGrid(sceneDimensions, gridDimensions);

            // Add tiles to the proper collision grid cell
            foreach (Tile tile in tileMap.TileArray)
            {
                collisionGrid.GetCellAtPosition(tile.Position).TileList.Add(tile);
            }

            // Add scene nodes to the proper collision grid cell
            foreach (SceneNode node in sceneNodeDictionary.Values)
            {
                CollisionCell cell = collisionGrid.GetCellAtPosition(node.Position);
                cell.SceneNodeList.Add(node);
                node.CollisionCell = cell;
                node.CollisionCellsToCheck = getCellsToCheck(node);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles collision detection for all scene nodes.
        /// </summary>
        private void handleNodeCollisions()
        {
            foreach (SceneNode node in sceneNodeDictionary.Values)
            {
                if (node is Entity)
                {
                    Entity entity = node as Entity;

                    // Has a solid tile collision been found?
                    bool tileCollisionFound = false;

                    foreach (CollisionCell cell in entity.CollisionCellsToCheck)
                    {
                        if (cell != null)
                        {
                            foreach (Tile tile in cell.TileList)
                            {
                                if (!tileCollisionFound && tile.CollisionValue == TileCollisionValue.Solid)
                                {
                                    // Check for tile collision
                                    if (entity.BoundingShape.CollidesWith(tile.BoundingBox))
                                    {
                                        tileCollisionFound = true;
                                        // Return to previous position
                                        entity.Position = prevEntityPositions[entity.Name];
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns the array of collision cells that a scene node should be checked against.
        /// </summary>
        /// <param name="node">Scene node object.</param>
        /// <returns>Array of collision cells.</returns>
        private CollisionCell[] getCellsToCheck(SceneNode node)
        {
            // Collision cells that should be checked against
            CollisionCell[] cellsToCheck = new CollisionCell[9];
            int currentCellIndex = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    cellsToCheck[currentCellIndex] = null;

                    // Check X index bounds
                    int cellXIndex = collisionGrid.GetCellAtPosition(node.Position).XIndex + i;

                    if (cellXIndex >= 0 && cellXIndex < collisionGrid.Cells.GetLength(0))
                    {
                        // Check Y index bounds
                        int cellYIndex = collisionGrid.GetCellAtPosition(node.Position).YIndex + j;

                        if (cellYIndex >= 0 && cellYIndex < collisionGrid.Cells.GetLength(1))
                        {
                            cellsToCheck[currentCellIndex] = collisionGrid.Cells[cellXIndex, cellYIndex];
                        }
                    }

                    currentCellIndex++;
                }
            }

            return cellsToCheck;
        }

        #endregion
    }
}