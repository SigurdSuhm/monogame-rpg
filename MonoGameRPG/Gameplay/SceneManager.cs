#region Using Statements

using System;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.Gameplay
{
    /// <summary>
    /// Scene manager class handling the functionality related to 
    /// the current game scene as well as scene transitions.
    /// </summary>
    public class SceneManager : IDrawable, IUpdateable
    {
        #region Constants

        // Base path to the location of scene files
        private const string SCENE_BASE_PATH = "Content/Scenes/";

        #endregion

        #region Fields

        // Current scene object
        private Scene currentScene;

        // Reference to the content manager object
        private ContentManager contentManager;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SceneManager()
        {
            currentScene = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for the scene manager.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        /// <summary>
        /// Unloads all content associated with the scene manager including the current scene.
        /// </summary>
        public void UnloadContent()
        {
            if (currentScene != null)
                currentScene.UnloadContent();
        }

        /// <summary>
        /// Changes the current scene to a new one. Also unloads content for the old scene and loads content for the new scene.
        /// </summary>
        /// <param name="newSceneName">Name of the new scene.</param>
        public void ChangeScene(string newSceneName)
        {
            // Check for .xml suffix on scene name
            string newSceneFileName = newSceneName;
            if (!newSceneFileName.EndsWith(".xml"))
                newSceneFileName += ".xml";

            // Unload old scene
            if (currentScene != null)
                currentScene.UnloadContent();

            // Open Xml scene document
            XmlDocument sceneFile = new XmlDocument();
            sceneFile.Load(SCENE_BASE_PATH + newSceneFileName);

            // Get parent node and child node list
            XmlNode sceneParentNode = sceneFile.DocumentElement;

            // Get tile map information
            string tileMapPath = sceneParentNode["TileMap"].InnerText;

            // Get all entity nodes
            XmlNodeList entityNodeList = sceneParentNode.SelectNodes("Entity");

            Scene newScene = new Scene(tileMapPath);

            // Process entity nodes according to type
            foreach (XmlNode entityNode in entityNodeList)
            {
                Entity entity = loadEntity(entityNode);
                newScene.EntityList.Add(entity);
            }

            newScene.LoadContent(contentManager);

            currentScene = newScene;
        }

        /// <summary>
        /// Update method called every game frame.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update current scene
            if (currentScene != null)
                currentScene.Update(gameTime);
        }

        /// <summary>
        /// Draws the current scene to the screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw current scene
            if (currentScene != null)
                currentScene.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns an entity object from an Xml node.
        /// </summary>
        /// <param name="entityNode">Xml node to parse.</param>
        /// <returns>Entity object parsed from Xml.</returns>
        private Entity loadEntity(XmlNode entityNode)
        {
            // Get entity type
            string type = entityNode["Type"].InnerText;

            if (type == "Player")
            {
                Player player = new Player();
                // Set looping flag for animations
                player.Image.AnimationManager.Looping = true;

                // Get and set player initial position in the scene
                string[] positionSplitString = entityNode["Position"].InnerText.Split(',');
                player.Position = new Vector2(int.Parse(positionSplitString[0]), int.Parse(positionSplitString[1]));

                return player;
            }

            throw new ArgumentException("Could not parse entity Xml node of type " + type);
        }

        #endregion
    }
}