#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// Base class for all objects within a game scene.
    /// </summary>
    public abstract class SceneNode : IDrawable, IUpdateable
    {
        #region Fields

        // Unique name of the scene node
        private string name;
        // Position of the scene node relative to the entire scene
        protected Vector2 position;
        // Is the scene node static?
        private bool isStatic;

        // Collision grid cell that the node belongs to
        private CollisionCell collisionCell;
        // Collision grid cells that we should check for collisions
        private CollisionCell[] collisionCellsToCheck;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the scene node.
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets or sets the position of the scene node.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// Gets the value indicating if the scene node is static.
        /// </summary>
        public bool IsStatic
        {
            get { return isStatic; }
        }

        /// <summary>
        /// Gets or sets the collision cell for the scene node.
        /// </summary>
        public CollisionCell CollisionCell
        {
            get { return collisionCell; }
            set { collisionCell = value; }
        }

        /// <summary>
        /// Gets or sets the array of collision cells that we should check for collisions.
        /// </summary>
        public CollisionCell[] CollisionCellsToCheck
        {
            get { return collisionCellsToCheck; }
            set { collisionCellsToCheck = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name">Unique scene node name.</param>
        public SceneNode(string name, bool isStatic)
        {
            this.name = name;
            this.isStatic = isStatic;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Update logic called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the scene node.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Draws the scene node at a position relative to the scene.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        /// <param name="scenePosition">Position of the scene.</param>
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 scenePosition);

        /// <summary>
        /// Loads all content for the scene node.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public abstract void LoadContent(ContentManager contentManager);

        /// <summary>
        /// Unloads all content for the scene node.
        /// </summary>
        public abstract void UnloadContent();

        #endregion
    }
}