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
        {   }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) { 
                Exit();
            }
            switch (gameState)
            {
                case GameState.Restart:
                    if (imgui.doButton(ButtonType.Restart, new Vector2(0.2f, 0.2f), null) || Keyboard.GetState().IsKeyDown(Keys.R))
                        {
                            gameController.RestartLevel();
                            gameState = GameState.Running;
                        }
                    break;

                case GameState.MainMenu:
                    if (imgui.doButton(ButtonType.Play, new Vector2(0.8f, 0.2f), null))
                    {
                        gameController.SetGameLevel(1);
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.PlayTutorial, new Vector2(0.8f, 0.5f), null))
                    {
                        gameController.SetGameLevel(0);
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.Credits, new Vector2(0.8f, 0.8f), null))
                    {
                        gameState = GameState.CreditsDisplay;
                    }
                    if (imgui.doButton(ButtonType.LevelSelect, new Vector2(0.5f, 0.2f), null))
                    {
                        gameState = GameState.LevelSelect;
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
                    if (imgui.doButton(ButtonType.Resume, new Vector2(0.8f, 0.5f), null))
                    {
                        gameState = GameState.Running;
                    }
                    if (imgui.doButton(ButtonType.MainMenu, new Vector2(0.8f, 0.2f), null))
                    {
                        gameState = GameState.MainMenu;
                        gameController.SetGameLevel(1);
                    }
                    break;



                case GameState.CreditsDisplay:
                    if (imgui.doButton(ButtonType.MainMenu, new Vector2(0.8f, 0.2f), null))
                    {
                        gameState = GameState.MainMenu;
                    }
                    break;


                case GameState.LevelSelect:
                    float yPos = 0.2f;

                    if (imgui.doButton(ButtonType.MainMenu, new Vector2(0.8f, 0.2f), null))
                    {
                        gameState = GameState.MainMenu;
                    }

                    //starts the current level
                    if (imgui.doButton(ButtonType.Play, new Vector2(0.8f, 0.5f), null))
                    {
                        gameState = GameState.Running;
                    }

                    for (int i = 1; i <= gameLevels.getAmountOfLevels; i++)
                    {
                        if (imgui.doButton(ButtonType.NumberButton, new Vector2(0.2f, yPos * i), i.ToString()))
                        {
                            gameController.SetGameLevel(i);
                        }
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
