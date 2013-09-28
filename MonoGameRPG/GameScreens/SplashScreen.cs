#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Splash screen object. This is the very first game screen shown.
    /// </summary>
    public class SplashScreen : GameScreen
    {
        #region Fields

        // Splash screen background
        private Image background;

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
        }

        /// <summary>
        /// Unloads all the content of the game screen.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Updates the game screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: FOR TESTING PURPOSES
            if (InputManager.Instance.KeyPressed(Keys.Space))
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
