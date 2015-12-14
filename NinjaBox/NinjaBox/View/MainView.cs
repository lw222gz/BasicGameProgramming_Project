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

        /// <summary>
        /// default constructor
        /// </summary>
        public MainView() { }

        /// <summary>
        /// Constructor with 2 params
        /// </summary>
        /// <param name="device"></param>
        /// <param name="content"></param>
        public MainView(GraphicsDevice device, ContentManager _content)
        {
            camera = new Camera(device);
            spriteBatch = new SpriteBatch(device);
            content = _content;

            playerView = new PlayerView();
            platformView = new PlatformView();
            enemyView = new EnemyView();
        }

        public void DrawGame(Level level)
        {
            //updates the camera offset so the screen follows the player
            camera.UpdateCameraOffset(level.Player);



            //calls on draw functions
            enemyView.DrawEnemies(level.Enemies);


            platformView.DrawPlatforms(level.Levelplatforms);

            playerView.DrawPlayer(level.Player);

            
        }
    }
}
