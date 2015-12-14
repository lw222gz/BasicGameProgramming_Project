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

        public GameController(GameLevels gameLevels)
        {
            CurrentLevel = 1;
            this.gameLevels = gameLevels;
            setCurrentGameLevel();
        }

        //properties
        public Level ActiveLevel
        {
            get { return activeLevel; }
        }

        public void UpdateGame(float ElapsedTime)
        {
            activeLevel.Player.Update(ElapsedTime);

            foreach (Platform p in activeLevel.Levelplatforms)
            {
                activeLevel.Player.CheckPlatformCollision(p);
            }

            foreach (Enemy e in activeLevel.Enemies)
            {
                activeLevel.Player.CheckEnemyCollision(e);
                if (activeLevel.Player.IsPlayerDetected(e))
                {
                    //CONTINUE HERE: Player has just died and need option to restart.

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
