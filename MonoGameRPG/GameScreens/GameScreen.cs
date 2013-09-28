#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Base class for all game screens.
    /// </summary>
    public abstract class GameScreen : IUpdateable, IDrawable
    {
        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public GameScreen()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for the game screen.
        /// </summary>
        public virtual void LoadContent(ContentManager contentManager)
        {
        }

        /// <summary>
        /// Unloads all the content of the game screen.
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the game screen.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draws the game screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for drawing sprites.</param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        #endregion
    }
}
