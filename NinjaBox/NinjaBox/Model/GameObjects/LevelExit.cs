using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class LevelExit
    {
        private Vector2 position;
        private Vector2 size = new Vector2(0.14f, 0.375f);


        public LevelExit(Platform platform)
        {
            position = new Vector2(platform.EndXPosition - size.X /2, platform.Position.Y - size.Y/2);
        }

        public LevelExit(Vector2 position)
        {
            this.position = position;
            //adjustments to the position due to the exit not being same size as displayed on the levelMakerGrid
            this.position.Y -= 0.087f;
            this.position.X += 0.025f;
        }

        public Vector2 Position
        {
            get { return position; }
        }

        public Vector2 Size
        {
            get { return size; }
        }
    }
}
