#region Using Statements

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// Stores information on individual animations for sprite sheets.
    /// </summary>
    public struct Animation
    {
        #region Fields

        // Name of the animation
        public string Name;

        // Number of frames in the animation
        public int FrameCount;
        // Array of indexes for individual frames
        public int[] FrameIndexArray;

        // Frame to use when idle after this animation has been player
        public int IdleFrameIndex;

        // Length of each frame in milliseconds
        public int FrameLength;

        #endregion
    }
}