using NinjaBox.Model;
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
            return;
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
