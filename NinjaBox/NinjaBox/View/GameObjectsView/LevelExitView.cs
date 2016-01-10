using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class LevelExitView : MainView
    {
        private Texture2D levelExitTexture;

        public LevelExitView()
        {
            levelExitTexture = content.Load<Texture2D>("ExitPlaceholder.png");
        }

        /// <summary>
        /// draws the level exit
        /// </summary>
        /// <param name="levelExit">obj refrence to the level exit</param>
        public void DrawExit(LevelExit levelExit)
        {
            spriteBatch.Draw(levelExitTexture, 
                            camera.getVisualCords(levelExit.Position), 
                            null, 
                            Color.White, 
                            0f, 
                            new Vector2(levelExitTexture.Bounds.Width/2, levelExitTexture.Bounds.Height/2), 
                            1f, 
                            SpriteEffects.None, 
                            0);
        }
    }
}
