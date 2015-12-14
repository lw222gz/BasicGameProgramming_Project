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

        /// <summary>
        /// default visual coords transformer
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition)
        {
            return new Vector2((modelPosition.X * ScreenWidth) - cameraOffset.X, (modelPosition.Y * ScreenHeight) - cameraOffset.Y);
        }

        /// <summary>
        /// used when getting visual cords for a platform
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition, float modifier)
        {
            return new Vector2(((modelPosition.X * ScreenWidth) + modifier * ScreenWidth) - cameraOffset.X, (modelPosition.Y * ScreenHeight) - cameraOffset.Y);
        }

        public Vector2 GetScale(Vector2 size, Texture2D texture)
        {
            return new Vector2((size.X * ScreenWidth) / texture.Bounds.Width, (size.Y * ScreenHeight) / texture.Bounds.Height);
        }

        public void UpdateCameraOffset(Player player)
        {
            if (player.Position.X >= 0.5f)
            {
                cameraOffset.X = (player.Position.X - 0.5f) * ScreenWidth;
            }
            if (player.Position.Y <= 0.3f)
            {
                cameraOffset.Y = (player.Position.Y - 0.3f) * ScreenHeight;
            }
        }
    }
}
