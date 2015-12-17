using NinjaBox.Model;
using NinjaBox.Model.GameObjects;
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
        private int CurrentLevel;
        private bool isPlayerDead;

        private int Counter;

        public GameController(GameLevels gameLevels)
        {
            CurrentLevel = 1;
            this.gameLevels = gameLevels;
            setCurrentGameLevel();
            isPlayerDead = false;
        }

        //properties
        public Level ActiveLevel
        {
            get { return activeLevel; }
        }
        public bool IsPlayerDead
        {
            get { return isPlayerDead; }
        }

        public void UpdateGame(float ElapsedTime)
        {
            activeLevel.Player.Update(ElapsedTime);

            //-- Platform collision and checks if a player is on a platform, if so the player can jump
            //TODO: refactor this functionality, I feel like there is alot of ways this can be done better but for now Ill leave it like this.
            Counter = 0;
            foreach (Platform p in activeLevel.Levelplatforms)
            {
                if (!activeLevel.Player.CheckPlatformCollision(p))
                {
                    Counter += 1;
                }
            }
            if (Counter == activeLevel.Levelplatforms.Count)
            {
                activeLevel.Player.PlayerCantJump();
            }
            else
            {
                activeLevel.Player.SetPlayerCanJump();
            }
            //-- 

            foreach (Enemy e in activeLevel.Enemies)
            {
                activeLevel.Player.CheckEnemyCollision(e);
                if (activeLevel.Player.IsPlayerDetected(e))
                {
                    isPlayerDead = true;
                }
                e.Update(ElapsedTime);
            }
        }


        /// <summary>
        /// Sets the activeLevel obj to the value of the current level the player is on
        /// </summary>
        public void setCurrentGameLevel()
        {
            activeLevel = gameLevels.getCurrentLevel(CurrentLevel);
        }


    }
}
