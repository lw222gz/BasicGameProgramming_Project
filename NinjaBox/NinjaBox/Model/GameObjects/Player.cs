using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Player
    {
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 size = new Vector2(0.1f, 0.1f);
        private float Speed = 0.3f;
        private Vector2 Gravity = new Vector2(0f, 0.7f);
        private bool playerWantsToMoveRight;
        private bool playerWantsToMoveLeft;

        private bool playerCanJump;
        private bool playerIsOnPlatform;
        public Player()
        {
            playerWantsToMoveLeft = false;
            playerWantsToMoveRight = false;

            playerIsOnPlatform = false;
            position = new Vector2(0.1f, 0.85f);
            velocity = new Vector2(0f, 0f);
        }

        //Properties
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Size
        {
            get { return size; }
        }

        public bool PlayerWantsToMoveLeft
        {
            get { return playerWantsToMoveLeft; }
            set { playerWantsToMoveLeft = value; }
        }
        public bool PlayerWantsToMoveRight
        {
            get { return playerWantsToMoveRight; }
            set { playerWantsToMoveRight = value; }
        }
        public bool PlayerCanJump
        {
            get { return playerCanJump; }
        }


        /// <summary>
        /// Updates the players position
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void Update(float elapsedTime)
        {
            if (playerWantsToMoveLeft)
            {
                position.X -= elapsedTime * Speed;
            }
            if (playerWantsToMoveRight)
            {
                position.X += elapsedTime * Speed;
            }

            //make the player jump and become affected by gravity
            position.Y += velocity.Y * elapsedTime;
            velocity.Y += Gravity.Y * elapsedTime;
            if (velocity.Y >= 1)
            {
                velocity.Y = 1;
            }

            playerWantsToMoveLeft = false;
            playerWantsToMoveRight = false;
        }

        public void PlayerJump()
        {
            playerCanJump = false;
            velocity.Y = -Gravity.Y - 0.1f;
        }
        
        public bool CheckPlatformCollision(Platform platform)
        {
            if(position.Y + (size.Y /2) >=  platform.StartPosition.Y &&
                position.Y + (size.Y / 2) <= platform.StartPosition.Y + platform.PlatformViewSize.Y &&
                position.X >= platform.StartPosition.X && 
                position.X <= platform.EndPosition && 
                velocity.Y >= 0)
            {
                //When the player lands on a platform the jump is re-enabled
                position.Y = platform.StartPosition.Y - size.Y/2;
                return true;
            }
            return false;
        }


        public void CollisionEffect()
        {
            return;
        }


        public void CheckEnemyCollision(Enemy enemy)
        {
            if (enemy.EnemyDirection == Direction.Left)
            {
                if (position.X - size.X / 2 <= enemy.Position.X + (enemy.Size.X / 2) &&
                   position.X - size.X / 2 >= enemy.Position.X &&
                   position.Y + (size.Y / 2) <= enemy.Position.Y + (enemy.Size.Y / 2) &&
                   position.Y - (size.Y / 2) >= enemy.Position.Y - (enemy.Size.Y / 2))
                {
                    enemy.ChangeDirection();
                }
            }
            else
            {
                if (position.X + size.X / 2 >= enemy.Position.X - (enemy.Size.X / 2) &&
                   position.X + size.X / 2 <= enemy.Position.X &&
                   position.Y + (size.Y / 2) <= enemy.Position.Y + (enemy.Size.Y / 2) &&
                   position.Y - (size.Y / 2) >= enemy.Position.Y - (enemy.Size.Y / 2))
                {
                    enemy.ChangeDirection();
                }
            }
        }

        public bool IsPlayerDetected(Enemy enemy)
        {
            if(enemy.EnemyDirection == Direction.Left){
                if (position.X + (size.X / 2) >= enemy.DetectionAreaPosition.X &&
                   position.X + (size.X / 2) <= enemy.Position.X && 
                   position.Y <= enemy.Position.Y + (enemy.Size.Y / 2) + enemy.DetectionAreaYDownLed &&
                   position.Y >= enemy.DetectionAreaPosition.Y)
                {
                    return true;
                }               
            }
            else
            {
                if (position.X - (size.X / 2) <= enemy.Position.X + (enemy.Size.X / 2) + enemy.DetectionAreaXLed &&
                   position.X - (size.X / 2) >= enemy.Position.X &&
                   position.Y <= enemy.Position.Y + (enemy.Size.Y / 2) + enemy.DetectionAreaYDownLed &&
                   position.Y >= enemy.Position.Y - (enemy.Size.Y / 2) - enemy.DetectionAreaYUpLed)
                {
                    return true;
                }  
            }
            
            return false;

        }

        public void SetPlayerCanJump()
        {
            playerCanJump = true;
        }

        public void PlayerCantJump()
        {
            playerCanJump = false;
        }
    }
}
