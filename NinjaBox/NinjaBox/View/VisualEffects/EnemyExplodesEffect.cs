using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.VisualEffects
{
    class EnemyExplodesEffect : MainView, IEffect
    {
        private static Texture2D explosionTexture;
        private Vector2 position;

        private int frameX;
        private int frameY;
        //Total number of frames in the sprite
        private const int numberOfFrames = 24;
        //total number of images in each led
        private const int NumFramesX = 4;
        private const int NumFramesY = 8;
        
        //time durotation for the explotion (seconds)
        private const float maxTime = .5f;
        private float totalExplosionTime;
        private float percentAnimated;

        /// <summary>
        /// Loads in the textures for the effect
        /// </summary>
        public EnemyExplodesEffect()
        {
            explosionTexture = content.Load<Texture2D>("explosion.png");
        }

        public EnemyExplodesEffect(Vector2 position)
        {
            this.position = position;
            totalExplosionTime = 0;
            percentAnimated = 0;
        }

        /// <summary>
        /// returns true if the effect is over
        /// </summary>
        public bool IsEffectOver
        {
            get { return percentAnimated >= 1; }
        }

        /// <summary>
        /// Updates the effect
        /// </summary>
        /// <param name="timeElapsed">amount of time passed since last update</param>
        public void RunEffect(float timeElapsed)
        {
            UpdateFrame(timeElapsed);

            int spriteXCord = (explosionTexture.Bounds.Width / NumFramesX) * frameX;
            int spriteYCord = (explosionTexture.Bounds.Height / NumFramesY) * frameY;

            spriteBatch.Draw(explosionTexture,
                             camera.getVisualCords(position),
                             new Rectangle(spriteXCord,
                                           spriteYCord,
                                           explosionTexture.Bounds.Width / NumFramesX,
                                           explosionTexture.Bounds.Height / NumFramesY),
                             Color.White,
                             0,
                             new Vector2(explosionTexture.Bounds.Width / (NumFramesX * 2), explosionTexture.Bounds.Height/ (NumFramesY * 2)),
                             1,
                             SpriteEffects.None,
                             0);
        }

        public void UpdateFrame(float timeElapsed)
        {
            totalExplosionTime += timeElapsed;

            percentAnimated = totalExplosionTime / maxTime;
            int frame = (int)(percentAnimated * numberOfFrames);

            //set values for the sprite "grid" 
            frameX = frame % NumFramesX;
            frameY = frame / NumFramesX;
        }
    }
}
