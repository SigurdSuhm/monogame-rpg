#region Using Statements

using System;

using Microsoft.Xna.Framework;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Physics
{
    /// <summary>
    /// An axis-aligned bounding box for determining collisions between objects.
    /// </summary>
    public class BoundingBoxAA : BoundingShape
    {
        #region Fields

        // Minimum and maximum bounds of the bounding box
        private Vector2 min, max;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum bounds of the bounding box.
        /// </summary>
        public Vector2 Min
        {
            get { return min; }
        }

        /// <summary>
        /// Gets the maximum bounds of the bounding box.
        /// </summary>
        public Vector2 Max
        {
            get { return max; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new axis-aligned bounding box from minimum and maximum bounds.
        /// </summary>
        /// <param name="min">Minimum bounds.</param>
        /// <param name="max">Maximum bounds.</param>
        public BoundingBoxAA(Vector2 min, Vector2 max)
        {
            this.min = min;
            this.max = max;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for collisions against another bounding shape.
        /// </summary>
        /// <param name="shape">Shape to check against.</param>
        /// <returns>True if a collision was found.</returns>
        public override bool CollidesWith(BoundingShape shape)
        {
            if (shape is BoundingCircle)
                return BoundingShape.CollisionCircleAABox(shape as BoundingCircle, this);
            else if (shape is BoundingBoxAA)
                return BoundingShape.CollisionAABoxAABox(shape as BoundingBoxAA, this);

            throw new Exception("Unknown bounding shape.");
        }

        /// <summary>
        /// Update logic called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Check if the bounding box should be moved with an entity
            if (attachedEntity != null)
            {
                Vector2 minMaxVector = getMinMaxVector();
                min = attachedEntity.Position + offsetVector;
                max = min + minMaxVector;
            }
        }

        /// <summary>
        /// Attaches the bounding box to an entity.
        /// </summary>
        /// <param name="entity">Entity to attach the box to.</param> 
        public override void AttachToEntity(Entity entity)
        {
            if (entity != null)
            {
                offsetVector = min - entity.Position;
            }

            base.AttachToEntity(entity);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Gets the vector between the minimum bounds and the maximum bounds.
        /// </summary>
        /// <returns>2-dimensional vector describing the distance between the bounds.</returns>
        private Vector2 getMinMaxVector()
        {
            Vector2 returnVector = max - min;
            return returnVector;
        }

        #endregion
    }
}