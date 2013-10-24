#region Using Statements

using System;

using Microsoft.Xna.Framework;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Physics
{
    /// <summary>
    /// Circular bounding shape for determining collisions between objects.
    /// </summary>
    public class BoundingCircle : BoundingShape
    {
        #region Fields

        // Center of the bounding circle
        private Vector2 center;
        // Radius of the bounding circle
        private float radius;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the center point of the bounding circle.
        /// </summary>
        public Vector2 Center
        {
            get { return center; }
        }

        /// <summary>
        /// Gets the radius of the bounding circle.
        /// </summary>
        public float Radius
        {
            get { return radius; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new bounding circle object from a center and a radius.
        /// </summary>
        /// <param name="center">Center point of the bounding circle.</param>
        /// <param name="radius">Radius of the bounding circle.</param>
        public BoundingCircle(Vector2 center, float radius)
        {
            this.center = center;
            this.radius = radius;
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
                return BoundingShape.CollisionCircleCircle(shape as BoundingCircle, this);
            else if (shape is BoundingBoxAA)
                return BoundingShape.CollisionCircleAABox(this, shape as BoundingBoxAA);

            throw new Exception("Unknown bounding shape.");
        }

        /// <summary>
        /// Update logic called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Check if the bounding circle should be moved with an entity
            if (attachedEntity != null)
            {
                center = attachedEntity.Position + offsetVector;
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
                offsetVector = center - entity.Position;
            }

            base.AttachToEntity(entity);
        }

        #endregion
    }
}