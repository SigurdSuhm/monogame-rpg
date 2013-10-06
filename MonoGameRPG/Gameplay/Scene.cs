#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGameRPG.Physics;

#endregion

namespace MonoGameRPG.Gameplay
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
        private Dictionary<string, Entity> entityList;
        // Contains snapshots of entity positions from last frame for collisions handling
        private Dictionary<string, Vector2> prevEntityPositions;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of entities in the scene.
        /// </summary>
        public Dictionary<string, Entity> EntityList
        {
            get { return entityList; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Scene(string tileMapPath)
        {
            this.tileMapPath = tileMapPath;
            entityList = new Dictionary<string, Entity>();
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

            // Load content for all entities
            foreach (Entity entity in entityList.Values)
            {
                entity.LoadContent(contentManager);
                // Create previous position for entity
                prevEntityPositions.Add(entity.Name, entity.Position);
            }
        }

        /// <summary>
        /// Unloads all content associated with the scene object.
        /// </summary>
        public void UnloadContent()
        {
            tileMap.UnloadContent();

            // Unload content for all active entities
            foreach (Entity entity in entityList.Values)
                entity.UnloadContent();
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            tileMap.Update(gameTime);

            // Update all active entities
            foreach (Entity entity in entityList.Values)
            {
                // Store old entity position before updating
                prevEntityPositions[entity.Name] = entity.Position;
                entity.Update(gameTime);

                // Check for tile collision
                foreach (Tile tile in tileMap.TileArray)
                {
                    if (tile.CollisionValue == CollisionValue.Solid)
                    {
                        // If a collision is found return to old entity position
                        if (entity.CollidesWith(tile))
                            entity.Position = prevEntityPositions[entity.Name];
                    }
                }
            }
        }

        /// <summary>
        /// Draws the entire scene to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);

            // Draw all active entities
            foreach (Entity entity in entityList.Values)
                entity.Draw(spriteBatch);
        }

        #endregion
    }
}