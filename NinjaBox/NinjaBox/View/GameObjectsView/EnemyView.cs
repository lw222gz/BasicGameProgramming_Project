using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class EnemyView : MainView
    {
        private const float bulletSpeed = 0.5f;
        private float shotTimer;
        private Vector2 gunLocation;

        private Texture2D enemyTexture;
        private Texture2D detectionTexture;
        private Texture2D bulletTexture;
        private Vector2 enemyScale;
        
        private SpriteEffects spriteEffect;

        public EnemyView()
        {
            shotTimer = 0;
            gunLocation = new Vector2(0, -0.01f);
            enemyTexture = content.Load<Texture2D>("Enemy.png");
            detectionTexture = content.Load<Texture2D>("EnemyDetectionArea.png");
            bulletTexture = content.Load<Texture2D>("BulletPlaceholder.png");
        }

        public void DrawEnemies(List<Enemy> Enemies, float elapsedTime){                   

            foreach (Enemy e in Enemies)
            {
                enemyScale = camera.GetScale(e.Size, enemyTexture);

                //default direction for the image
                if (e.EnemyFaceDirection == Direction.Left)
                {
                    spriteEffect = SpriteEffects.None;
                    gunLocation.X = -(e.Size.X / 2) - 0.0070f;
                }
                else
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;
                    gunLocation.X = e.Size.X / 2;
                }

                spriteBatch.Draw(detectionTexture,
                                camera.getVisualCords(e.DetectionAreaPosition),
                                null,
                                new Color(0.7f, 0.7f, 0.7f, 0.7f),
                                0f,
                                Vector2.Zero,
                                1f,
                                spriteEffect,
                                0);

                spriteBatch.Draw(enemyTexture,
                                camera.getVisualCords(e.Position),
                                null,
                                Color.White,
                                0f,
                                new Vector2((enemyScale.X * enemyTexture.Bounds.Width) / 2, (enemyScale.Y * enemyTexture.Bounds.Height) / 2),
                                enemyScale,
                                spriteEffect,
                                0);

                if (e.HasKilledPlayer)
                {
                    //shoot bullets to the players location
                    shotTimer += elapsedTime;
                    Vector2 bulletLocation = e.Position + gunLocation;
                    bulletLocation.X += shotTimer * bulletSpeed;
                    bulletLocation.Y += shotTimer * bulletSpeed;
                    spriteBatch.Draw(bulletTexture, camera.getVisualCords(bulletLocation), Color.White);
                }

                
                            
            }

        }
    }
}
