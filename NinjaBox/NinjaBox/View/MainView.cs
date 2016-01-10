using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
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
    /// <summary>
    /// This class handels all the other views and draws the entire game. 
    /// All other classes that draws NEED to inherit from this class
    /// All of these classes should have a default constructor with 0 parameters to load in all of it's content
    /// </summary>
    class MainView
    {
        protected static Camera camera;
        protected static SpriteBatch spriteBatch;
        protected static ContentManager content;
        //message view is protected to allow other draw classes to write out messages
        protected static MessagesView messageView;

        private Song backgroundMusic;
        private SoundEffect EnemyDiesSound;
        private SoundEffect playerFallSound;

        //View classes for game objects       
        private PlatformView platformView;
        private EnemyView enemyView;
        private PlayerView playerView;
        private LevelExitView levelExitView;        
        private PowerBoxView powerBoxView;
        private SecurityCameraView securityCameraView;
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
            imgui.LoadResources();

            //loads all the visual effects by initializing the constuctors
            playerView = new PlayerView();
            platformView = new PlatformView();
            enemyView = new EnemyView();
            levelExitView = new LevelExitView();
            messageView = new MessagesView();
            powerBoxView = new PowerBoxView();
            securityCameraView = new SecurityCameraView();

            //content used in the mainview
            menuBackgroundTexture = content.Load<Texture2D>("MenuBackground.png");
            backGroundTexture = content.Load<Texture2D>("GameBackground.png");
            backGroundWinTexture = content.Load<Texture2D>("WinBackgroundPlaceholder.png");

            EnemyDiesSound = content.Load<SoundEffect>("EnemyDies");
            playerFallSound = content.Load<SoundEffect>("WilhelmScream");
            backgroundMusic = content.Load<Song>("StealthGroover");


            //these objects are never used but I need to initiate their constructors to load their content
            //and thus their content wont have to load each time a new effect is created.
            EnemyShootingPlayerEffect loadShootingEffectContent = new EnemyShootingPlayerEffect();
            EnemyExplodesEffect loadEnemyExplodeEffect = new EnemyExplodesEffect();
            

            //initiates the background music after all the content has been loaded
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(backgroundMusic);

            activeBackgroundTexture = backGroundTexture;

            visualEffects = new List<IEffect>(5);
        }

        /// <summary>
        /// Draws the entire game
        /// </summary>
        /// <param name="level">level obj for the current level the player is on</param>
        /// <param name="elapsedTime">time elapsed since last update, this value is used for some visual effects</param>
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
                //if the effect is over it is removed from, the list
                if (visualEffects[i].IsEffectOver)
                {
                    visualEffects.Remove(visualEffects[i]);
                }
            }

            platformView.DrawPlatforms(level.Levelplatforms);

            securityCameraView.DrawSecurityCameras(level.LevelCameras);

            powerBoxView.DrawPowerBoxes(level.LevelPowerBoxes);

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

        //Initiates the visual effect of the enemy shooting the player.
        public void ShootPlayer(Enemy enemy, Player player)
        {
            visualEffects.Add(new EnemyShootingPlayerEffect(enemy, player.Velocity, player.PlayerCanJump));
        }

        //TODO -Add visual effect: small explosion
        public void EnemyDead(Vector2 position)
        {
            EnemyDiesSound.Play();
            visualEffects.Add(new EnemyExplodesEffect(position));
        }

        /// <summary>
        /// Overlay draw for the credits, is called when the player wants to see the game credits
        /// </summary>
        public void DisplayCredits()
        {
            spriteBatch.Begin();

            spriteBatch.Draw(menuBackgroundTexture, 
                            camera.getVisualCords(new Vector2(0.1f, 0.1f) + camera.CameraOffSet), 
                            null, 
                            Color.Black, 
                            0, 
                            Vector2.Zero, 
                            new Vector2(0.5f, 0.8f), 
                            SpriteEffects.None, 
                            0);
            messageView.DrawCredits();

            spriteBatch.End();
        }

        //Sound effect when the player falls to death
        public void PlayerFall()
        {
            playerFallSound.Play();
        }
    }
}
