using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model
{
    /// <summary>
    /// inherits from the player class since all collisions will have to do with the player
    /// </summary>
    class GameCollisions : Player
    {
        //empty constructor due to the inheritance it's required
        public GameCollisions() {   }

        //checks if the player is too close to the enemies backs, if so they turn around to then be killed.
        //NOTE: This function does not cause the player to die. If the robots turn the method IsPlayerDetected will kill them.
        //WHY: If I want to add a detection timer, let's say if the player is within the detection area for 0.4 seconds THEN the player dies. Thus I dont want to kill the player via this method.
        public void CheckEnemyCollision(Enemy enemy)
        {
            if (enemy.EnemyFaceDirection == Direction.Left)
            {
                if (position.X - size.X / 2 <= enemy.Position.X + (enemy.Size.X / 2) &&
                   position.X - size.X / 2 >= enemy.Position.X &&
                   position.Y <= enemy.Position.Y + (enemy.Size.Y / 2) &&
                   position.Y >= enemy.Position.Y - (enemy.Size.Y / 2))
                {
                    enemy.ChangeDirection();
                }
            }
            else
            {
                if (position.X + size.X / 2 >= enemy.Position.X - (enemy.Size.X / 2) &&
                   position.X + size.X / 2 <= enemy.Position.X &&
                   position.Y <= enemy.Position.Y + (enemy.Size.Y / 2) &&
                   position.Y >= enemy.Position.Y - (enemy.Size.Y / 2))
                {
                    enemy.ChangeDirection();
                }
            }
        }


        //checks if the player is within the detection area for an enemy. If so the player dies instantly.
        public bool IsPlayerDetected(Enemy enemy)
        {
            if (enemy.EnemyFaceDirection == Direction.Left)
            {
                if (position.X + (size.X / 2) >= enemy.DetectionAreaPosition.X &&
                   position.X + size.X / 2  <= enemy.Position.X + enemy.Size.X / 4 &&
                   position.Y - size.Y/2 <= enemy.Position.Y + (enemy.Size.Y / 2) + enemy.DetectionAreaYDownLed &&
                   position.Y + size.Y/2 >= enemy.DetectionAreaPosition.Y)
                {
                    return true;
                }
            }
            else
            {
                if (position.X - (size.X / 2) <= enemy.Position.X + (enemy.Size.X / 2) + enemy.DetectionAreaXLed &&
                   position.X - (size.X / 2) >= enemy.Position.X - enemy.Size.X / 4 &&
                   position.Y - size.Y / 2<= enemy.Position.Y + (enemy.Size.Y / 2) + enemy.DetectionAreaYDownLed &&
                   position.Y + size.Y/2>= enemy.Position.Y - (enemy.Size.Y / 2) - enemy.DetectionAreaYUpLed)
                {
                    return true;
                }
            }

            return false;
        }


        /// <summary>
        /// checks if the player has passed through the level exit
        /// </summary>
        /// <param name="levelExit">object refrence to the levelExit</param>
        /// <returns>return boolean, true if player has finished level, otherwise false</returns>
        public bool IsLevelCompleted(LevelExit levelExit)
        {
            if (position.X >= levelExit.Position.X + levelExit.Size.X / 2 &&
               position.Y <= levelExit.Position.Y + levelExit.Size.Y / 2 &&
               position.Y >= levelExit.Position.Y - levelExit.Size.Y / 2)
            {
                return true;
            }
            return false;
        }



        //checks the collision for platforms
        public bool CheckPlatformCollision(Platform platform)
        {
            if (position.Y + size.Y/2 >= platform.StartPosition.Y &&
                position.Y <= platform.StartPosition.Y &&
                position.X >= platform.StartPosition.X &&
                position.X <= platform.EndXPosition &&
                velocity.Y >= 0)
            {
                //When the player lands on a platform the jump is re-enabled
                position.Y = platform.StartPosition.Y - size.Y / 2;
                return true;
            }
            return false;
        }

        /// <summary>
        /// checks if the player has hit an enemy
        /// </summary>
        /// <param name="e">enemy that is checked if hit</param>
        /// <returns></returns>
        public bool CheckPlayerAttackArea(Enemy e)
        {
            float attackXMax; 
            float attackXMin; 
            if (playerDirection == Direction.Left)
            {
                attackXMax = position.X - size.X / 2 - attackRange.X;
                attackXMin = position.X - size.X / 2 - attackRange.X;
            }
            else
            {
                attackXMax = position.X + size.X / 2 + attackRange.X;
                attackXMin = position.X + size.X / 2 + attackRange.X;
            }

            if (attackXMin <= e.Position.X + e.Size.X / 2 && 
                attackXMax >= e.Position.X - e.Size.X /2 &&
                position.Y >= e.Position.Y - e.Size.Y / 2 && 
                position.Y <= e.Position.Y + e.Size.Y / 2)
            {
                return true;
            }

            return false;
        }
    }
}
