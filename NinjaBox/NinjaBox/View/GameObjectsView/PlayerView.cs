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
    class PlayerView : MainView
    {
        private Texture2D playerTexture;
        private Texture2D attackTexture;
        private float attackDirectionModifier;

        /// <summary>
        /// loads in all textures
        /// </summary>
        public PlayerView() 
        {
            playerTexture = content.Load<Texture2D>("Player.png");
            attackTexture = content.Load<Texture2D>("PlaceholderAttack.png");
        }

        /// <summary>
        /// Draws the player obj
        /// </summary>
        /// <param name="player"></param>
        public void DrawPlayer(Player player)
        {
            //adds the attack texture
            if (player.PlayerIsAttacking)
            {
                attackDirectionModifier = player.Size.X / 2 + player.AttackRange.X / 2;
                if (player.PlayerDirection == Direction.Left)
                {
                    attackDirectionModifier = -(attackDirectionModifier);
                }
                spriteBatch.Draw(attackTexture,
                                camera.getVisualCords(new Vector2(player.Position.X + attackDirectionModifier, player.Position.Y)),
                                null,
                                Color.White,
                                0f,
                                new Vector2(attackTexture.Bounds.Width / 2, attackTexture.Bounds.Height / 2),
                                1f,
                                SpriteEffects.None,
                                0);
            }

            spriteBatch.Draw(playerTexture, 
                    camera.getVisualCords(player.Position), 
                    null,
                    Color.White,
                    0f,
                    new Vector2(playerTexture.Bounds.Width /2, playerTexture.Bounds.Height / 2),
                    1f,
                    SpriteEffects.None,
                    0);

            
        }
    }
}
