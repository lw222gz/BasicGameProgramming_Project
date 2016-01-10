using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using NinjaBox.View.GameObjectsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.VisualEffects
{
    class EnemyShootingPlayerEffect : MainView, IEffect
    {
        private Enemy enemy;
        private Vector2 playerVelocity;
        private bool playerCanJump;

        private const float bulletShootInterval = 0.05f;
        private float shootDurotation;
        private float nextShootMark;
        private List<EnemyBullet> bullets;

        private static Texture2D bulletTexture;
        private static SoundEffect bulletShotSound;

        /// <summary>
        /// loads in the visual effects content
        /// </summary>
        public EnemyShootingPlayerEffect()
        {
            bulletTexture = content.Load<Texture2D>("BulletPlaceholder.png");
            bulletShotSound = content.Load<SoundEffect>("GunShot");
        }
        /// <summary>
        /// Initiates the visual effect
        /// </summary>
        /// <param name="enemy">enemy obj that is shooting the player</param>
        /// <param name="playerVelocity">Velocity of the player so the bullets aim can be predicted</param>
        /// <param name="playerCanJump">To avoid a bugg with the velocity being count in if the player would stand still I need to see if the player is on the platform</param>
        public EnemyShootingPlayerEffect(Enemy enemy, Vector2 playerVelocity, bool playerCanJump)
        {
            this.enemy = enemy;
            this.playerVelocity = playerVelocity;
            this.playerCanJump = playerCanJump;

            shootDurotation = 0;
            nextShootMark = 0;
            bullets = new List<EnemyBullet>(5);
        }

        /// <summary>
        /// returns boolean representing if the visual effect is over
        /// </summary>
        public bool IsEffectOver
        {
            get { return shootDurotation == .25f; }
        }

        /// <summary>
        /// Runs the visual effect
        /// </summary>
        /// <param name="timeElapsed">time elapsed since last update</param>
        public void RunEffect(float timeElapsed)
        {
            //shoot bullets to the players location
            shootDurotation += timeElapsed;
            //stops shooting after 5 shots, (0.25 % 0.05 = 5)
            if (shootDurotation >= .25f)
            {
                shootDurotation = .25f;
            }
            if (shootDurotation >= nextShootMark)
            {
                bullets.Add(new EnemyBullet(enemy, playerVelocity, playerCanJump));
                bulletShotSound.Play();
                nextShootMark += bulletShootInterval;
            }

            for (int i = 0; i < bullets.Count; i++)
            {
                spriteBatch.Draw(bulletTexture, camera.getVisualCords(bullets[i].Position), Color.White);
                if (bullets[i].Update(timeElapsed))
                {
                    bullets.Remove(bullets[i]);
                }
            }
        }

        
    }
}
