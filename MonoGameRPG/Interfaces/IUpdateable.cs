#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Interface implementing the Update(GameTime) method.
    /// </summary>
    public interface IUpdateable
    {
        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        void Update(GameTime gameTime);
    }
}
