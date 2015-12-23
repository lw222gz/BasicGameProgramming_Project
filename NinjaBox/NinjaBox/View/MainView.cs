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
        private LevelExitView levelExitView;
        private IMGUI imgui;

        private Texture2D menuBackgroundTexture;
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
            //need to load all buttons here when the protected content varible has gotten it's value
            imgui.LoadButtons();

            playerView = new PlayerView();
            platformView = new PlatformView();
            enemyView = new EnemyView();
            levelExitView = new LevelExitView();

            menuBackgroundTexture = content.Load<Texture2D>("MenuBackground.png");
            backGroundTexture = content.Load<Texture2D>("GameBackground.png");
            backGroundPosition = new Vector2(0, 0);
        }

        public void DrawGame(Level level)
        {
            //updates the camera offset so the screen follows the player
            camera.UpdateCameraOffset(level.Player, level.LevelExit);

            spriteBatch.Begin();

            //The background allways have the same model coords as the camera offset
            spriteBatch.Draw(backGroundTexture, camera.getVisualCords(camera.CameraOffSet), Color.White);
            
           

            //calls on draw functions for level objects
            enemyView.DrawEnemies(level.Enemies);


            platformView.DrawPlatforms(level.Levelplatforms);

            

            levelExitView.DrawExit(level.LevelExit);
            playerView.DrawPlayer(level.Player);


            //if a buttons exist then the menu is drawn over the game.
            if (imgui.ActiveButtons.Count > 0)
            {
                spriteBatch.Draw(menuBackgroundTexture, new Vector2(0, 0), new Color(0.7f, 0.7f, 0.7f, 0.7f));
                imgui.DrawMenu();
            }

            spriteBatch.End();

            
        }
    }
}
