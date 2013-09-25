#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Sprite class used for drawing 2D textures to the screen.
    /// </summary>
    public class Image : IDrawable
    {
        #region Fields

        // Image texture
        private Texture2D texture;
        // Texture path
        private string texturePath;

        // Dimensions of the texture
        private Vector2 dimensions;
        // Image position on the screen
        private Vector2 position;

        // Determines if the image should be drawn
        private bool visible = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the dimensions of the image texture.
        /// </summary>
        public Vector2 Dimensions
        {
            get { return dimensions; }
        }

        /// <summary>
        /// Gets or sets the position of the image.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets or sets the visibility flag.
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default image constructor.
        /// </summary>
        /// <param name="texturePath"></param>
        public Image(string texturePath)
        {
            this.texturePath = texturePath;
            position = new Vector2(0, 0);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the texture for the image object.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            // Load texture from the texture path
            texture = contentManager.Load<Texture2D>(texturePath);

            // Set image dimensions
            dimensions = new Vector2(texture.Width, texture.Height);
        }

        /// <summary>
        /// Unloads content for the image.
        /// </summary>
        public void UnloadContent()
        {
            // Dispose of the loaded texture
            texture.Dispose();
        }

        /// <summary>
        /// Draws the image to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object for drawing the texture.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the image
            spriteBatch.Draw(texture, position, Color.White);
        }

        #endregion
    }
}
