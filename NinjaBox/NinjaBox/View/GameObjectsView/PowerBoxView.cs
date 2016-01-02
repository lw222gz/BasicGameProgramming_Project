using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.GameObjectsView
{
    class PowerBoxView : MainView
    {
        private Texture2D powerBoxTexture;
        private Texture2D destroyedPowerBoxTexture;
        private Texture2D activePowerBoxTexture;
        public PowerBoxView()
        {
            powerBoxTexture = content.Load<Texture2D>("SecurityCameraPowerBoxPlaceholder.png");
            destroyedPowerBoxTexture = content.Load<Texture2D>("DestroyedPowerBoxPlaceholder.png");
        }

        //draws all the game levels powerboxes
        public void DrawPowerBoxes(List<PowerBox> levelPowerBoxes)
        {
            foreach(PowerBox pb in levelPowerBoxes)
            {
                if (pb.IsActive)
                {
                    activePowerBoxTexture = powerBoxTexture;
                }
                else
                {
                    activePowerBoxTexture = destroyedPowerBoxTexture;
                }

                spriteBatch.Draw(activePowerBoxTexture, 
                    camera.getVisualCords(pb.Position),
                    null,
                    Color.White,
                    0,
                    new Vector2(activePowerBoxTexture.Bounds.Width /2, activePowerBoxTexture.Bounds.Height /2),
                    1,
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
