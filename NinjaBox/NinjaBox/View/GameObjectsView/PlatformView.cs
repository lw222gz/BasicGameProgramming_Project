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
        private Texture2D movingPlatformBarHorizontalTexture;
        private Texture2D movingPlatformBarVerticalTexture;
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
            movingPlatformBarHorizontalTexture = content.Load<Texture2D>("PlatformImages/MovingPlatformBarHorizontal.png");
            movingPlatformBarVerticalTexture = content.Load<Texture2D>("PlatformImages/MovingPlatformBarVertical.png");
        }
        /// <summary>
        /// Draws all the platforms for a level
        /// </summary>
        /// <param name="platForms">list of all platforms for the current level</param>
        public void DrawPlatforms(List<Platform> platForms)
        {
            foreach (Platform p in platForms)
            {
                //if a platform is moving it gets a couple of metal "bars" for a visual effect
                if (p.IsMoving)
                {            
                    DrawMovingPlatformBars(p);                          
                }

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
                                        camera.getVisualCords(new Vector2(p.Position.X - p.PlatformWidth / 2, p.Position.Y - p.PlatformViewSize.Y / 2), i * p.PlatformViewSize.X),
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
                                    camera.getVisualCords(new Vector2(p.Position.X - p.PlatformWidth / 2, p.Position.Y - p.PlatformViewSize.Y / 2)),
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

        /// <summary>
        /// Draws bars for a moving platform
        /// </summary>
        /// <param name="p">platform object that is moving and gets it's bars drawn</param>
        private void DrawMovingPlatformBars(Platform p)
        {
            //will carry the values for the coords the bar will be placed
            float YCoordValue;
            float XCoordValue;

            //carries the values added to the coords each time one is placed
            float XAdd;
            float YAdd;

            Texture2D activeBarTexture;
            Vector2 imageOrigin;

            int amountOfBars = 0;

            //set values for a horizontal moving platform
            if (p.MovingDirection == Direction.Horizontal)
            {
                XCoordValue = p.StartPatrolPoint + p.PlatformViewSize.X / 2;
                YCoordValue = p.Position.Y;
                XAdd = camera.getModelValue(new Vector2(movingPlatformBarHorizontalTexture.Bounds.Width, movingPlatformBarHorizontalTexture.Bounds.Height)).X;
                YAdd = 0;
                amountOfBars = p.AmountOfViewPlatforms + (int)Math.Round((p.EndPatrolPoint - p.StartPatrolPoint - p.PlatformWidth) * 10) - 1;
                activeBarTexture = movingPlatformBarHorizontalTexture;
                imageOrigin = new Vector2(0, movingPlatformBarHorizontalTexture.Bounds.Height / 2);
            }

            //sets values for a vertical moving platform
            else
            {
                XCoordValue = p.Position.X;
                YCoordValue = p.StartPatrolPoint + p.PlatformViewSize.Y / 2;
                XAdd = 0;
                YAdd = camera.getModelValue(new Vector2(movingPlatformBarVerticalTexture.Bounds.Width, movingPlatformBarVerticalTexture.Bounds.Height)).Y;
                amountOfBars = (int)((p.EndPatrolPoint - p.StartPatrolPoint) * 10) - 1;
                activeBarTexture = movingPlatformBarVerticalTexture;
                imageOrigin = new Vector2(movingPlatformBarVerticalTexture.Bounds.Width / 2, 0);
            }


            
            for (int j = 0; j < amountOfBars; j++)
            {
                spriteBatch.Draw(activeBarTexture,
                                camera.getVisualCords(new Vector2(XCoordValue, YCoordValue)),
                                null,
                                Color.Gray,
                                0,
                                imageOrigin,
                                1f,
                                SpriteEffects.None,
                                0);

                XCoordValue += XAdd;
                YCoordValue += YAdd;
            }
        }
    }
}
