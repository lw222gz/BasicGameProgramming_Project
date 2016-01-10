using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    
    class Player
    {
        //protected fields that are used in the gameCollision script
        protected static Vector2 position;
        protected static Vector2 size = new Vector2(0.1f, 0.1f);
        protected static Vector2 velocity;
        protected static Vector2 attackRange = new Vector2(0.05f, 0.025f);
        protected static Direction playerDirection;
        
        //movement affecting fields
        private float Speed = 0.3f;
        private Vector2 Gravity = new Vector2(0f, 0.7f);
        private bool playerWantsToMoveRight;
        private bool playerWantsToMoveLeft;

        //Attack related fields
        private bool playerIsAttacking;
        private const float attackDurotation = 1f;
        private float attackLasted;

        //state of played related fields
        //TODO: add a playerState Enum, state could be: dead, alive, hasFinishedgame, stunned etc.
        private bool hasFinishedGame;
        private bool isAlive;

        private bool playerCanJump;

        public Player()
        {
            isAlive = true;
            playerWantsToMoveLeft = false;
            playerWantsToMoveRight = false;
            playerIsAttacking = false;
            hasFinishedGame = false;

            playerDirection = Direction.Right;
            position = new Vector2(0.1f, 0.85f);
            velocity = new Vector2(0f, 0f);
        }

        //Properties for private fields
        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Size
        {
            get { return size; }
        }  
        public bool PlayerCanJump
        {
            get { return playerCanJump; }
        }
        public bool PlayerIsAttacking
        {
            get { return playerIsAttacking; }
        }
        public Direction PlayerDirection 
        { 
            get { return playerDirection; } 
        }
        public Vector2 AttackRange
        {
            get { return attackRange; }
        }
        public bool HasFinishedGame
        {
            get { return hasFinishedGame; }
        }
        public Vector2 Velocity
        {
            get { return velocity; }
        }
        public bool IsAlive
        {
            get { return isAlive; }
        }

        /// <summary>
        /// Updates the players position
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void Update(float elapsedTime)
        {
            if (playerIsAttacking)
            {
                attackLasted += elapsedTime;
                if (attackLasted >= attackDurotation)
                {
                    StopPlayerAttack();
                }
            }
            if (playerWantsToMoveLeft)
            {
                //if the player changes direction any current attack will also be interupted.
                if (playerDirection == Direction.Right)
                {
                    StopPlayerAttack();
                    playerDirection = Direction.Left;
                }
                
                position.X -= elapsedTime * Speed;
            }
            if (playerWantsToMoveRight)
            {
                //if the player changes direction any current attack will also be interupted.
                if (playerDirection == Direction.Left)
                {
                    StopPlayerAttack();
                    playerDirection = Direction.Right;
                }
                
                position.X += elapsedTime * Speed;
            }

            //make the player jump and become affected by gravity
            position.Y += velocity.Y * elapsedTime;
            velocity.Y += Gravity.Y * elapsedTime;
            //velocity is at maximum 1, if it is let to increase constantly the pull 
            //will end up too great and just pull the player through the ground
            if (velocity.Y >= 1)
            {
                velocity.Y = 1;
            }

            playerWantsToMoveLeft = false;
            playerWantsToMoveRight = false;
        }

        //initiates a jump
        public void PlayerJump(float elapsedTime)
        {
            velocity.Y = -Gravity.Y - 0.1f;
        }
        //This is called if the player releases the space bar during a jump to reduce the height of the jump
        public void ReduceJump()
        {
            // If character is still ascending in the jump
            if (velocity.Y < -Gravity.Y / 2 - 0.1f)
            {
                velocity.Y = -Gravity.Y / 2 - 0.1f;
            }
        }
        

        /// <summary>
        /// Force set the position of a player, only used atm when initiating a level
        /// </summary>
        /// <param name="_position">model coords of the position that is gonna be set</param>
        public void SetPlayerPosition(Vector2 _position)
        {
            position = _position;
        }       

        //Is called when the player has completed the game.
        public void CompletedGame()
        {
            hasFinishedGame = true;
        }

        /// <summary>
        /// Initiates an attack
        /// </summary>
        public void PlayerAttack()
        {
            playerIsAttacking = true;
        }

        /// <summary>
        /// interupts any current attack
        /// </summary>
        private void StopPlayerAttack()
        {
            playerIsAttacking = false;
            attackLasted = 0;
        }

        //booleans that control the player movements
        public void PlayerWantsToMoveLeft()
        {
            playerWantsToMoveLeft = true;
        }
        public void PlayerWantsToMoveRight()
        {
            playerWantsToMoveRight = true;
        }
        //--

        //These 2 are called form the gameController, 
        //if the player is on a platform then the player can initiate a new jump
        public void SetPlayerCanJump()
        {
            playerCanJump = true;
        }
        //if a player is not on a platform, then the player cant initiate a new jump
        public void PlayerCantJump()
        {
            playerCanJump = false;
        }
        //--

        public void PlayerDead()
        {
            isAlive = false;
        }

        public void Alive()
        {
            isAlive = true;
        }

        //Resets values, this is called when reseting the game via the paus menu, not from dieing
        public void Reset()
        {
            isAlive = true;
            hasFinishedGame = false;
        }

        /// <summary>
        /// if the player stands on a platform that moves in X-Led then the players position
        /// is updated with the speed of the platform
        /// </summary>
        /// <param name="speed">Speed of the platform</param>
        /// <param name="elapsedTime">elapsed gametime since last update</param>
        public void standingOnMovingPlatform(Vector2 speed, float elapsedTime)
        {
            position += speed * elapsedTime;            
        }
    }
}
