#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Graphics;
using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Splash screen object. This is the very first game screen shown.
    /// </summary>
    public class SplashScreen : GameScreen
    {
        #region Constants

        // Number of milliseconds the screen should be shown
        private const double SPLASH_SCREEN_DURATION = 3500;

        #endregion

        #region Fields

        // Splash screen background
        private Image background;

        // Number of milliseconds the splash screen has been shown
        private double timeShown = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Splash screen default constructor.
        /// </summary>
        public SplashScreen()
            : base()
        {
            background = new Image("Textures/SplashScreen");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for the game screen.
        /// </summary>
        public override void LoadContent(ContentManager contentManager)
        {
            // Load content for the backgrounds
            background.LoadContent(contentManager);

            base.LoadContent(contentManager);

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content loaded for splash screen.");
        }

        /// <summary>
        /// Unloads all the content of the game screen.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content unloaded for splash screen.");
        }

        /// <summary>
        /// Updates the game screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            timeShown += gameTime.ElapsedGameTime.TotalMilliseconds;

            // TODO: FOR TESTING PURPOSES
            if (InputManager.Instance.KeyPressed(Keys.Space) || timeShown > SPLASH_SCREEN_DURATION)
            {
                ScreenManager.Instance.ChangeScreen("MenuScreen");
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the game screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for drawing sprites.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
