using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.Model;
using NinjaBox.Model.GameObjects;
using NinjaBox.View.GameObjectsView;
using NinjaBox.View.VisualEffects;
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

        //View classes for game objects
        private PlatformView platformView;
        private EnemyView enemyView;
        private PlayerView playerView;
        private LevelExitView levelExitView;
        private MessagesView messageView;
        private IMGUI imgui;

        //Main textures
        private Texture2D menuBackgroundTexture;
        private Texture2D backGroundTexture;
        private Texture2D backGroundWinTexture;
        private Texture2D activeBackgroundTexture;


        //Special visual effects that can be executed on command  
        //List of all ingame visual effects
        private List<IEffect> visualEffects;

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
            //need to load all buttons here when the protected static content field has gotten it's value
            imgui.LoadButtons();

            playerView = new PlayerView();
            platformView = new PlatformView();
            enemyView = new EnemyView();
            levelExitView = new LevelExitView();
            messageView = new MessagesView();

            menuBackgroundTexture = content.Load<Texture2D>("MenuBackground.png");
            backGroundTexture = content.Load<Texture2D>("GameBackground.png");
            backGroundWinTexture = content.Load<Texture2D>("WinBackgroundPlaceholder.png");

            activeBackgroundTexture = backGroundTexture;

            visualEffects = new List<IEffect>(5);
        }

        public void DrawGame(Level level, float elapsedTime)
        {
            //updates the camera offset so the screen follows the player
            camera.UpdateCameraOffset(level.Player, level.LevelExit);

            spriteBatch.Begin();

            //The background allways have the same model coords as the camera offset
            if (level.Player.HasFinishedGame)
            {
                activeBackgroundTexture = backGroundWinTexture;
            }            
            spriteBatch.Draw(activeBackgroundTexture, camera.getVisualCords(camera.CameraOffSet), Color.White);

            //if the level has any messages they are drawn
            if (level.LevelMessages != null)
            {
                messageView.DrawMessages(level.LevelMessages);
            }

            //calls on draw functions for level objects
            foreach (Enemy e in level.Enemies)
            {
                enemyView.DrawEnemies(e);
            }

            for (int i = 0; i < visualEffects.Count; i++)
            {
                visualEffects[i].RunEffect(elapsedTime);
                if (visualEffects[i].IsEffectOver)
                {

                }
            }

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

        public void ShootPlayer(Enemy enemy, Player player)
        {
            visualEffects.Add(new EnemyShootingPlayerEffect(enemy, player.Velocity, player.PlayerCanJump));
        }
    }
}
