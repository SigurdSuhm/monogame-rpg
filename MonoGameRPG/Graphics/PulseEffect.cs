#region Using Statements

using Microsoft.Xna.Framework;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Pulse effect making the image alpha go between 0 and 1.
    /// </summary>
    public class PulseEffect : ImageEffect
    {
        #region Fields

        // Pulse effect speed
        private float pulseSpeed = 1.0f;

        // Pulse effect maximum alpha
        private float maxAlpha = 1.0f;
        // Pulse effect minimum alpha
        private float minAlpha = 0.0f;

        // Indicates if the alpha is currently increasing
        private bool increasing;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the pulse effect speed.
        /// </summary>
        public float PulseSpeed
        {
            get { return pulseSpeed; }
            set { pulseSpeed = value; }
        }

        /// <summary>
        /// Gets or sets the maximum alpha value.
        /// </summary>
        public float MaxAlpha
        {
            get { return maxAlpha; }
            set { maxAlpha = value; }
        }

        /// <summary>
        /// Gets or sets the minimum alpha value.
        /// </summary>
        public float MinAlpha
        {
            get { return minAlpha; }
            set { minAlpha = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PulseEffect()
        {
            increasing = false;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event fired when the active state of the effect changes.
        /// </summary>
        /// <param name="newActiveState">New active state value.</param>
        protected override void OnActiveStateChanged(bool newActiveState)
        {
            // Reset alpha when deactivated
            if (newActiveState == false)
                image.Alpha = maxAlpha;

            base.OnActiveStateChanged(newActiveState);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the pulse effect.
        /// </summary>
        /// <param name="image">Image that the effect is associated with.</param>
        public override void LoadContent(Image image)
        {
            base.LoadContent(image);
        }

        /// <summary>
        /// Unloads all content asoociated with the effect.
        /// </summary>
        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Increase or decrease the image alpha
            if (increasing)
            {
                image.Alpha += pulseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Check if alpha should start decreasing
                if (image.Alpha >= maxAlpha)
                {
                    increasing = false;
                    image.Alpha = maxAlpha;
                }
            }
            else
            {
                image.Alpha -= pulseSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Check if alpha should start increasing
                if (image.Alpha <= minAlpha)
                {
                    increasing = true;
                    image.Alpha = minAlpha;
                }
            }

            base.Update(gameTime);
        }

        #endregion
    }
}
