using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Platform
    {
        private Vector2 startPosition;
        private float endXPosition;
        private Vector2 size;

        /// <summary>
        /// To not streatch out the platform image Ill reuse it several times.
        /// so the view size of a platform is 0.5 x 0.1 in model cords while the model platform is the size of the given start and end position.
        /// </summary>
        //private Vector2 platformSize = new Vector2(0.5f, 0.1f);
        private Vector2 platformSize = new Vector2(0.1f, 0.1f);
        private int amountOfViewPlatforms;

        public Platform(Vector2 startPosition, float endXPosition, int amountOfViewPlatforms)
        {
            this.startPosition = startPosition;
            this.endXPosition = endXPosition;
            this.amountOfViewPlatforms = amountOfViewPlatforms;       
        }

        //properties
        public Vector2 StartPosition
        {
            get { return startPosition; }
        }
        public float EndXPosition
        {
            get { return endXPosition; }
        }
        public Vector2 PlatformViewSize
        {
            get { return platformSize; }
        }
        public Vector2 Size
        {
            get { return size; }
        }
        public int AmountOfViewPlatforms
        {
            get { return amountOfViewPlatforms; }
        }


        /// <summary>
        /// ATM there is no need for an update method here but if wanted I could make platforms move, therefore Ill keep the the method here for future use.
        /// </summary>
        /// <param name="elapsedTime">Amount of time that has elapsed</param>
        public void Update(float elapsedTime)
        {
            return;
        }




    }
}
