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
        private Vector2 enemyScale;
        private SpriteEffects spriteEffect;

        public EnemyView()
        {
            enemyTexture = content.Load<Texture2D>("EnemyPlaceHolder.png");
            detectionTexture = content.Load<Texture2D>("DetectionAreaPlaceholder.png");
        }

        public void DrawEnemies(List<Enemy> Enemies){

            
            spriteBatch.Begin();            

            //SpriteEffect.FlipHorizontally
            foreach (Enemy e in Enemies)
            {
                enemyScale = camera.GetScale(e.Size, enemyTexture);

                //default direction for the image
                if (e.EnemyDirection == Direction.Left)
                {
                    spriteEffect = SpriteEffects.None;
                }
                else
                {
                    spriteEffect = SpriteEffects.FlipHorizontally;
                }

                spriteBatch.Draw(enemyTexture,
                                camera.getVisualCords(e.Position),
                                null,
                                Color.White,
                                0f,
                                new Vector2((enemyScale.X * enemyTexture.Bounds.Width) / 2, (enemyScale.Y * enemyTexture.Bounds.Height) / 2),
                                enemyScale,
                                spriteEffect,
                                0);

                spriteBatch.Draw(detectionTexture,
                                camera.getVisualCords(e.DetectionAreaPosition),
                                null,
                                Color.Yellow,
                                0f,
                                Vector2.Zero,
                                1f,
                                spriteEffect,
                                0);
                            
            }

            spriteBatch.End();
            return;
        }
    }
}
