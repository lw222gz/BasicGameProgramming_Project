﻿using Microsoft.Xna.Framework;
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
        private SpriteFont creditFont;

        public MessagesView() {
            gameFont = content.Load<SpriteFont>("NewSpriteFont");
            creditFont = content.Load<SpriteFont>("creditFont");
            
        }


        public void DrawMessages(List<Message> levelMessages){
            Vector2 FontOrigin;

            foreach(Message m in levelMessages){
                FontOrigin = gameFont.MeasureString(m.getMessage) / 2;

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

        /// <summary>
        /// Draws out all credtis
        /// </summary>
        public void DrawCredits()
        {
            //Background music
            spriteBatch.DrawString(creditFont, "Backgroundmusic : ", camera.getVisualCords(new Vector2(0.13f, 0.13f) + camera.CameraOffSet), Color.White);
            spriteBatch.DrawString(creditFont, "'Stealth Groover' Kevin MacLeod (incompetech.com)", camera.getVisualCords(new Vector2(0.13f, 0.16f) + camera.CameraOffSet), Color.White);
            spriteBatch.DrawString(creditFont, "Licensed under Creative Commons: By Attribution 3.0", camera.getVisualCords(new Vector2(0.13f, 0.19f) + camera.CameraOffSet), Color.White);
            spriteBatch.DrawString(creditFont, "http://creativecommons.org/licenses/by/3.0/", camera.getVisualCords(new Vector2(0.13f, 0.22f) + camera.CameraOffSet), Color.White);

            //WilhelmScream sound effect
            spriteBatch.DrawString(creditFont, "Wilhelm Scream(Fall to death sound): Syna-Max (freesound.org)", camera.getVisualCords(new Vector2(0.13f, 0.28f) + camera.CameraOffSet), Color.White);
            spriteBatch.DrawString(creditFont, "Licensed under Creative Commons: the Attribution Noncommercial License.", camera.getVisualCords(new Vector2(0.13f, 0.31f) + camera.CameraOffSet), Color.White);
            spriteBatch.DrawString(creditFont, "http://creativecommons.org/licenses/by-nc/3.0/", camera.getVisualCords(new Vector2(0.13f, 0.34f) + camera.CameraOffSet), Color.White);

            //platform textures
            spriteBatch.DrawString(creditFont, "Platform textures: Dante Wik", camera.getVisualCords(new Vector2(0.13f, 0.40f) + camera.CameraOffSet), Color.White);

            //Rest
            spriteBatch.DrawString(creditFont, "Rest: Lucas Wik", camera.getVisualCords(new Vector2(0.13f, 0.46f) + camera.CameraOffSet), Color.White);
        }
    }
}
