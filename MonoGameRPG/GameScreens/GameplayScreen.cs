#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Game screen used for actual gameplay.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        #region Fields

        // Player object
        private Player player;

        // Indicates if the game is paused
        // This halts all input related to player movement, AI etc.
        private bool paused = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public GameplayScreen()
            : base()
        {
            player = new Player();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads all content for the gameplay screen.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public override void LoadContent(ContentManager contentManager)
        {
            // Load content for the player
            player.LoadContent(contentManager);

            base.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the gameplay screen.
        /// </summary>
        public override void UnloadContent()
        {
            player.UnloadContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Update method called once every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (!paused)
                handlePlayerInput(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D renderin.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw player object
            player.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles all input related to the player object.
        /// </summary>
        private void handlePlayerInput(GameTime gameTime)
        {
            // TODO: PLAYER INPUT FOR TESTING PURPOSES
            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                player.Position = new Vector2((float)(player.Position.X - 80.0 * gameTime.ElapsedGameTime.TotalSeconds),
                    player.Position.Y);
            }
            if (InputManager.Instance.KeyDown(Keys.Right))
            {
                player.Position = new Vector2((float)(player.Position.X + 80.0 * gameTime.ElapsedGameTime.TotalSeconds),
                    player.Position.Y);
            }
            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                player.Position = new Vector2(player.Position.X,
                    (float)(player.Position.Y + 80.0 * gameTime.ElapsedGameTime.TotalSeconds));
            }
            if (InputManager.Instance.KeyDown(Keys.Up))
            {
                player.Position = new Vector2(player.Position.X,
                    (float)(player.Position.Y - 80.0 * gameTime.ElapsedGameTime.TotalSeconds));
            }
        }

        #endregion
    }
}
