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
        private Texture2D enemyTexture;
        private Texture2D detectionTexture;
        private Texture2D bulletTexture;

        private SpriteEffects spriteEffect;

        public EnemyView()
        {
            enemyTexture = content.Load<Texture2D>("Enemy.png");
            detectionTexture = content.Load<Texture2D>("EnemyDetectionArea.png");
            bulletTexture = content.Load<Texture2D>("BulletPlaceholder.png");
        }

        /// <summary>
        /// Draws an enemy obj
        /// </summary>
        /// <param name="e">enemy obj that is drawn</param>
        public void DrawEnemies(Enemy e){                   
                //default direction for the image
                if (e.EnemyFaceDirection == Direction.Left)
                {
                    spriteEffect = SpriteEffects.None;
                }
                else
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;                   
                }
                
                //Detection area is draw first so that it wont overlap with the enemies heads
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
                                new Vector2(enemyTexture.Bounds.Width / 2, enemyTexture.Bounds.Height / 2),
                                1f,
                                spriteEffect,
                                0);
        }
    }
}
