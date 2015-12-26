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
        private Texture2D platformMiddlePartTexture;
        private Texture2D platformLeftEndTexture;
        private Texture2D platformRightEndTexture;
        private Texture2D platformLoneTexture;
        //private SpriteBatch spriteBatch;

        /// <summary>
        /// default constructor
        /// </summary>
        public PlatformView() 
        {
            platformMiddlePartTexture = content.Load<Texture2D>("PlatformImages/PlatformMiddlePiece.png");
            platformLeftEndTexture = content.Load<Texture2D>("PlatformImages/LeftPlatformEnd.png");
            platformRightEndTexture = content.Load<Texture2D>("PlatformImages/RightPlatformEnd.png");
            platformLoneTexture = content.Load<Texture2D>("PlatformImages/Loneplatform.png"); 
        }
        /// <summary>
        /// Draws all the platforms for a level
        /// </summary>
        /// <param name="platForms">list of all platforms for the current level</param>
        public void DrawPlatforms(List<Platform> platForms)
        {
            foreach (Platform p in platForms)
            {
                if (p.AmountOfViewPlatforms > 1)
                {
                    for (int i = 0; i < p.AmountOfViewPlatforms; i++)
                    {
                        Texture2D currentTexture;
                        if (i == 0)
                        {
                            currentTexture = platformLeftEndTexture;
                        }
                        else if (i == p.AmountOfViewPlatforms - 1)
                        {
                            currentTexture = platformRightEndTexture;
                        }
                        else
                        {
                            currentTexture = platformMiddlePartTexture;
                        }
                        spriteBatch.Draw(currentTexture,
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
                else
                {
                    spriteBatch.Draw(platformLoneTexture,
                                    camera.getVisualCords(p.StartPosition),
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
