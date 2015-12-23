using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    class Camera
    {
        private float ScreenWidth;
        private float ScreenHeight;
        private Vector2 cameraOffset;
        public Camera(GraphicsDevice device)
        {
            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
            cameraOffset = new Vector2(0, 0);
            
        }
        public Vector2 CameraOffSet
        {
            get { return cameraOffset; }
        }

        /// <summary>
        /// default visual coords transformer
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition)
        {
            modelPosition -= cameraOffset;
            return new Vector2((modelPosition.X * ScreenWidth), (modelPosition.Y * ScreenHeight));
        }

        /// <summary>
        /// used when getting visual cords for a platform
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition, float modifier)
        {
            modelPosition -= cameraOffset;
            return new Vector2(((modelPosition.X * ScreenWidth) + modifier * ScreenWidth), (modelPosition.Y * ScreenHeight));
        }

        public Vector2 GetScale(Vector2 size, Texture2D texture)
        {
            return new Vector2((size.X * ScreenWidth) / texture.Bounds.Width, (size.Y * ScreenHeight) / texture.Bounds.Height);
        }

        public void UpdateCameraOffset(Player player, LevelExit levelExit)
        {
            if (player.Position.X >= 0.5f)
            {
                if (!(player.Position.X + 0.5f >= levelExit.Position.X + levelExit.Size.X / 2))
                {
                    cameraOffset.X = (player.Position.X - 0.5f);// * ScreenWidth;
                }
                
            }
            else 
            { 
                cameraOffset.X = 0; 
            }

            if (player.Position.Y <= 0.3f)
            {
                cameraOffset.Y = (player.Position.Y - 0.3f);// * ScreenHeight;
            }
            else 
            { 
                cameraOffset.Y = 0; 
            }
        }
    }
}
