#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Base class used for game entities such as the player, enemies, etc.
    /// </summary>
    public abstract class Entity : IDrawable, IUpdateable
    {
        #region Fields

        // Entity position
        private Vector2 position;

        // Entity image
        protected Image image;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the position of the entity.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set 
            { 
                position = value;
                image.Position = position;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Entity()
        {
            position = Vector2.Zero;
        }

        /// <summary>
        /// Constructor with initial position value.
        /// </summary>
        /// <param name="position">Initial position of the entity.</param>
        public Entity(Vector2 position)
        {
            this.position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the entity.
        /// </summary>
        /// <param name="contentManager">Content manager object</param>
        public virtual void LoadContent(ContentManager contentManager)
        {
            // Load image object content
            if (image != null)
                image.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the entity.
        /// </summary>
        public virtual void UnloadContent()
        {
            // Unload image object content
            if (image != null)
                image.UnloadContent();
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws the entity to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            // Draw image object
            if (image != null)
                image.Draw(spriteBatch);
        }

        #endregion
    }
}