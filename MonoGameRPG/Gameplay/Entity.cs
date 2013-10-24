#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;
using MonoGameRPG.Scene;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Base class used for game entities such as the player, enemies, etc.
    /// </summary>
    public abstract class Entity : SceneNode
    {
        #region Fields

        // Entity image
        protected Image image;
        // Bounding shape used for the entity
        private BoundingShape boundingShape;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image object associated with the entity.
        /// </summary>
        public Image Image
        {
            get { return image; }
        }

        /// <summary>
        /// Gets the bounding shape associated with the entity.
        /// </summary>
        public BoundingShape BoundingShape
        {
            get { return boundingShape; }
            protected set
            {
                boundingShape = value;
                boundingShape.AttachToEntity(this);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Entity(string name)
            : base(name, false)
        {
            position = Vector2.Zero;
        }

        /// <summary>
        /// Constructor with initial position value.
        /// </summary>
        /// <param name="position">Initial position of the entity.</param>
        public Entity(string name, Vector2 position)
            : base(name, false)
        {
            this.position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the entity.
        /// </summary>
        /// <param name="contentManager">Content manager object</param>
        public override void LoadContent(ContentManager contentManager)
        {
            // Load image object content
            if (image != null)
                image.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads all content associated with the entity.
        /// </summary>
        public override void UnloadContent()
        {
            // Unload image object content
            if (image != null)
                image.UnloadContent();
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            image.Update(gameTime);

            // Update bounding shape
            if (boundingShape != null)
                boundingShape.Update(gameTime);
        }

        /// <summary>
        /// Draws the entity to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw image object
            if (image != null)
                image.Draw(spriteBatch, position);
        }

        #endregion
    }
}