using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NinjaBox.Controller;
using NinjaBox.Model;
using NinjaBox.View;

namespace NinjaBox
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MasterController : Game
    {
        GraphicsDeviceManager graphics;

        private GameLevels gameLevels;
        private GameController gameController;
        private MainView mainView;
        private IMGUI imgui;
        

        public MasterController()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1400;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            imgui = new IMGUI();
            gameLevels = new GameLevels();
            gameController = new GameController(gameLevels);
            mainView = new MainView(GraphicsDevice, Content, imgui);
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //TODO:Menu - have a enum GameState and defines the state of the game, paused, start menu, game over manu etc.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) { 
                Exit();
            }
            if (!gameController.ActiveLevel.Player.IsPlayerDead && !gameController.IsGamePaused)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    gameController.ActiveLevel.Player.PlayerWantsToMoveLeft = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    gameController.ActiveLevel.Player.PlayerWantsToMoveRight = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameController.ActiveLevel.Player.PlayerCanJump)
                {
                    gameController.ActiveLevel.Player.PlayerJump();
                }

                
            }
            else if(gameController.ActiveLevel.Player.IsPlayerDead)
            {
                this.IsMouseVisible = true;
                if (imgui.doButton(ButtonType.Restart))
                {
                    gameController.RestartLevel();
                }
            }

            if (!gameController.IsGamePaused)
            {
                gameController.UpdateGame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            mainView.DrawGame(gameController.ActiveLevel);

            base.Draw(gameTime);
        }
    }
}
