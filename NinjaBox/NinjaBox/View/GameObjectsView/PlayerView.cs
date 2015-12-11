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
    class PlayerView
    {
        private SpriteBatch spriteBatch;
        private Camera camera;
        private Vector2 playerScale;

        private Texture2D playerTexture;

        public PlayerView(Camera camera, ContentManager content, SpriteBatch spriteBatch)
        {
            this.camera = camera;
            this.spriteBatch = spriteBatch;

            playerTexture = content.Load<Texture2D>("PlaceHolderHero.png");
        }

        public void DrawPlayer(Player player)
        {
            playerScale = camera.GetScale(player.Size, playerTexture);

            spriteBatch.Begin();

            spriteBatch.Draw(playerTexture, 
                    camera.getVisualCords(player.Position), 
                    null,
                    Color.White,
                    0f,
                    new Vector2((playerScale.X * playerTexture.Bounds.Width) /2, (playerScale.Y * playerTexture.Bounds.Height) / 2),
                    playerScale,
                    SpriteEffects.None,
                    0);

            spriteBatch.End();
        }
    }
}
