#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Handles the active screen. Switching screens should happen through
    /// an instance of this object.
    /// </summary>
    public class ScreenManager : IUpdateable, IDrawable
    {
        #region Fields

        // Singleton instance
        private static ScreenManager instance;

        // Current game screen
        private GameScreen currentScreen;

        // Reference to the game content manager
        private ContentManager contentManager;

        #endregion

        #region Properties

        // Property for the singleton instance
        public static ScreenManager Instance
        {
            get
            {
                // Check if the singleton instance has been created
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default screen manager constructor.
        /// </summary>
        private ScreenManager()
        {
            currentScreen = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the active screen.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            // Set content manager reference
            this.contentManager = contentManager;

            // Load content for the current screen
            if (currentScreen != null)
                currentScreen.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content for the active screen.
        /// </summary>
        public void UnloadContent()
        {
            // Unload content for the current screen
            if (currentScreen != null)
                currentScreen.UnloadContent();
        }

        /// <summary>
        /// Updates the current screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update current game screen
            if (currentScreen != null)
                currentScreen.Update(gameTime);
        }

        /// <summary>
        /// Draws all content from the active screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object for drawing sprites.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            // Draw current game screen
            if (currentScreen != null)
                currentScreen.Draw(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Changes to another game screen.
        /// </summary>
        /// <param name="newScreen">Name of the screen to change to.</param>
        public void ChangeScreen(string newScreen)
        {
            // Unload content for the current screen
            if (currentScreen != null)
                currentScreen.UnloadContent();

            // Create instance of the new screen type
            currentScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoGameRPG.GameScreens." + newScreen));
            // Load content for the new screen
            currentScreen.LoadContent(contentManager);
        }

        #endregion
    }
}
