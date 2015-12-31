using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NinjaBox.Controller;
using NinjaBox.Model;
using NinjaBox.View;
using NinjaBox.View.MenuView;

namespace NinjaBox
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MasterController : Game
    {
        private GameState gameState;

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

            gameState = GameState.MainMenu;

            Content.RootDirectory = "Content";
        }
        
        //The enum should only be abel to get set values from other classes
        public GameState SetGameState
        {
            set { gameState = value; }
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
            mainView = new MainView(GraphicsDevice, Content, imgui);
            gameController = new GameController(gameLevels, mainView, this);

            this.IsMouseVisible = true;      
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { 
                Exit();
            }
            switch (gameState)
            {
                case GameState.Restart:
                        if (imgui.doButton(ButtonType.Restart) || Keyboard.GetState().IsKeyDown(Keys.R))
                        {
                            gameController.RestartLevel();
                            gameState = GameState.Running;
                        }
                    break;

                case GameState.MainMenu:
                    if (imgui.doButton(ButtonType.Play))
                    {
                        gameController.SetFirstGameLevel();
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.PlayTutorial))
                    {
                        gameController.SetTutorialLevel();
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.Credits))
                    {
                        gameState = GameState.CreditsDisplay;
                    }
                    break;

                case GameState.Running:
                        //movement horizontal
                        if (Keyboard.GetState().IsKeyDown(Keys.Left))
                        {
                            gameController.ActiveLevel.Player.PlayerWantsToMoveLeft();
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.Right))
                        {
                            gameController.ActiveLevel.Player.PlayerWantsToMoveRight();
                        }

                        //Jumping
                        if (Keyboard.GetState().IsKeyDown(Keys.Space) && gameController.ActiveLevel.Player.PlayerCanJump)
                        {
                            gameController.ActiveLevel.Player.PlayerJump((float)gameTime.ElapsedGameTime.TotalSeconds);
                        }
                        if (!gameController.ActiveLevel.Player.PlayerCanJump && Keyboard.GetState().IsKeyUp(Keys.Space))
                        {
                            gameController.ActiveLevel.Player.ReduceJump();
                        }

                        //Attack
                        if (!gameController.ActiveLevel.Player.PlayerIsAttacking && Keyboard.GetState().IsKeyDown(Keys.V))
                        {
                            gameController.PlayerAttack((float)gameTime.ElapsedGameTime.TotalSeconds);
                        }

                        if (Keyboard.GetState().IsKeyDown(Keys.P))
                        {
                            gameState = GameState.Pause;
                        }
                    break;


                case GameState.Pause:
                    if (imgui.doButton(ButtonType.Resume))
                    {
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.MainMenu))
                    {
                        gameState = GameState.MainMenu;
                        gameController.SetFirstGameLevel();
                    }
                    break;



                case GameState.CreditsDisplay:
                    if (imgui.doButton(ButtonType.MainMenu))
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;
            }

            //if the gameState is not paused the game should keep updating.
            if (gameState != GameState.Pause)
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

            mainView.DrawGame(gameController.ActiveLevel, (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (gameState == GameState.CreditsDisplay)
            {
                mainView.DisplayCredits();
            }
            base.Draw(gameTime);
        }
    }
}
