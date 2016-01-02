using NinjaBox.Model;
using NinjaBox.Model.GameObjects;
using NinjaBox.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Controller
{
    /// <summary>
    /// Updates all game objects
    /// </summary>
    class GameController
    {
        private Level activeLevel;
        private GameLevels gameLevels;
        private GameCollisions gameCollisions;
        private MasterController masterController;
        private MainView mainView;
        private int CurrentLevel;

        private bool isGamePaused;

        private bool PlayerIsOnPlatfrom;

        public GameController(GameLevels gameLevels, MainView mainView ,MasterController masterController)
        {
            CurrentLevel = 1;
            this.mainView = mainView;
            this.gameLevels = gameLevels;
            this.masterController = masterController;
            gameCollisions = new GameCollisions();
            setCurrentGameLevel();
            isGamePaused = false;
        }

        //properties
        public Level ActiveLevel
        {
            get { return activeLevel; }
        }
        public bool IsGamePaused
        {
            get { return isGamePaused; }
        }

        public void UpdateGame(float ElapsedTime)
        {
            activeLevel.Player.Update(ElapsedTime);

            foreach (Enemy e in activeLevel.Enemies)
            {
                e.Update(ElapsedTime);
            }

            //-- Platform collision and checks if a player is on a platform, if so the player can jump
            //TODO: refactor this functionality, I feel like there is alot of ways this can be done better but for now Ill leave it like this.
            PlayerIsOnPlatfrom = false;
            foreach (Platform p in activeLevel.Levelplatforms)
            {
                if (gameCollisions.CheckPlatformCollision(p))
                {
                    PlayerIsOnPlatfrom = true;
                }
            }
            if (!PlayerIsOnPlatfrom)
            {
                activeLevel.Player.PlayerCantJump();
            }
            else
            {
                activeLevel.Player.SetPlayerCanJump();
            }
            //-- 

            //Statements only checked if the player is alive.
            if (activeLevel.Player.IsAlive)
            {
                //Checks all enemy collison, attacking, gets detected or if the player gets too close
                for (int i = 0; i < activeLevel.Enemies.Count; i++)
                {
                    //checks if the player is too close or is detected by an enemy
                    gameCollisions.CheckEnemyCollision(activeLevel.Enemies[i]);

                    if (gameCollisions.IsPlayerDetected(activeLevel.Enemies[i]))
                    {
                        PlayerDied();
                        activeLevel.Player.ReduceJump();
                        activeLevel.Enemies[i].KilledPlayer(activeLevel.Player.Position);
                        //Visual effect for shooting the player
                        mainView.ShootPlayer(activeLevel.Enemies[i], activeLevel.Player);
                    }
                    

                    //checks if the player has hit an enemy
                    if (activeLevel.Player.PlayerIsAttacking && gameCollisions.CheckPlayerAttackArea(activeLevel.Enemies[i]))
                    {
                        activeLevel.RemoveEnemy(activeLevel.Enemies[i]);
                        //effect when enemy dies
                        mainView.EnemyDead();
                    } 
                }

                foreach (SecurityCamera securityCam in activeLevel.LevelCameras)
                {
                    if (securityCam.IsTurnedOn && gameCollisions.CheckCameraDetection(securityCam))
                    {
                        PlayerDied();
                    }
                }

                for (int i = 0; i < activeLevel.LevelPowerBoxes.Count; i++)
                {
                    if (activeLevel.Player.PlayerIsAttacking)
                    {
                        int a = 5;
                    }
                    if (activeLevel.Player.PlayerIsAttacking && 
                        gameCollisions.CheckPlayerAttackArea(activeLevel.LevelPowerBoxes[i]))
                    {
                        activeLevel.DestroyPowerBox(activeLevel.LevelPowerBoxes[i]);
                    }
                }

                //if the player is under the level the player dies
                if (activeLevel.Player.Position.Y - activeLevel.Player.Size.Y / 2 > 1)
                {
                    PlayerDied();
                    //effect when player dies by falling down
                    mainView.PlayerFall();
                }

                //checks if the player reached the end of the level
                if (activeLevel.LevelExit != null && gameCollisions.IsLevelCompleted(activeLevel.LevelExit))
                {
                    if (CurrentLevel == 0)
                    {
                        masterController.SetGameState = GameState.MainMenu;
                    }
                    CurrentLevel += 1;
                    activeLevel = gameLevels.getCurrentLevel(CurrentLevel);
                }
            }
        }


        /// <summary>
        /// Sets the activeLevel obj to the value of the current level the player is on
        /// </summary>
        public void setCurrentGameLevel()
        {
            activeLevel = gameLevels.getCurrentLevel(CurrentLevel);
        }

        /// <summary>
        /// Kills the player 
        /// </summary>
        private void PlayerDied()
        {
            activeLevel.Player.PlayerDead();
            masterController.SetGameState = GameState.Restart;
        }
        //restarts the current level
        public void RestartLevel()
        {
            activeLevel = gameLevels.ResetLevel(CurrentLevel);
        }

        public void PlayerAttack(float elapsedTime)
        {
            activeLevel.Player.PlayerAttack();
        }


        public void SetTutorialLevel()
        {
            CurrentLevel = 0;
            setCurrentGameLevel();
        }

        public void SetFirstGameLevel()
        {
            CurrentLevel = 1;
            setCurrentGameLevel();
            activeLevel.Player.Reset();
        }
    }
}
