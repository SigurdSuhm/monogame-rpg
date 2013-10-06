#region Using Statements

using System;

using Microsoft.Xna.Framework;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Physics
{
    /// <summary>
    /// Static class containing extension methods related to physics.
    /// </summary>
    public static class PhysicsExtensions
    {
        /// <summary>
        /// Checks if an entity collides with a tile.
        /// </summary>
        /// <param name="entity">Entity to check.</param>
        /// <param name="tile">Tile to check against.</param>
        /// <returns>True if a collision is found.</returns>
        public static bool CollidesWith(this Entity entity, Tile tile)
        {
            // Seperate values for collision on X and Y axis
            bool collidesOnX = false;
            bool collidesOnY = false;

            // Get vector between player position and tile position
            Vector2 diffVector = entity.Position - tile.Position;

            // Check for collision on X axis according to screen coordinates
            if (diffVector.X < 0)
                collidesOnX = Math.Abs(diffVector.X) < entity.Image.ScreenDimensions.X;
            else
                collidesOnX = Math.Abs(diffVector.X) < tile.Dimensions.X;

            // Check for collision on Y axis according to screen coordinates
            if (diffVector.Y < 0)
                collidesOnY = Math.Abs(diffVector.Y) < entity.Image.ScreenDimensions.Y;
            else
                collidesOnY = Math.Abs(diffVector.Y) < tile.Dimensions.Y;

            return collidesOnX && collidesOnY;
        }
    }
}