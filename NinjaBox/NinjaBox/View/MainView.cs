using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model;
using NinjaBox.View.GameObjectsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    class MainView
    {
        protected static Camera camera;
        protected static SpriteBatch spriteBatch;
        protected static ContentManager content;

        private PlatformView platformView;
        private EnemyView enemyView;
        private PlayerView playerView;
        private IMGUI imgui;

        private Texture2D backGroundTexture;
        private Vector2 backGroundPosition;

        /// <summary>
        /// default constructor
        /// </summary>
        public MainView() { }

        /// <summary>
        /// Constructor with 2 params
        /// </summary>
        /// <param name="device"></param>
        /// <param name="content"></param>
        public MainView(GraphicsDevice device, ContentManager _content, IMGUI imgui)
        {
            camera = new Camera(device);
            spriteBatch = new SpriteBatch(device);
            content = _content;
            this.imgui = imgui;

            playerView = new PlayerView();
            platformView = new PlatformView();
            enemyView = new EnemyView();

            backGroundTexture = content.Load<Texture2D>("GameBackground.png");
            backGroundPosition = new Vector2(0, 0);
        }

        public void DrawGame(Level level)
        {
            //updates the camera offset so the screen follows the player
            camera.UpdateCameraOffset(level.Player);

            spriteBatch.Begin();

            if (level.Player.Position.X > 0.5f)
            {
                backGroundPosition.X = level.Player.Position.X - 0.5f;
            }                
            if(level.Player.Position.Y < 0.3f)
            {
                backGroundPosition.Y = level.Player.Position.Y - 0.3f;
            }

            spriteBatch.Draw(backGroundTexture, camera.getVisualCords(backGroundPosition), Color.White);
            
           

            //calls on draw functions
            enemyView.DrawEnemies(level.Enemies);


            platformView.DrawPlatforms(level.Levelplatforms);

            playerView.DrawPlayer(level.Player);




            if (imgui.ActiveMenu != MenuType.None)
            {
                imgui.DrawMenu();
            }

            spriteBatch.End();

            
        }
    }
}
