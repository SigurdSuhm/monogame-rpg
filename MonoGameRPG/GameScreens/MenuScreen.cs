#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameRPG.Graphics;
using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Game screen used for the main menu.
    /// </summary>
    public class MenuScreen : GameScreen
    {
        #region Constants

        // Array of menu item identifiers
        private readonly string[] MENU_ITEM_IDENTIFIERS = { "NewGame", "LoadGame", "Options", "Exit" };

        #endregion

        #region Fields

        // Menu object containing all the main menu items
        private Menu menu;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuScreen()
            : base()
        {
            menu = new Menu(MENU_ITEM_IDENTIFIERS, "Textures/MenuScreen");
            menu.Position = new Vector2(BaseGame.Instance.GraphicsDevice.Viewport.Width / 2,
                (BaseGame.Instance.GraphicsDevice.Viewport.Height / 2) - 120);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads content associated with the menu screen.
        /// </summary>
        /// <param name="contentManager">Content manager object.</param>
        public override void LoadContent(ContentManager contentManager)
        {
            menu.LoadContent(contentManager);

            // Set methods for menu item activate events
            menu.MenuItemDictionary["NewGame"].Activated += newGame_OnActivate;
            menu.MenuItemDictionary["LoadGame"].Activated += loadGame_OnActivate;
            menu.MenuItemDictionary["Options"].Activated += options_OnActivate;
            menu.MenuItemDictionary["Exit"].Activated += exit_OnActivate;

            base.LoadContent(contentManager);

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content loaded for menu screen.");
        }
                
        /// <summary>
        /// Unloads content associated with the menu screen.
        /// </summary>
        public override void UnloadContent()
        {
            menu.UnloadContent();

            base.UnloadContent();

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content unloaded for menu screen.");
        }

        /// <summary>
        /// Updates the menu screen.
        /// </summary>
        /// <param name="gameTime">Snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // Update menu
            menu.Update(gameTime);

            // TODO: FOR TESTING PURPOSES
            if (InputManager.Instance.KeyPressed(Keys.Escape))
            {
                menu.MenuItemDictionary["Exit"].OnActivate();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the entire menu screen.
        /// </summary>
        /// <param name="spriteBatch">Sprite batch object used for 2D rendering.</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        #endregion

        #region Menu Item Methods

        /// <summary>
        /// Method called when new 'New Game' menu item is activated.
        /// </summary>
        private void newGame_OnActivate()
        {
            // Switch to gameplay screen
            ScreenManager.Instance.ChangeScreen("GameplayScreen");
        }

        /// <summary>
        /// Method called when new 'Load Game' menu item is activated.
        /// </summary>
        private void loadGame_OnActivate()
        {
            throw new NotImplementedException("MenuScree.loadGame_OnActivate() has not yet been implemented.");
        }

        /// <summary>
        /// Method called when new 'Options' menu item is activated.
        /// </summary>
        private void options_OnActivate()
        {
            ScreenManager.Instance.ChangeScreen("OptionsScreen");
        }

        /// <summary>
        /// Method called when new 'Exit' menu item is activated.
        /// </summary>
        private void exit_OnActivate()
        {
            // Exits the game completely
            BaseGame.Instance.Exit();
        }

        #endregion
    }
}
