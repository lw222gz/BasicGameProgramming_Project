using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model
{
    class GameLevels
    {
        private List<Level> gameLevels;
        private const int amountOfLevels = 1;

        public GameLevels()
        {
            gameLevels = new List<Level>(3);
            ResetGameLevels();
        }


        /// <summary>
        /// returns the Level obj of the current level
        /// </summary>
        /// <param name="currentLevel"> represents the current level, not in list index tho so it has to be reduced by 1</param>
        /// <returns></returns>
        public Level getCurrentLevel(int currentLevel)
        {
            return gameLevels[currentLevel - 1];
        }
        /// <summary>
        /// Resets all game levels to their default
        /// </summary>
        private void ResetGameLevels()
        {
            for (int i = 0; i < amountOfLevels; i++)
            {
                gameLevels.Add(new Level(i + 1));
            }
        }
    }
}
