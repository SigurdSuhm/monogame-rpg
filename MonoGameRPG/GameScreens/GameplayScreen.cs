#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Game screen used for actual gameplay.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        #region Constants

        // TODO: FOR TESTING PURPOSES
        private const float PLAYER_SPEED = 120.0f;

        #endregion

        #region Fields

        // Player object
        private Player player;

        // Indicates if the game is paused
        // This halts all input related to player movement, AI etc.
        private bool paused = false;

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
            player.Image.AnimationManager.Looping = true;

            // Create scene manager object
            sceneManager = new SceneManager();
            sceneManager.LoadContent(contentManager);
            // TODO: FOR TESTING PURPOSES
            sceneManager.ChangeScene("TestScene");

            base.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the gameplay screen.
        /// </summary>
        public override void UnloadContent()
        {
            player.UnloadContent();

            sceneManager.UnloadContent();

            base.UnloadContent();
        }

        /// <summary>
        /// Update method called once every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            if (!paused)
                handlePlayerInput(gameTime);

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
            // Indicates if the player is moving this frame
            bool playerMoving = false;
            // Vector describing the current player direction - Used for animation
            Vector2 playerDirection;
            playerDirection.X = 0;
            playerDirection.Y = 0;

            // TODO: PLAYER INPUT FOR TESTING PURPOSES
            if (InputManager.Instance.KeyDown(Keys.Left))
            {
                player.Position = new Vector2((float)(player.Position.X - PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds),
                    player.Position.Y);
                playerMoving = true;
                playerDirection.X = -1;
            }
            else if (InputManager.Instance.KeyDown(Keys.Right))
            {
                player.Position = new Vector2((float)(player.Position.X + PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds),
                    player.Position.Y);
                playerMoving = true;
                playerDirection.X = 1;
            }

            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                player.Position = new Vector2(player.Position.X,
                    (float)(player.Position.Y + PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds));
                playerMoving = true;
                playerDirection.Y = 1;
            }
            else if (InputManager.Instance.KeyDown(Keys.Up))
            {
                player.Position = new Vector2(player.Position.X,
                    (float)(player.Position.Y - PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds));
                playerMoving = true;
                playerDirection.Y = -1;
            }

            if (!playerMoving)
                player.Image.AnimationManager.StopAnimation();
            else
            {
                // Determine which animation should be playing
                if (playerDirection.X == 1)
                {
                    player.Image.AnimationManager.PlayAnimation("WalkRight");
                }
                else if (playerDirection.X == -1)
                {
                    player.Image.AnimationManager.PlayAnimation("WalkLeft");
                }
                else
                {
                    if (playerDirection.Y == 1)
                    {
                        player.Image.AnimationManager.PlayAnimation("WalkDown");
                    }
                    else if (playerDirection.Y == -1)
                    {
                        player.Image.AnimationManager.PlayAnimation("WalkUp");
                    }
                }
            }
        }

        #endregion
    }
}
