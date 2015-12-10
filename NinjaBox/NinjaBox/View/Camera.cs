using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public Camera(GraphicsDevice device)
        {
            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
        }




        public Vector2 getVisualCords(Vector2 modelPosition)
        {
            return new Vector2(modelPosition.X * ScreenWidth, modelPosition.Y * ScreenHeight);
        }

        public Vector2 getVisualCords(Vector2 modelPosition, float modifier)
        {
            return new Vector2((modelPosition.X * ScreenWidth) + modifier*ScreenWidth, modelPosition.Y * ScreenHeight);
        }
    }
}
