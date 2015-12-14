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
        private float endPosition;
        private Vector2 size;

        /// <summary>
        /// To not streatch out the platform image Ill reuse it several times.
        /// so the view size of a platform is 0.5 x 0.1 in model cords while the model platform is the size of the given start and end position.
        /// </summary>
        private Vector2 platformSize = new Vector2(0.5f, 0.1f);
        private int amountofViewPlatforms;
        
        public Platform(Vector2 startPosition, int amountOfPlatforms)
        {
            this.startPosition = startPosition;
            this.endPosition = startPosition.X + platformSize.X * amountOfPlatforms;

            this.size = new Vector2(startPosition.X, endPosition);

            this.amountofViewPlatforms = amountOfPlatforms;
        }

        //properties
        public Vector2 StartPosition
        {
            get { return startPosition; }
        }
        public float EndPosition
        {
            get { return endPosition; }
        }
        public Vector2 PlatformViewSize
        {
            get { return platformSize; }
        }
        public Vector2 Size
        {
            get { return size; }
        }
        public float AmountOfViewPlatforms
        {
            get { return amountofViewPlatforms; }
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
