using Microsoft.Xna.Framework;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class EnemyBullet
    {
        private Vector2 originLocation;
        private Vector2 direction;
        private Vector2 currentPosition;
        private float bulletTimer;

        private const float bulletSpeed = 1f;
        private const float bulletLifeTime = 0.1f;

        private float YDiffrence;
        private float XDiffrence;
        
        private Vector2 gunLocation;

        /// <summary>
        /// Initiates values for a bullet
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="playerVelocity"></param>
        /// <param name="playerCanJump"></param>
        public EnemyBullet(Enemy enemy, Vector2 playerVelocity, bool playerCanJump)
        {
            gunLocation = new Vector2(0, -0.01f);
            if (enemy.EnemyFaceDirection == Direction.Left)
            {
                gunLocation.X = -(enemy.Size.X / 2) - 0.0070f;
            }
            else
            {
                gunLocation.X = enemy.Size.X / 2;
            }
            this.originLocation = enemy.Position + gunLocation;
            currentPosition = originLocation;

            this.direction = enemy.ShotDirection;
            bulletTimer = 0;

            //get the diffrance in the coords from the point of the shot and the position where the shot is gonna land.            
            //if the player cant jump then they are on a platform and there is no need for the Y-led modification
            if (playerCanJump)
            {
                YDiffrence = -(originLocation.Y - (enemy.ShotDirection.Y));
                
            }
            else
            {
                //The YDiffrence gets added an approximated amount of Y cords depending on where the person will be on the Y-led when the bullet 
                //reaches the hit position
                YDiffrence = -(originLocation.Y - (enemy.ShotDirection.Y + (playerVelocity.Y * bulletLifeTime)));
            }
            
            XDiffrence = -(originLocation.X - enemy.ShotDirection.X); 
        }

        public Vector2 Position
        {
            get { return currentPosition; }
        }

        /// <summary>
        /// Updates the position of a bullet
        /// </summary>
        /// <param name="elapsedTime"></param>
        public bool Update(float elapsedTime)
        {
            /*if (currentPosition.X <= direction.X)
            {
                currentPosition.X += direction.X * elapsedTime * bulletSpeed;
            }
            else
            {
                currentPosition.X -= direction.X * elapsedTime * bulletSpeed;
            }

            if (currentPosition.Y <= direction.Y)
            {
                currentPosition.Y -= (direction.Y * elapsedTime * bulletSpeed);
            }
            else
            {
                currentPosition.Y += (direction.Y * elapsedTime * bulletSpeed);
            }*/

            bulletTimer += elapsedTime;
            float lifePrecent = bulletTimer / bulletLifeTime;

            
            //When life precent hits 1 (100%) then the bullet will be at the position where the player got detected
            currentPosition.Y = originLocation.Y + YDiffrence * lifePrecent;
            currentPosition.X = originLocation.X + XDiffrence * lifePrecent;

            if (lifePrecent >= 1)
            {
                return true;
            }

            return false;
        }
    }
}
