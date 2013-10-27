#region Using Statements

using Microsoft.Xna.Framework.Input;

#endregion

namespace MonoGameRPG.Input
{
    /// <summary>
    /// Stores settings for correlation between input and player actions.
    /// </summary>
    public class PlayerController
    {
        #region Fields

        // Movement keys
        Keys moveDown, moveUp, moveLeft, moveRight;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the key used to move the player down.
        /// </summary>
        public Keys MoveDown
        {
            get { return moveDown; }
        }

        /// <summary>
        /// Gets the key used to move the player up.
        /// </summary>
        public Keys MoveUp
        {
            get { return moveUp; }
        }

        /// <summary>
        /// Gets the key used to move the player left.
        /// </summary>
        public Keys MoveLeft
        {
            get { return moveLeft; }
        }

        /// <summary>
        /// Gets the key used to move the player right.
        /// </summary>
        public Keys MoveRight
        {
            get { return moveRight; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PlayerController()
        {
            moveDown = Keys.S;
            moveUp = Keys.W;
            moveLeft = Keys.A;
            moveRight = Keys.D;
        }

        #endregion
    }
}