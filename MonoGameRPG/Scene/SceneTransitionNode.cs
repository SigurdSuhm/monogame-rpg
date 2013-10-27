#region Using Statements

#endregion

namespace MonoGameRPG.Scene
{
    /// <summary>
    /// Scene node triggering a scene transition when the player enters.
    /// </summary>
    public class TransitionSceneNode : StaticSceneNode
    {
        #region Fields

        // Name of the file to the new scene
        private string newSceneName;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the scene the node transitions to.
        /// </summary>
        public string NewSceneName
        {
            get { return newSceneName; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="name">Unique scene node name.</param>
        public TransitionSceneNode(string name, string newSceneName)
            : base(name, null)
        {
            this.newSceneName = newSceneName;
        }

        #endregion
    }
}