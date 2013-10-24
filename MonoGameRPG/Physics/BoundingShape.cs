#region Using Statements

using System;

using Microsoft.Xna.Framework;

using MonoGameRPG.Gameplay;

#endregion

namespace MonoGameRPG.Physics
{
    /// <summary>
    /// The bounding shape object is used for determining collisions between objects in 2D.
    /// </summary>
    public abstract class BoundingShape : IUpdateable
    {
        #region Fields

        // Entity that the bounding shape is attached to
        protected Entity attachedEntity;
        // Vector describing the initial offset between the entity and the shape
        protected Vector2 offsetVector;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public BoundingShape()
        {
            attachedEntity = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the bounding shape collides with another bounding shape.
        /// </summary>
        /// <param name="shape">Shape to check against.</param>
        /// <returns>True if a collision was found.</returns> 
        public abstract bool CollidesWith(BoundingShape shape);

        /// <summary>
        /// Update logic called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Attaches the bounding shape to an entity.
        /// </summary>
        /// <param name="entity">Entity to attach the shape to.</param> 
        public virtual void AttachToEntity(Entity entity)
        {
            attachedEntity = entity;
        }

        /// <summary>
        /// Checks for a collision between two bounding circles.
        /// </summary>
        /// <param name="circle1">First bounding circle.</param>
        /// <param name="circle2">Seconds bounding circle.</param>
        /// <returns>True if a collision was found.</returns>
        public static bool CollisionCircleCircle(BoundingCircle circle1, BoundingCircle circle2)
        {
            float radius = circle1.Radius + circle2.Radius;
            radius *= radius;

            return radius < (Math.Pow((circle1.Center.X + circle2.Center.X), 2.0f) +
                Math.Pow((circle1.Center.Y + circle2.Center.Y), 2.0f));
        }

        /// <summary>
        /// Checks for a collision between a bounding circle and an axis-aligned bounding box.
        /// </summary>
        /// <param name="circle">Bounding circle.</param>
        /// <param name="box">Axis-aligned bounding box.</param>
        /// <returns>True if a collision was found.</returns>
        public static bool CollisionCircleAABox(BoundingCircle circle, BoundingBoxAA box)
        {
            // Find the point within the box that is closest to the circle center
            Vector2 closestPoint = box.Min + (circle.Center - box.Min);
            closestPoint.X = MathHelper.Clamp(closestPoint.X, box.Min.X, box.Max.X);
            closestPoint.Y = MathHelper.Clamp(closestPoint.Y, box.Min.Y, box.Max.Y);

            // Check if the point closest to the circle center is within the circle
            return (closestPoint - circle.Center).Length() < circle.Radius;
        }

        /// <summary>
        /// Checks for a collisions between two axis-aligned bounding boxes.
        /// </summary>
        /// <param name="box1">First bounding box.</param>
        /// <param name="box2">Second bounding box.</param>
        /// <returns>True if a collision was found.</returns>
        public static bool CollisionAABoxAABox(BoundingBoxAA box1, BoundingBoxAA box2)
        {
            // Seperating Axis Theorem is used for determining collisions
            if (box1.Max.X < box2.Min.X || box1.Min.X > box2.Max.X)
                return false;
            if (box1.Max.Y < box2.Min.Y || box1.Min.Y > box2.Max.Y)
                return false;

            return true;
        }

        #endregion
    }
}