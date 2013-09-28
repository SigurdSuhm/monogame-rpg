#region Using Statements

using System.Diagnostics;
using System.Collections.Generic;
using System.Xml;

using Microsoft.Xna.Framework;

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// Manager class for playing animations on 2D images using sprite sheets.
    /// </summary>
    public class AnimationManager : IUpdateable
    {
        #region Fields

        // Reference to the image the animation manager is working on
        private Image image;

        // Dimensions of the sprite sheet in images
        private Dimensions2 spriteSheetDimensions;
        // Dimensions of individual sprite sheet elements in pixels
        private Dimensions2 spriteElementDimensions;

        // List of all animations
        private Dictionary<string, Animation> animationList;

        // Queue of animations to be played
        private Queue<Animation> animationQueue;

        // Indicates if the current animation should loop
        private bool looping;

        // Number of milliseconds since last frame switch
        private double timeSinceFrameSwitch = 0;
        // Current animation frame
        private int currentAnimationFrame = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value determining if animations should loop.
        /// </summary>
        public bool Looping
        {
            get { return looping; }
            set { looping = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AnimationManager(Image image)
        {
            this.image = image;

            animationList = new Dictionary<string, Animation>();
            animationQueue = new Queue<Animation>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads animation information from an animation Xml file.
        /// </summary>
        /// <param name="animationDataFilePath">Path to the animation Xml file.</param>
        public void LoadAnimationData(string animationDataFilePath)
        {
            // Create XmlDocument object to be parsed
            XmlDocument animationFile = new XmlDocument();
            animationFile.Load("Content/Animations/" + animationDataFilePath);

            XmlNode animationInfoParentNode = animationFile.DocumentElement;
            XmlNodeList animationNodeList = animationInfoParentNode.ChildNodes;

            // Create a new animation for each animation in the Xml file
            foreach (XmlNode currentNode in animationNodeList)
            {
                if (currentNode.Name == "SpriteSheetDimensions")
                {
                    string[] splitString = currentNode.InnerText.Split(',');
                    spriteSheetDimensions.X = int.Parse(splitString[0]);
                    spriteSheetDimensions.Y = int.Parse(splitString[1]);
                }
                else if (currentNode.Name == "SpriteElementDimensions")
                {
                    string[] splitString = currentNode.InnerText.Split(',');
                    spriteElementDimensions.X = int.Parse(splitString[0]);
                    spriteElementDimensions.Y = int.Parse(splitString[1]);
                }
                else if (currentNode.Name == "Animation")
                {
                    Animation currentAnimation = new Animation();
                    string animationName = currentNode.SelectSingleNode("Name").InnerText;
                    currentAnimation.Name = animationName;

                    currentAnimation.FrameCount = int.Parse(currentNode.SelectSingleNode("FrameCount").InnerText);
                    currentAnimation.FrameLength = int.Parse(currentNode.SelectSingleNode("FrameLength").InnerText);
                    currentAnimation.IdleFrameIndex = int.Parse(currentNode.SelectSingleNode("IdleFrameIndex").InnerText);

                    // Get frame index string and create the appropriate number of indices
                    string[] frameIndices = currentNode.SelectSingleNode("FrameIndices").InnerText.Split(',');

                    currentAnimation.FrameIndexArray = new int[frameIndices.Length];

                    for (int i = 0; i < frameIndices.Length; i++)
                    {
                        currentAnimation.FrameIndexArray[i] = int.Parse(frameIndices[i]);
                    }

                    // Add animation to the dictionary
                    animationList.Add(animationName, currentAnimation);
                }
            }

            updateSourceRectangle(0);
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            if (animationQueue.Count > 0)
            {
                timeSinceFrameSwitch += gameTime.ElapsedGameTime.TotalMilliseconds;

                // Check if animation should jump to next frame
                if (timeSinceFrameSwitch >= animationQueue.Peek().FrameLength)
                {
                    currentAnimationFrame++;
                    timeSinceFrameSwitch -= animationQueue.Peek().FrameLength;

                    // Check if every frame in the animation has been played
                    if (currentAnimationFrame >= animationQueue.Peek().FrameCount)
                    {
                        currentAnimationFrame = 0;

                        // Remove animation from queue if we are not looping
                        if (!looping)
                            animationQueue.Dequeue();
                    }

                    // Update the source rectangle if an animation is still being played
                    if (animationQueue.Count > 0)
                        updateSourceRectangle(animationQueue.Peek().FrameIndexArray[currentAnimationFrame]);
                }
            }
        }

        /// <summary>
        /// Clears animation queue and immediately starts playing an animation.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        public void PlayAnimation(string animationName)
        {
            // Check if animation is already playing
            if (animationQueue.Count > 0)
            {
                if (animationQueue.Peek().Name != animationName)
                {
                    animationQueue.Clear();
                    animationQueue.Enqueue(animationList[animationName]);

                    currentAnimationFrame = 0;
                    updateSourceRectangle(animationList[animationName].FrameIndexArray[currentAnimationFrame]);
                }
            }
            else
            {
                animationQueue.Enqueue(animationList[animationName]);

                currentAnimationFrame = 0;
                updateSourceRectangle(animationList[animationName].FrameIndexArray[currentAnimationFrame]);
            }
        }

        /// <summary>
        /// Queues up an animation to be played after all other animations in the queue.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        public void QueueAnimation(string animationName)
        {
            animationQueue.Enqueue(animationList[animationName]);
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <param name="jumpToNext">Indicates if the next queued animation should start playing.</param>
        public void StopAnimation(bool jumpToNext = false)
        {
            if (animationQueue.Count > 0)
            {
                updateSourceRectangle(animationQueue.Peek().IdleFrameIndex);

                if (jumpToNext)
                    animationQueue.Dequeue();
                else
                    animationQueue.Clear();
            }

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates the source rectangle on the image according to current animation frame.
        /// </summary>
        private void updateSourceRectangle(int frameIndex)
        {
            image.SourceRect = new Rectangle((frameIndex % spriteSheetDimensions.X) * spriteElementDimensions.X,
                (frameIndex / spriteSheetDimensions.X) * spriteElementDimensions.Y,
                spriteElementDimensions.X, spriteElementDimensions.Y);
        }

        #endregion
    }
}