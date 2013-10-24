#region Using Statements

using System;
using System.Xml;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Gameplay;
using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.Scene
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

        // Logger object
        private Logger logger;

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
            logger = BaseGame.Instance.Logger;
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

            logger.PostEntry(LogEntryType.Info, "Loading scene from file: " + newSceneFileName);

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

            // Get all scene nodes
            XmlNodeList sceneNodeList = sceneParentNode.SelectNodes("SceneNode");

            Scene newScene = new Scene(tileMapPath);

            // Load individual scene nodes
            foreach (XmlNode curSceneNode in sceneNodeList)
            {
                SceneNode sceneNode = loadSceneNode(curSceneNode);

                // Check for name duplicate
                if (newScene.SceneNodeDictionary.ContainsKey(sceneNode.Name))
                {
                    throw new Exception("A scene node with the name " + sceneNode.Name + " already exists.");
                }
                newScene.SceneNodeDictionary.Add(sceneNode.Name, sceneNode);

                logger.PostEntry(LogEntryType.Info, "Scene node " + sceneNode.Name + " loaded.");
            }

            newScene.LoadContent(contentManager);

            // Get dimensions of the scene collision grid
            string[] collisionGridDimensionsSplitString = sceneParentNode["CollisionGridDimensions"].InnerText.Split(',');
            Dimensions2 collisionGridDimensions = new Dimensions2(int.Parse(collisionGridDimensionsSplitString[0]),
                int.Parse(collisionGridDimensionsSplitString[1]));

            newScene.CreateCollisionGrid(collisionGridDimensions);

            currentScene = newScene;
        }

        /// <summary>
        /// Attempts to find an entity of type 'Player' in the entity list of the current scene.
        /// </summary>
        /// <returns>The first player object found in the current scene. Null if none was found.</returns>
        public Player FindPlayerObject()
        {
            return (currentScene.SceneNodeDictionary["Player"] as Player);
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
        /// Loads a scene node from the given Xml node.
        /// </summary>
        /// <param name="sceneNodeXmlNode">Xml node for the scene node.</param>
        /// <returns>New scene node.</returns>
        private SceneNode loadSceneNode(XmlNode sceneNodeXmlNode)
        {
            // Get scene node type
            string sceneNodeType = sceneNodeXmlNode["Type"].InnerText;
            SceneNode sceneNode = null;

            // Load unqiue scene node name
            string sceneNodeName;
            if (sceneNodeXmlNode.SelectNodes("Name").Count != 0)
                sceneNodeName = sceneNodeXmlNode["Name"].InnerText;

            if (sceneNodeType == "Player")
            {
                sceneNode = new Player();
            }
            else
            {
                throw new ArgumentException("Could not load scene node of type " + sceneNodeType);
            }

            // Load scene node position
            string[] positionSplitString = sceneNodeXmlNode["Position"].InnerText.Split(',');
            sceneNode.Position = new Vector2(float.Parse(positionSplitString[0]), float.Parse(positionSplitString[1]));

            return sceneNode;
        }

        #endregion
    }
}