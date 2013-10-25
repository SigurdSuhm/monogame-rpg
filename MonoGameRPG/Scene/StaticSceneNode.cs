#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Graphics;
using MonoGameRPG.Physics;

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// Represents static scene nodes in a scene.
    /// </summary>
    public class StaticSceneNode : SceneNode
    {
        #region Fields

        // Image used for the scene node
        private Image image;
        // Bounding shape for collision detection
        private BoundingShape boundingShape;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image object associated with the scene node.
        /// </summary>
        public Image Image
        {
            get { return image; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new static scene node from an image.
        /// </summary>
        /// <param name="name">Unique scene node name.</param>
        /// <param name="image">Image for the scene node.</param>
        public StaticSceneNode(string name, Image image)
            : base(name, true)
        {
            this.image = image;
            position = Vector2.Zero;
        }

        /// <summary>
        /// Creates a new static scene node from an image.
        /// </summary>
        /// <param name="name">Unique scene node name.</param>
        /// <param name="image">Image for the scene node.</param>
        /// <param name="position">Scene node position.</param>
        public StaticSceneNode(string name, Image image, Vector2 position)
            : base(name, true)
        {
            this.image = image;
            this.position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the scene node.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public override void LoadContent(ContentManager contentManager)
        {
            if (image != null)
                image.LoadContent(contentManager);
        }

        /// <summary>
        /// Unloads content for the scene node.
        /// </summary>
        public override void UnloadContent()
        {
            if (image != null)
                image.UnloadContent();
        }

        /// <summary>
        /// Update logic called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update image object
            if (image != null)
                image.Update(gameTime);
        }

        /// <summary>
        /// Draws the scene node.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object user for 2D rendering.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw image object
            if (image != null)
                image.Draw(spriteBatch, position);
        }

        /// <summary>
        /// Draws the scene node.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object user for 2D rendering.</param>
        /// <param name="scenePosition"></param>
        public override void Draw(SpriteBatch spriteBatch, Vector2 scenePosition)
        {
            // Draw image object
            if (image != null)
                image.Draw(spriteBatch, position + scenePosition);
        }

        #endregion
    }
}