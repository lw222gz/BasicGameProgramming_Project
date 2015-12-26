using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Enemy
    {
        //indicates the direction the enemy is facing
        private Direction faceDirection;

        private Direction movingDirection;

        private Vector2 position;
        private Vector2 size = new Vector2(0.1f, 0.25f);
        private Vector2 detectionAreaPosition;
        //the detection area extends 0.35 logical cords infront of the enemy
        private const float detectionAreaXled = 0.35f;
        //the detection area extends 0.1 logical cords over each enemy 
        private const float detectionAreaYUpled = 0.1f;
        //the detection area extends 0.05 logical cords under the enemy
        private const float detectionAreaYDownled = 0.05f;

        //An X-cord that the enemy patrols to
        private float patrolPointB;
        private float patrolPointA;
        private float speed = 0.1f;
        private bool shouldTurn;
        private bool hasKilledPlayer;
        private Vector2 shotLocation;



        public Enemy(Vector2 position, Direction faceDirection) : this(position, faceDirection, 0, false) { }

        public Enemy(Vector2 position, Direction faceDirection, float patrolPath, bool shouldTurn)
        {
            hasKilledPlayer = false;
            this.position = position;
            //small adjustments to the position required due to the enemies take up more than 1 grid size-wise
            this.position.Y -= size.Y / 10;
            this.position.X += size.X / 2;
            this.faceDirection = faceDirection;

            if (patrolPath != 0)
            {
                this.patrolPointB = patrolPath - size.X / 2;
            }
            else
            {
                this.patrolPointB = 0;
            }
            this.patrolPointA = position.X + size.X / 2;
            this.shouldTurn = shouldTurn;            
            this.movingDirection = Direction.Right;  
            if(this.shouldTurn)
            {
                this.faceDirection = Direction.Right;
            }
            setDetectionArea();

        }

        

        /// <summary>
        /// Properties for private varibles
        /// </summary>
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Size
        {
            get { return size; }
        }
        public Direction EnemyFaceDirection
        {
            get { return faceDirection; }
        }
        public Vector2 DetectionAreaPosition
        {
            get { return detectionAreaPosition; }
        }
        public float DetectionAreaXLed
        {
            get { return detectionAreaXled; }
        }
        public float DetectionAreaYUpLed
        {
            get { return detectionAreaYUpled; }
        }
        public float DetectionAreaYDownLed
        {
            get { return detectionAreaYDownled; }
        }
        public bool HasKilledPlayer
        {
            get { return hasKilledPlayer; }
        }
        public Vector2 ShotLocation
        {
            get { return shotLocation; }
        }

        
        private void setDetectionArea()
        {
            if (faceDirection == Direction.Left)
            {                
                detectionAreaPosition = new Vector2(this.position.X - detectionAreaXled - (size.X / 2), position.Y - detectionAreaYUpled - (size.Y / 2));
            }
            else
            {
                detectionAreaPosition = new Vector2(this.position.X + (size.X / 2), position.Y - detectionAreaYUpled - (size.Y / 2));
            }
        }

        public void ChangeDirection()
        {
            if (faceDirection == Direction.Left)
            {
                faceDirection = Direction.Right;
            }
            else
            {
                faceDirection = Direction.Left;
            }
            setDetectionArea();
        }


        /// <summary>
        /// If an enemy has a patrol point it will between those 2 points. The points are set depending on the spawn point
        /// and another point decided by the levelMatrix
        /// </summary>
        /// <param name="elapsedTime">amount of time passed since last run in seconds</param>
        public void Update(float elapsedTime)
        {
            if (patrolPointB == 0)
            {
                return;
            }
            float movementSpeed;
            if (movingDirection == Direction.Right)
            {
                if (position.X >= patrolPointB)
                {
                    movingDirection = Direction.Left;
                    if (shouldTurn)
                    {
                        ChangeDirection();
                    }
                }
                movementSpeed = speed;
            }
            else
            {
                if (position.X <= patrolPointA)
                {
                    movingDirection = Direction.Right;
                    if (shouldTurn)
                    {
                        ChangeDirection();
                    }
                }
                movementSpeed = -speed;
            }

            position.X += movementSpeed * elapsedTime;
            //updates the detection areas position
            setDetectionArea();
        }

        //sets values required when shooting the player
        public void KilledPlayer(Vector2 shotLocation)
        {
            hasKilledPlayer = true;
            this.shotLocation = shotLocation;
            ///CONTINUE HERE! 
            ///Maby use linear algebra to decide the bullets speed x and y led?
        }
    }
}
