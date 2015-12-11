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
    class PlatformView
    {
        private Camera camera;
        private Texture2D platFormTexture;
        private SpriteBatch spriteBatch;
        public PlatformView(Camera camera, ContentManager content, SpriteBatch spriteBatch)
        {
            this.camera = camera;
            this.spriteBatch = spriteBatch;

            platFormTexture = content.Load<Texture2D>("PlaceHolderPlatform.png");
        }

        public void DrawPlatforms(List<IGameObject> platForms)
        {
            spriteBatch.Begin();
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
            spriteBatch.End();
        }
    }
}
