#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Represents the player in the gameplay screen.
    /// </summary>
    public class Player : Entity, IUpdateable
    {
        #region Constants

        // Path to the player texture
        private const string TEXTURE_PATH = "Textures/Player";
        // Path to the animation Xml file
        private const string ANIMATION_FILE_NAME = "Player.xml";

        #endregion

        #region Fields

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image object associated with the player.
        /// </summary>
        public Image Image
        {
            get { return image; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Player()
            : base()
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
        public void Update(GameTime gameTime)
        {
            image.Update(gameTime);
        }

        /// <summary>
        /// Draws the player to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw image
            image.Draw(spriteBatch);
        }

        #endregion
    }
}
