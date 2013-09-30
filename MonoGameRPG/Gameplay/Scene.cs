#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
        // List of entities in the scene
        private List<Entity> entityList;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the list of entities in the scene.
        /// </summary>
        public List<Entity> EntityList
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
            entityList = new List<Entity>();
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
            foreach (Entity entity in entityList)
                entity.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the scene object.
        /// </summary>
        public void UnloadContent()
        {
            tileMap.UnloadContent();

            // Unload content for all active entities
            foreach (Entity entity in entityList)
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
            foreach (Entity entity in entityList)
                entity.Update(gameTime);
        }

        /// <summary>
        /// Draws the entire scene to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);

            // Draw all active entities
            foreach (Entity entity in entityList)
                entity.Draw(spriteBatch);
        }

        #endregion
    }
}