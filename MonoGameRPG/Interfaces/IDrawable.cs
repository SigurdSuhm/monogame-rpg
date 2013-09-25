#region Using Statements

using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Interface implementing the Draw(SpriteBatch) method
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws the object to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        void Draw(SpriteBatch spriteBatch);
    }
}
