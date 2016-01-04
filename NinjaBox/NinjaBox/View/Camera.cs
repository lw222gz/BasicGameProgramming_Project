using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    class Camera
    {
        private float ScreenWidth;
        private float ScreenHeight;
        private float currentPlatformHeight;
        private Vector2 cameraOffset;
        public Camera(GraphicsDevice device)
        {
            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
            cameraOffset = new Vector2(0, 0);

            currentPlatformHeight = 0;
            
        }
        public Vector2 CameraOffSet
        {
            get { return cameraOffset; }
        }

        /// <summary>
        /// default visual coords transformer
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition)
        {
            modelPosition -= cameraOffset;
            return new Vector2((modelPosition.X * ScreenWidth), (modelPosition.Y * ScreenHeight));
        }

        /// <summary>
        /// used when getting visual cords for a platform
        /// </summary>
        /// <param name="modelPosition"></param>
        /// <param name="modifier"></param>
        /// <returns></returns>
        public Vector2 getVisualCords(Vector2 modelPosition, float modifier)
        {
            modelPosition -= cameraOffset;
            return new Vector2(((modelPosition.X * ScreenWidth) + modifier * ScreenWidth), (modelPosition.Y * ScreenHeight));
        }

        public Vector2 GetScale(Vector2 size, Texture2D texture)
        {
            return new Vector2((size.X * ScreenWidth) / texture.Bounds.Width, (size.Y * ScreenHeight) / texture.Bounds.Height);
        }

        //updates the camera offset depending on the player position and if the player is close to the level exit
        public void UpdateCameraOffset(Player player, LevelExit levelExit)
        {
            if (player.Position.X >= 0.5f)
            {
                if (!(player.Position.X + 0.5f >= levelExit.Position.X + levelExit.Size.X / 2))
                {
                    cameraOffset.X = (player.Position.X - 0.5f);
                }                
            }
            else 
            { 
                cameraOffset.X = 0; 
            }
            //if the player can jump, the player is standing on a platform and therefore I will save the playform'
            //Y-led height to have it as the "base" of the game view.
            if (player.Position.Y <= 0.4f)
            {
                cameraOffset.Y = (player.Position.Y - 0.4f);
            }
            else
            {
                cameraOffset.Y = 0;
            }
        }

        /// <summary>
        /// Takes a Vector2 as agrument and transforms it's values to model values
        /// </summary>
        /// <param name="pixelVector">Vector2 that is being transformed.</param>
        /// <returns></returns>
        public Vector2 getModelValue(Vector2 pixelVector)
        {
            return new Vector2(pixelVector.X / ScreenWidth, pixelVector.Y / ScreenHeight);
        }
    }
}
