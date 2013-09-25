#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Represents the player in the gameplay screen.
    /// </summary>
    public class Player : Entity
    {
        #region Constants

        // Path to the player texture
        private const string TEXTURE_PATH = "Textures/Player";

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
        }

        /// <summary>
        /// Unloads all content associated with the player object.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
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
