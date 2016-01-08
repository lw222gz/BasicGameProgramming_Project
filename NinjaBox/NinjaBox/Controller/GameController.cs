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
            foreach (Platform p in activeLevel.Levelplatforms)
            {
                p.Update(ElapsedTime);
            }

            //-- Platform collision and checks if a player is on a platform, if so the player can jump
            PlayerIsOnPlatfrom = false;
            foreach (Platform p in activeLevel.Levelplatforms)
            {
                if (gameCollisions.CheckPlatformCollision(p))
                {
                    PlayerIsOnPlatfrom = true;
                    if (p.IsMoving && p.MovingDirection == Direction.Horizontal)
                    {
                        //if the platform is moving the player moves with the platform
                        activeLevel.Player.standingOnMovingPlatform(p.Speed, ElapsedTime);
                    }
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
            //contains things as attack collision or enemy collision.
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
                        //effect when enemy dies      
                        mainView.EnemyDead(activeLevel.Enemies[i].Position);
                        //removes the enemy obj
                        activeLevel.RemoveEnemy(activeLevel.Enemies[i]);
                                          
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

        internal void SetGameLevel(int index)
        {
            CurrentLevel = index;
            setCurrentGameLevel();
            activeLevel.Player.Reset();
        }
    }
}
