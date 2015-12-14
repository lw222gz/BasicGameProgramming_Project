using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    public enum Direction
    {
        Left,
        Right
    }
    class Enemy
    {
        //indicates the direction the enemy is facing
        private Direction direction;

        private Vector2 position;
        private Vector2 size = new Vector2(0.1f, 0.25f);
        private Vector2 detectionAreaPosition;
        //the detection area extends 0.35 logical cords infront of the enemy
        private const float detectionAreaXled = 0.35f;
        //the detection area extends 0.1 logical cords over each enemy 
        private const float detectionAreaYUpled = 0.1f;
        //the detection area extends 0.05 logical cords under the enemy
        private const float detectionAreaYDownled = 0.05f;

        

        public Enemy(Vector2 position, Direction direction)
        {
            this.position = position;
            this.position.Y -= size.Y / 2;
            this.direction = direction;

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
        public Direction EnemyDirection
        {
            get { return direction; }
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


        
        private void setDetectionArea()
        {
            if (direction == Direction.Left)
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
            if (direction == Direction.Left)
            {
                direction = Direction.Right;
            }
            else
            {
                direction = Direction.Left;
            }
            setDetectionArea();
        }



        public void Update(float elapsedTime)
        {
            return;
        }
    }
}
