using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class MessagesView : MainView
    {
        private SpriteFont gameFont;
        public MessagesView() {
            gameFont = content.Load<SpriteFont>("NewSpriteFont");
        }


        public void DrawMessages(List<Message> levelMessages){

            foreach(Message m in levelMessages){
                Vector2 FontOrigin = gameFont.MeasureString(m.getMessage) / 2;
                spriteBatch.DrawString(gameFont, 
                    m.getMessage, 
                    camera.getVisualCords(m.Position), 
                    Color.White, 
                    0, 
                    Vector2.Zero, 
                    1.0f, 
                    SpriteEffects.None, 
                    0.5f);
            }
        }
    }
}
