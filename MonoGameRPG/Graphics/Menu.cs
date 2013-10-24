#region Using Statements

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace MonoGameRPG.Graphics
{
    /// <summary>
    /// A menu containing several menu items.
    /// </summary>
    public class Menu : IUpdateable, IDrawable
    {
        #region Fields

        // List of menu items in the menu
        private Dictionary<string, MenuItem> menuItemDictionary;

        // Position of the menu on screen
        private Vector2 position;

        // Indicates if the menu items have been loaded
        // This is used for determining if menu position is being applied to the menu items for the first time
        private bool menuItemsLoaded = false;

        // Spacing between menu items
        private int spacing;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the menu item dictionary.
        /// </summary>
        public Dictionary<string, MenuItem> MenuItemDictionary
        {
            get { return menuItemDictionary; }
        }

        /// <summary>
        /// Gets or sets the position of the menu.
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
            set { updateMenuPosition(value); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Menu(string[] menuItemIdentifiers, string menuFolderName, int spacing)
        {
            menuItemDictionary = new Dictionary<string, MenuItem>();
            this.spacing = spacing;

            // Load individual menu items
            foreach (string curMenuItem in menuItemIdentifiers)
            {
                menuItemDictionary.Add(curMenuItem, new MenuItem(menuFolderName + "/" + curMenuItem));
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content for all menu items in the menu.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public void LoadContent(ContentManager contentManager)
        {
            // Height of the previous image texture
            int previousImageYPosition = 0;

            // Load content for each individual menu item
            foreach (MenuItem item in menuItemDictionary.Values)
            {
                item.LoadContent(contentManager);

                // Set menu item position and store current texture height
                item.Position = new Vector2(0 - (int)(item.Image.Dimensions.X / 2.0f), previousImageYPosition);
                previousImageYPosition += (int)item.Image.Dimensions.Y + spacing;
            }

            // Update menu item positions and set the flag indicating that menu items have been loaded
            updateMenuPosition(position);
            menuItemsLoaded = true;
        }

        /// <summary>
        /// Unloads all content associated with the menu.
        /// </summary>
        public void UnloadContent()
        {
            foreach (MenuItem item in menuItemDictionary.Values)
            {
                // Unload content for the menu item
                item.UnloadContent();
            }
        }

        /// <summary>
        /// Updates the menu.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {
            // Update each individual menu item
            foreach (MenuItem item in menuItemDictionary.Values)
                item.Update(gameTime);
        }

        /// <summary>
        /// Draws all menu items.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw each individual menu item
            foreach (MenuItem item in menuItemDictionary.Values)
            {
                item.Draw(spriteBatch);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Updates all menu item positions according to a new menu position.
        /// </summary>
        private void updateMenuPosition(Vector2 newMenuPosition)
        {
            // Subtraction of old position value should only happen if this takes place after
            //   menu items have been loaded
            if (menuItemsLoaded)
            {
                // Subtract the old menu position from menu item positions
                foreach (MenuItem item in menuItemDictionary.Values)
                    item.Position -= position;
            }

            position = newMenuPosition;

            // Update positions of the menu items
            foreach (MenuItem item in menuItemDictionary.Values)
                item.Position += position;
        }

        #endregion
    }
}
