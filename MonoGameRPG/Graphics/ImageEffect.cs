#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

#endregion

namespace MonoGameRPG
{
    /// <summary>
    /// Base class for image effects.
    /// </summary>
    public abstract class ImageEffect : IUpdateable
    {
        #region Fields

        // The image that the effect is associated with
        protected Image image;

        // Indicates if the effect is currently active
        protected bool isActive = true;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the active value for the image effect.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set
            { 
                isActive = value;
                OnActiveStateChanged(isActive);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="image">The image that the effect is associated with.</param>
        public ImageEffect()
        {
        }

        #endregion

        #region Events

        // Event type for when the active state of the effect is changed
        public delegate void ActiveStateChangedEventHandler();
        public event ActiveStateChangedEventHandler ActiveStateChanged;

        /// <summary>
        /// Event fired when the active state of the effect changes.
        /// </summary>
        /// <param name="newActiveState">New active state value.</param>
        protected virtual void OnActiveStateChanged(bool newActiveState)
        {
            if (ActiveStateChanged != null)
                ActiveStateChanged();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the content for the image effect.
        /// </summary>
        /// <param name="image">Image that the effect is associated with.</param>
        public virtual void LoadContent(Image image)
        {
            this.image = image;
        }

        /// <summary>
        /// Unloads all content associated with the image effect.
        /// </summary>
        public virtual void UnloadContent()
        {
        }

        /// <summary>
        /// Updates the image effect.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public virtual void Update(GameTime gameTime)
        {
        }

        #endregion
    }
}
