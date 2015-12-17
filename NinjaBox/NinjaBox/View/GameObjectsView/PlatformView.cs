using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class PlatformView : MainView
    {
        //private Camera camera;
        private Texture2D platFormTexture;
        //private SpriteBatch spriteBatch;

        /// <summary>
        /// default constructor
        /// </summary>
        public PlatformView() 
        {
            platFormTexture = content.Load<Texture2D>("Platform.png");
        }

        public void DrawPlatforms(List<Platform> platForms)
        {
            foreach (Platform p in platForms)
            {
                for(int i = 0; i < p.AmountOfViewPlatforms; i ++){
                    spriteBatch.Draw(platFormTexture,
                                    camera.getVisualCords(p.StartPosition, i * p.PlatformViewSize.X), 
                                    null, 
                                    Color.White, 
                                    0, 
                                    Vector2.Zero, 
                                    1, 
                                    SpriteEffects.None, 
                                    0f);
                }
                
            }
        }
    }
}
