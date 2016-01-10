using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Platform
    {
        private Vector2 position;

        private Vector2 platformSize = new Vector2(0.1f, 0.1f);
        private float platformWidth;
        private int amountOfViewPlatforms;
        private float patrolEndPoint;
        private float patrolStartPoint;
        private Vector2 speed;
        private Vector2 movingSpeed;
        private Direction movingDirection;
        private bool isMoving;

        public Platform(Vector2 startPosition, float endXPosition, int amountOfViewPlatforms) : this (startPosition, endXPosition, amountOfViewPlatforms , 0, Direction.None)
        {   }
        /// <summary>
        /// Sets all values for a platform
        /// </summary>
        /// <param name="startPosition">The start X and Y position for a platform</param>
        /// <param name="endXPosition">The end position of a platform</param>
        /// <param name="amountOfViewPlatforms">Amount of platform parts required to draw the platform out in the view</param>
        /// <param name="patrolEndPoint">Is the point where a platform maby patrol between said point nad the start position</param>
        /// <param name="movingDirection">Eighter horizontal or vertical value if the platform is gonna patrol</param>
        public Platform(Vector2 startPosition, float endXPosition, int amountOfViewPlatforms, float patrolEndPoint, Direction movingDirection)
        {
            //base values required for a platform
            this.position = new Vector2(startPosition.X + (endXPosition - startPosition.X) / 2,  startPosition.Y + platformSize.Y/2);
            platformWidth = endXPosition - startPosition.X;

            this.amountOfViewPlatforms = amountOfViewPlatforms;

            isMoving = false;
            this.movingDirection = Direction.None;

            //if the platform is gonna patrol between 2 points then some more values need to be set.
            if (movingDirection == Direction.Horizontal || movingDirection == Direction.Vertical)
            {
                isMoving = true;
                this.patrolEndPoint = patrolEndPoint;
                this.movingDirection = movingDirection;

                if (movingDirection == Direction.Horizontal)
                {
                    this.patrolStartPoint = startPosition.X;                    
                    speed = new Vector2(0.1f, 0);
                }
                else
                {
                    this.patrolStartPoint = startPosition.Y;
                    speed = new Vector2(0, 0.1f);                   
                }
                movingSpeed = speed;
            }
                      
        }

        //properties
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 PlatformViewSize
        {
            get { return platformSize; }
        }
        public int AmountOfViewPlatforms
        {
            get { return amountOfViewPlatforms; }
        }
        public bool IsMoving
        {
            get { return isMoving; }
        }
        public Vector2 Speed
        {
            get { return movingSpeed; }
        }
        public float PlatformWidth
        {
            get { return platformWidth; }
        }
        public Direction MovingDirection
        {
            get { return movingDirection; }
        }
        public float StartPatrolPoint
        {
            get { return patrolStartPoint; }
        }
        public float EndPatrolPoint
        {
            get { return patrolEndPoint; }
        }

        /// <summary>
        /// ATM there is no need for an update method here but if wanted I could make platforms move, therefore Ill keep the the method here for future use.
        /// </summary>
        /// <param name="elapsedTime">Amount of time that has elapsed</param>
        public void Update(float elapsedTime)
        {
            if (!isMoving)
            {
                return;
            }

            if (movingDirection == Direction.Horizontal)
            {
                if (position.X - platformWidth / 2 <= patrolStartPoint)
                {
                    movingSpeed = speed;
                }
                if (position.X + platformWidth / 2 >= patrolEndPoint)
                {
                    movingSpeed = -speed;
                }
            }
            else
            {
                if (position.Y - platformSize.Y/2 <= patrolStartPoint)
                {
                    movingSpeed = speed;
                }
                if (position.Y + platformSize.Y / 2 >= patrolEndPoint)
                {
                    movingSpeed = -speed;
                }
            }

            position += elapsedTime * movingSpeed;
        }
    }
}
