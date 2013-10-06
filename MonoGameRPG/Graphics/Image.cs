#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// Sprite class used for drawing 2D textures to the screen.
    /// </summary>
    public class Image : IDrawable, IUpdateable
    {
        #region Fields

        // Image texture
        private Texture2D texture;
        // Texture path
        private string texturePath;

        // Source rectangle defining what part of the image is drawn
        private Rectangle sourceRect;
        // Dimensions of the texture
        private Dimensions2 dimensions;
        // Dimensions of the image when drawn on screen
        protected Dimensions2 screenDimensions;
        // Image position on the screen
        private Vector2 position;

        // Image alpha value
        private float alpha;

        // Determines if the image should be drawn
        private bool visible = true;

        // Dictionary containing image effects for the image
        private Dictionary<string, ImageEffect> imageEffectDictionary;

        // Manager for sprite sheet animations
        private AnimationManager animationManager;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source rectangle of the image.
        /// </summary>
        public Rectangle SourceRect
        {
            get { return sourceRect; }
            set 
            { 
                sourceRect = value;
                // Update screen dimensions
                screenDimensions.X = sourceRect.Width;
                screenDimensions.Y = sourceRect.Height;
            }
        }

        /// <summary>
        /// Gets the dimensions of the image texture.
        /// </summary>
        public Dimensions2 Dimensions
        {
            get { return dimensions; }
        }

        /// <summary>
        /// Gets the dimensions of the image on screen.
        /// </summary>
        public Dimensions2 ScreenDimensions
        {
            get { return screenDimensions; }
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
        /// Gets or sets the alpha value of the image.
        /// </summary>
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        /// <summary>
        /// Gets or sets the visibility flag.
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public AnimationManager AnimationManager
        {
            get { return animationManager; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default image constructor.
        /// </summary>
        /// <param name="texturePath">Path to the image texture.</param>
        public Image(string texturePath)
        {
            texture = null;
            this.texturePath = texturePath;
            position = Vector2.Zero;
            alpha = 1.0f;

            imageEffectDictionary = new Dictionary<string, ImageEffect>();

            animationManager = new AnimationManager(this);
        }

        /// <summary>
        /// Creates a new image from an existing texture.
        /// </summary>
        /// <param name="texture">Texture to use for the image.</param>
        public Image(Texture2D texture)
        {
            this.texture = texture;
            texturePath = String.Empty;
            position = Vector2.Zero;
            alpha = 1.0f;

            imageEffectDictionary = new Dictionary<string, ImageEffect>();

            animationManager = new AnimationManager(this);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the texture for the image object.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            if (texture == null)
            {
                // Load texture from the texture path
                texture = contentManager.Load<Texture2D>(texturePath);
            }

            // Set image dimensions
            dimensions = new Dimensions2(texture.Width, texture.Height);
            screenDimensions = dimensions;
            // Set default source rectangle
            sourceRect = new Rectangle(0, 0, texture.Width, texture.Height); 
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
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update animation manager
            animationManager.Update(gameTime);

            // Update each active image effect
            foreach (ImageEffect effect in imageEffectDictionary.Values)
            {
                if (effect.IsActive)
                    effect.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the image to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object for drawing the texture.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the image
            spriteBatch.Draw(texture, position, sourceRect, Color.White * alpha);
        }

        /// <summary>
        /// Draws the specified part of the image to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            // Draw the image
            spriteBatch.Draw(texture, position, sourceRect, Color.White * alpha);
        }

        /// <summary>
        /// Draws the specified part of the image to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        /// <param name="sourceRect">The part of the texture to draw.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Rectangle imageSourceRect)
        {
            // Draw the image
            spriteBatch.Draw(texture, position, imageSourceRect, Color.White * alpha);
        }

        /// <summary>
        /// Adds an effect of the type T to the image effects of the image.
        /// </summary>
        /// <typeparam name="T">Type of the ImageEffect</typeparam>
        /// <param name="effect">Effect reference - Will be instantiated if null</param>
        public void AddEffect<T>(ref T effect, string key)
        {
            // Check if the effect is already instantiated
            if (effect == null)
                effect = (T)Activator.CreateInstance(typeof(T));
            
            (effect as ImageEffect).IsActive = true;
            (effect as ImageEffect).LoadContent(this);

            // Add effect to dictionary
            imageEffectDictionary.Add(key, effect as ImageEffect);
        }

        /// <summary>
        /// Activates an image effect.
        /// </summary>
        /// <param name="effect">Name of the image effect.</param>
        public void ActivateEffect(string effect)
        {
            if (imageEffectDictionary.ContainsKey(effect))
            {
                if (!imageEffectDictionary[effect].IsActive)
                {
                    // Activate effect and load content
                    imageEffectDictionary[effect].IsActive = true;
                    imageEffectDictionary[effect].LoadContent(this);
                }
            }
            else
            {
                // Effect was not found in the dictionary
                throw new Exception("The specified effect (" + effect + ") was not found in the effects of the image.");
            }
        }

        /// <summary>
        /// Activates an image effect.
        /// </summary>
        /// <param name="effect">Name of the image effect.</param>
        public void DeactivateEffect(string effect)
        {
            if (imageEffectDictionary.ContainsKey(effect))
            {
                if (imageEffectDictionary[effect].IsActive)
                {
                    // Activate effect and load content
                    imageEffectDictionary[effect].IsActive = false;
                    imageEffectDictionary[effect].UnloadContent();
                }
            }
            else
            {
                // Effect was not found in the dictionary
                throw new Exception("The specified effect (" + effect + ") was not found in the effects of the image.");
            }
        }

        #endregion
    }
}
