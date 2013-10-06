#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Represents the player in the gameplay screen.
    /// </summary>
    public class Player : Entity
    {
        #region Constants

        // Path to the player texture
        private const string TEXTURE_PATH = "Textures/Player";
        // Path to the animation Xml file
        private const string ANIMATION_FILE_NAME = "Player.xml";

        // TODO: FOR TESTING PURPOSES
        private const float PLAYER_SPEED = 120.0f;

        #endregion

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Player()
            : base("Player")
        {
            // Create image object with texture path
            image = new Image(TEXTURE_PATH);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the player.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            // Load animation data
            image.AnimationManager.LoadAnimationData(ANIMATION_FILE_NAME);
        }

        /// <summary>
        /// Unloads all content associated with the player object.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update method called every frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values</param>
        public override void Update(GameTime gameTime)
        {
            // Handle player input
            handleInput(gameTime);

            base.Update(gameTime);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Handles all player movement based on input.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        private void handleInput(GameTime gameTime)
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
                Position = new Vector2((float)(Position.X - PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds),
                    Position.Y);
                playerMoving = true;
                playerDirection.X = -1;
            }
            else if (InputManager.Instance.KeyDown(Keys.Right))
            {
                Position = new Vector2((float)(Position.X + PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds),
                    Position.Y);
                playerMoving = true;
                playerDirection.X = 1;
            }

            if (InputManager.Instance.KeyDown(Keys.Down))
            {
                Position = new Vector2(Position.X,
                    (float)(Position.Y + PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds));
                playerMoving = true;
                playerDirection.Y = 1;
            }
            else if (InputManager.Instance.KeyDown(Keys.Up))
            {
                Position = new Vector2(Position.X,
                    (float)(Position.Y - PLAYER_SPEED * gameTime.ElapsedGameTime.TotalSeconds));
                playerMoving = true;
                playerDirection.Y = -1;
            }

            if (!playerMoving)
                image.AnimationManager.StopAnimation();
            else
            {
                // Determine which animation should be playing
                if (playerDirection.X == 1)
                {
                    image.AnimationManager.PlayAnimation("WalkRight");
                }
                else if (playerDirection.X == -1)
                {
                    image.AnimationManager.PlayAnimation("WalkLeft");
                }
                else
                {
                    if (playerDirection.Y == 1)
                    {
                        image.AnimationManager.PlayAnimation("WalkDown");
                    }
                    else if (playerDirection.Y == -1)
                    {
                        image.AnimationManager.PlayAnimation("WalkUp");
                    }
                }
            }
        }

        #endregion
    }
}
