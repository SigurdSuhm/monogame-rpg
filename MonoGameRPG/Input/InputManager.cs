#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoGameRPG.Input
{
    /// <summary>
    /// Input manager class handling all user input.
    /// </summary>
    public class InputManager : IUpdateable
    {
        #region Fields

        // Singleton instance
        private static InputManager instance;

        // Determines if the input manager is accepting input
        private bool active = true;

        // Keyboard states for the previous and current frame
        KeyboardState currentKeyState, prevKeyState;
        // Mouse states for the previous and current frame
        MouseState currentMouseState, prevMouseState;

        // Current position of the mouse
        private Vector2 mousePosition;

        // Player controller for player input abstraction
        private PlayerController playerController;

        #endregion

        #region Properties

        /// <summary>
        /// Property used for accessing the singleton instance.
        /// </summary>
        public static InputManager Instance
        {
            get
            {
                // Check if the singleton instance has been created
                if (instance == null)
                    instance = new InputManager();

                return instance;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current position of the mouse.
        /// </summary>
        public Vector2 MousePosition
        {
            get { return mousePosition; }
        }

        /// <summary>
        /// Gets the player controller.
        /// </summary>
        public PlayerController PlayerController
        {
            get { return playerController; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default input manager constructor.
        /// </summary>
        private InputManager()
        {
            mousePosition = Vector2.Zero;
            playerController = new PlayerController();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            // Set previous key state to the current key state
            prevKeyState = currentKeyState;
            // Set previous mouse state to the current mouse state
            prevMouseState = currentMouseState;

            // Update current key state
            currentKeyState = Keyboard.GetState();
            // Update current mouse state
            currentMouseState = Mouse.GetState();

            // Update mouse position
            mousePosition.X = currentMouseState.X;
            mousePosition.Y = currentMouseState.Y;
        }

        /// <summary>
        /// Checks if a key is currently down.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if the key is currently down, false otherwise.</returns>
        public bool KeyDown(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        /// <summary>
        /// Checks if a key was pressed this frame.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if the key was pressed, false otherwise.</returns>
        public bool KeyPressed(Keys key)
        {
            // Check if the key is down this frame but was up last frame
            return (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key));
        }

        /// <summary>
        /// Checks if a key was released this frame.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if the key was released, false otherwise.</returns>
        public bool KeyReleased(Keys key)
        {
            // Check if the key is up this frame but was down last frame
            return (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key));
        }

        /// <summary>
        /// Checks if the left mouse button is pressed down.
        /// </summary>
        /// <returns>True if the left mouse button is down</returns>
        public bool MouseLeftDown()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks if the left mouse button was pressed this frame.
        /// </summary>
        /// <returns>True if the left mouse button was just pressed</returns>
        public bool MouseLeftPressed()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed &&
                prevMouseState.LeftButton == ButtonState.Released);
        }

        /// <summary>
        /// Checks if the left mouse button was released this frame.
        /// </summary>
        /// <returns>True if the left mouse button was just released</returns>
        public bool MouseLeftReleased()
        {
            return (currentMouseState.LeftButton == ButtonState.Released &&
                prevMouseState.LeftButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks if the right mouse button is pressed down.
        /// </summary>
        /// <returns>True if the right mouse button is down</returns>
        public bool MouseRightDown()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Checks if the right mouse button was pressed this frame.
        /// </summary>
        /// <returns>True if the right mouse button was just pressed</returns>
        public bool MouseRightPressed()
        {
            return (currentMouseState.RightButton == ButtonState.Pressed &&
                prevMouseState.RightButton == ButtonState.Released);
        }

        /// <summary>
        /// Checks if the right mouse button was released this frame.
        /// </summary>
        /// <returns>True if the right mouse button was just released</returns>
        public bool MouseRightReleased()
        {
            return (currentMouseState.RightButton == ButtonState.Released &&
                prevMouseState.RightButton == ButtonState.Pressed);
        }

        /// <summary>
        /// Activates or deactivates the input manager.
        /// </summary>
        /// <param name="active">New active state.</param>
        public void SetActive(bool active)
        {
            this.active = active;
        }

        #endregion
    }
}
