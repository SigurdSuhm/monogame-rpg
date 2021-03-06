﻿#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using MonoGameRPG.Input;

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// Represents individual items in a game menu.
    /// </summary>
    public class MenuItem : IUpdateable, IDrawable
    {
        #region Fields

        // The image associated with the menu item
        private Image image;

        // Menu item image pulse effect
        private PulseEffect pulseEffect;

        // Position of the menu item relative to the menu
        private Vector2 position;

        // Indicates if the menu item has mouse focus
        private bool hasMouseFocus = false;
        // Indicates if the mouse was pressed down on this menu item
        private bool mousePressedOnThis = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the image associated with the menu item.
        /// </summary>
        public Image Image
        {
            get { return image; }
        }

        /// <summary>
        /// Gets or sets the position of the menu item.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set
            {
                // Update position of the image
                position = value;
                image.Position = position;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuItem(string texturePath)
        {
            // Set texture path for the menu item image
            image = new Image(texturePath);

            position = Vector2.Zero;
        }

        #endregion

        #region Events

        // Delegate type for when menu items are activated
        public delegate void ActivatedEventHandler();
        public event ActivatedEventHandler Activated;

        /// <summary>
        /// Activates the current menu item and triggers the associated methods.
        /// </summary>
        public void OnActivate()
        {
            if (Activated != null)
                Activated();
        }

        // Delegate type for when the mouse enters the menu item area
        public delegate void MouseEnterEventHandler();
        public event MouseEnterEventHandler MouseEnter;

        /// <summary>
        /// Calls the MouseEnter event for the menu item.
        /// </summary>
        private void OnMouseEnter()
        {
            hasMouseFocus = true;

            // Activate pulse effect
            image.ActivateEffect("PulseEffect");

            if (MouseEnter != null)
                MouseEnter();
        }

        // Delegate type for when the mouse leaves the menu item area
        public delegate void MouseLeaveEventHandler();
        public event MouseLeaveEventHandler MouseLeave;

        /// <summary>
        /// Calls the MouseEnter event for the menu item.
        /// </summary>
        private void OnMouseLeave()
        {
            hasMouseFocus = false;

            // Deactivate pulse effect
            image.DeactivateEffect("PulseEffect");

            if (MouseLeave != null)
                MouseLeave();
        }

        // Delegate type for when the mouse is clicked on the menu item
        public delegate void MouseClickEventHandler();
        public event MouseClickEventHandler MouseClick;

        /// <summary>
        /// Calls the MouseClick event for the menu item.
        /// </summary>
        private void OnMouseClick()
        {
            if (MouseClick != null)
                MouseClick();

            // Mouse click also calls the activate event
            if (Activated != null)
                OnActivate();

            mousePressedOnThis = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the menu item.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            image.LoadContent(contentManager);

            // Load pulse effect and associate it with the image
            image.AddEffect<PulseEffect>(ref pulseEffect, "PulseEffect");
            pulseEffect.MinAlpha = 0.2f;
            pulseEffect.PulseSpeed = 1.5f;
            pulseEffect.ResetAlphaOnDeactivate = true;
            image.DeactivateEffect("PulseEffect");
        }

        /// <summary>
        /// Unloads all content associated with the menu item.
        /// </summary>
        public void UnloadContent()
        {
            image.UnloadContent();
        }

        /// <summary>
        /// Updates the menu item.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            image.Update(gameTime);

            if (InputManager.Instance.MousePosition.X >= position.X &&
            InputManager.Instance.MousePosition.X <= (position.X + Image.Dimensions.X) &&
            InputManager.Instance.MousePosition.Y >= position.Y &&
            InputManager.Instance.MousePosition.Y <= (position.Y + Image.Dimensions.Y))
            {
                // Call method for the MouseEnter event if the menu item does not currently have mouse focus
                if (!hasMouseFocus)
                    OnMouseEnter();

                if (InputManager.Instance.MouseLeftPressed())
                {
                    // Remember that the mouse was pressed down on this item
                    mousePressedOnThis = true;
                }

                // Check for click event
                if (InputManager.Instance.MouseLeftReleased() && mousePressedOnThis)
                {
                    OnMouseClick();
                }
            }
            else
            {
                // Call method for the MouseLeave event if the menu item currently has mouse focus
                if (hasMouseFocus)
                    OnMouseLeave();

                // Check if the mouse was released outside of the menu item
                // This is done to reset the mousePressedOnThis flag
                if (InputManager.Instance.MouseLeftReleased())
                    mousePressedOnThis = false;
            }
        }

        /// <summary>
        /// Draws the menu item.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }

        #endregion
    }
}
