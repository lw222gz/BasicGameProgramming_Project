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
        private float cameraOffset;
        public Camera(GraphicsDevice device)
        {
            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
            cameraOffset = 0;
            
        }


        public Vector2 getVisualCords(Vector2 modelPosition)
        {
            return new Vector2((modelPosition.X * ScreenWidth) - cameraOffset, modelPosition.Y * ScreenHeight);
        }

        public Vector2 getVisualCords(Vector2 modelPosition, float modifier)
        {
            return new Vector2(((modelPosition.X * ScreenWidth) + modifier * ScreenWidth) - cameraOffset, modelPosition.Y * ScreenHeight);
        }

        public Vector2 GetScale(Vector2 size, Texture2D texture)
        {
            return new Vector2((size.X * ScreenWidth) / texture.Bounds.Width, (size.Y * ScreenHeight) / texture.Bounds.Height);
        }

        public void UpdateCameraOffset(Player player)
        {
            if (player.Position.X >= 0.5f)
            {
                cameraOffset = (player.Position.X - 0.5f) * ScreenWidth;
            }            
        }
    }
}
