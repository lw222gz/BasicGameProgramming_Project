﻿using Microsoft.Xna.Framework;
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
        private bool isPlayerDead;

        private bool playerCanJump;
        public Player()
        {
            playerWantsToMoveLeft = false;
            playerWantsToMoveRight = false;

            isPlayerDead = false;
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

        public bool IsPlayerDead
        {
            get { return isPlayerDead; }
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

        public void SetPlayerPosition(Vector2 position)
        {
            this.position = position;
        }

        public void Restart(Vector2 position)
        {
            isPlayerDead = false;
            SetPlayerPosition(position);
        }
        
        //TODO: Right now all of the collision code is in the player scirpt. Could be broken out to it's own script

        //checks the collision for platforms
        public bool CheckPlatformCollision(Platform platform)
        {
            if(position.Y + (size.Y /2) >=  platform.StartPosition.Y &&
                position.Y + (size.Y / 2) <= platform.StartPosition.Y + platform.PlatformViewSize.Y &&
                position.X >= platform.StartPosition.X && 
                position.X <= platform.EndXPosition && 
                velocity.Y >= 0)
            {
                //When the player lands on a platform the jump is re-enabled
                position.Y = platform.StartPosition.Y - size.Y/2;
                return true;
            }
            return false;
        }

        //checks if the player is too close to the enemies backs, if so they turn around to then be killed.
        //NOTE: This function does not cause the player to die. If the robots turn the method IsPlayerDetected will kill them.
        //WHY: If I want to add a detection timer, let's say if the player is within the detection area for 0.4 seconds THEN the player dies. Thus I dont want to kill the player via this method.
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

        //checks if the player is within the detection area for an enemy. If so the player dies instantly.
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

        public bool IsLevelCompleted(LevelExit levelExit)
        {
            if(position.X >= levelExit.Position.X + levelExit.Size.X/2 &&
               position.Y <= levelExit.Position.Y + levelExit.Size.Y/2 && 
               position.Y >= levelExit.Position.Y - levelExit.Size.Y/2)
            {                
                return true;
            }

            return false;
        }

        //changes values for private fields
        public void SetPlayerCanJump()
        {
            playerCanJump = true;
        }
        public void PlayerCantJump()
        {
            playerCanJump = false;
        }
        public void PlayerDead()
        {
            isPlayerDead = true;
        }

        
    }
}
