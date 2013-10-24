#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// The UI shown over everything in the gameplay screen.
    /// </summary>
    public class GameplayUI : IUpdateable, IDrawable
    {
        #region Constants

        // Width of the health bar
        private const int HEALTH_BAR_WIDTH = 175;

        #endregion

        #region Fields

        // Reference to the player object
        private Player player;
        // Texture for the player health bar
        private Image healthBarImage;
        // Image for the health bar frame
        private Image healthBarFrameImage;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the player object for the UI.
        /// </summary>
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameplayUI()
        {
            player = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads all content for the gameplay UI.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            Texture2D healthBarTexture = new Texture2D(BaseGame.Instance.GraphicsDevice, 213, 30, false, SurfaceFormat.Color);

            Color[] colors = new Color[healthBarTexture.Width * healthBarTexture.Height];
            for (int i = 0; i < colors.Length; i++)
                colors[i] = Color.Red;

            healthBarTexture.SetData(colors);
            healthBarImage = new Image(healthBarTexture);
            healthBarImage.Position = new Vector2(10, 18);
            healthBarImage.LoadContent(contentManager);

            healthBarFrameImage = new Image("Textures/UI/HealthBarFrame");
            healthBarFrameImage.Position = new Vector2(0, 0);
            healthBarFrameImage.LoadContent(contentManager);
        }

        /// <summary>
        /// Update logic performed every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (player != null)
            {
                healthBarImage.Scale = new Vector2((float)player.CurrentHealth / (float)player.MaxHealth, 1.0f);
            }

            healthBarImage.Update(gameTime);
            healthBarFrameImage.Update(gameTime);
        }

        /// <summary>
        /// Renders the UI to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            healthBarImage.Draw(spriteBatch);
            healthBarFrameImage.Draw(spriteBatch);
        }

        #endregion
    }
}
