using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class SecurityCameraView : MainView
    {
        private Texture2D securityCameraTexture;
        private Texture2D securityCameraDetectionAreaTexture;
        public SecurityCameraView()
        {
            securityCameraTexture = content.Load<Texture2D>("SecurityCameraPlaceholder.jpg");
            securityCameraDetectionAreaTexture = content.Load<Texture2D>("CameraDetectionAreaPlaceholder.png");
        }

        //draws all the levels security cameras
        public void DrawSecurityCameras(List<SecurityCamera> securityCameras)
        {
            foreach (SecurityCamera securityCam in securityCameras)
            {
                spriteBatch.Draw(securityCameraTexture, 
                                camera.getVisualCords(securityCam.Position),
                                null,
                                Color.Azure, 
                                0,                    
                                new Vector2(securityCameraTexture.Bounds.Width / 2, securityCameraTexture.Bounds.Height / 2),
                                1f,
                                SpriteEffects.None,
                                0);

                if (securityCam.IsTurnedOn)
                {
                    spriteBatch.Draw(securityCameraDetectionAreaTexture,
                                    camera.getVisualCords(securityCam.DetectionAreaPosition),
                                    null,
                                    Color.White,
                                    0,
                                    new Vector2(securityCameraDetectionAreaTexture.Bounds.Width/2, 0),
                                    1,
                                    SpriteEffects.None,
                                    0);
                }
            }
        }
    }
}
