using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Platform : IGameObject
    {
        private Vector2 startPosition;
        private Vector2 endPosition;

        /// <summary>
        /// To not streatch out the platform image Ill reuse it several times.
        /// so the view size of a platform is 0.5 x 0.1 in model cords while the model platform is the size of the given start and end position.
        /// </summary>
        private Vector2 platformViewSize = new Vector2(0.5f, 0.1f);
        private float amountofViewPlatforms;
        
        public Platform(Vector2 startPosition, Vector2 endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;

            amountofViewPlatforms = (endPosition.X - startPosition.X) / platformViewSize.X;
        }

        //properties
        public Vector2 StartPosition
        {
            get { return startPosition; }
        }
        public Vector2 EndPosition
        {
            get { return endPosition; }
        }
        public Vector2 PlatformViewSize
        {
            get { return platformViewSize; }
        }
        public float AmountOfViewPlatforms
        {
            get { return amountofViewPlatforms; }
        }
        

    }
}
