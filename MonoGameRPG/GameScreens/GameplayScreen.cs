#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Gameplay;
using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Game screen used for actual gameplay.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        #region Constants

        #endregion

        #region Fields

        // Scene manager used for managing and changing scenes
        private SceneManager sceneManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameplayScreen()
            : base()
        {
            sceneManager = new SceneManager();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads all content for the gameplay screen.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public override void LoadContent(ContentManager contentManager)
        {
            sceneManager.LoadContent(contentManager);

            // TODO: FOR TESTING PURPOSES
            sceneManager.ChangeScene("TestScene");

            base.LoadContent(contentManager);

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content loaded for gameplay screen.");
        }

        /// <summary>
        /// Unloads all content associated with the gameplay screen.
        /// </summary>
        public override void UnloadContent()
        {
            sceneManager.UnloadContent();

            base.UnloadContent();

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content unloaded for gameplay screen.");
        }

        /// <summary>
        /// Update method called once every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: FOR TESTING PURPOSES
            if (InputManager.Instance.KeyPressed(Keys.Escape))
                BaseGame.Instance.Exit();

            // Update scene manager
            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D renderin.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw current scene
            sceneManager.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
