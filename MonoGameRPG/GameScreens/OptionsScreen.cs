#region Using Statements

using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MonoGameRPG.Utility;

#endregion

namespace MonoGameRPG.GameScreens
{
    /// <summary>
    /// Game screen used for options menu.
    /// </summary>
    public class OptionsScreen : GameScreen
    {
        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content loaded for options screen.");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();

            BaseGame.Instance.Logger.PostEntry(LogEntryType.Info, "Content unloaded for options screen.");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
