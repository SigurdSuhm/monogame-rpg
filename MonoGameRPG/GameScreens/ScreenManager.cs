#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;

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
        // Game screen to transition to
        private string newScreen;
        // Indicates if the screen manager is transitioning
        private bool transitioning = false;
        // Overlay texture used for screen transitions
        private Image transitionTextureImage;

        // Reference to the graphics device
        private GraphicsDevice graphicsDevice;
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

            // Get graphics device reference from the game
            graphicsDevice = BaseGame.Instance.GraphicsDevice;
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

            // Create texture for screen transitions
            Texture2D transitionTexture = new Texture2D(graphicsDevice, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height,
                false, SurfaceFormat.Color);

            Color[] colors = new Color[transitionTexture.Width * transitionTexture.Height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = Color.Black;

            transitionTexture.SetData(colors);
            transitionTextureImage = new Image(transitionTexture);
            transitionTextureImage.LoadContent(contentManager);
            transitionTextureImage.Alpha = 0.0f;

            // Pulse effect used for fading during screen transitions
            PulseEffect pulseEffect = new PulseEffect();
            pulseEffect.MaxAlphaReached += transition_FadeFull;
            pulseEffect.MinAlphaReached += transition_Done;
            transitionTextureImage.AddEffect<PulseEffect>(ref pulseEffect, "PulseEffect");

            transitionTextureImage.DeactivateEffect("PulseEffect");

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

            transitionTextureImage.UnloadContent();
        }

        /// <summary>
        /// Updates the current screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update current game screen
            if (currentScreen != null && !transitioning)
                currentScreen.Update(gameTime);

            // Update transition texture
            transitionTextureImage.Update(gameTime);
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

            // Draw transition texture
            transitionTextureImage.Draw(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Changes to another game screen.
        /// </summary>
        /// <param name="newScreen">Name of the screen to change to.</param>
        public void ChangeScreen(string newScreen)
        {
            if (currentScreen != null)
            {
                this.newScreen = newScreen;
                transitioning = true;

                transitionTextureImage.Alpha = 0.0f;
                transitionTextureImage.ActivateEffect("PulseEffect");
            }
            else
                setNewScreen(newScreen);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Unloads the current screen and loads a new one.
        /// </summary>
        private void setNewScreen(string newScreen)
        {
            // Unload content for the current screen
            if (currentScreen != null)
                currentScreen.UnloadContent();

            // Create instance of the new screen type
            currentScreen = (GameScreen)Activator.CreateInstance(Type.GetType("MonoGameRPG.GameScreens." + newScreen));
            // Load content for the new screen
            currentScreen.LoadContent(contentManager);
        }

        /// <summary>
        /// Fired when the transition texture reaches 1.0 alpha during transitions.
        /// </summary>
        private void transition_FadeFull()
        {
            setNewScreen(newScreen);
        }

        /// <summary>
        /// Fired when the transition texture reaches 0.0 af a transition.
        /// </summary>
        private void transition_Done()
        {
            transitioning = false;
            transitionTextureImage.DeactivateEffect("PulseEffect");
        }

        #endregion
    }
}
