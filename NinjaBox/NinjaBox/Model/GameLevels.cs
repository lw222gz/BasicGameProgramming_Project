using Microsoft.Xna.Framework;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model
{
    class GameLevels
    {
        private List<Level> gameLevels;
        //private const int amountOfLevels = 2;
        private static Player player;


        private List<char[,]> levelMakerGrid;

        public GameLevels()
        {
            player = new Player();
            gameLevels = new List<Level>(3);
            levelMakerGrid = new List<char[,]>(3);
            setGameLevels();
            LoadLevels();
        }


        //BONUS TODO: Rebuild it to a gird system, example:
        
        /// - = empty square
        /// / = platform
        /// ! = enemy
        /// $ = player position
        /// * = level end
        /// base grid copy for a new level:
        /// each position represents 0.1 model coordinates. This grid is Y: 10 X:40
        /*new char[,] {
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'},
                    {'~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~'}
                }*/


        /// <summary>
        /// ~ = empty
        /// - = platform
        /// < = enemy facing left by default
        /// > = enemy facing right by default
        /// * = level end
        /// $ = player start position
        /// </summary>
        private void setGameLevels()
        {
            levelMakerGrid.Add(new char[,] {
                                {'~','~','~','~','~','~','~','~','~','-','~','~','~','~','~','~','~','~','~','~'},
                                {'~','~','~','~','~','~','~','~','-','~','-','~','~','~','~','~','~','~','~','*'},
                                {'~','~','~','~','~','~','~','-','~','~','~','-','~','~','~','~','~','~','~','~'},
                                {'~','~','~','~','~','~','-','~','~','~','~','~','-','~','~','~','~','~','~','~'},
                                {'~','~','~','~','~','-','~','~','~','~','~','~','~','-','~','~','~','~','~','~'},
                                {'~','~','~','~','-','~','~','~','~','~','~','~','~','~','-','~','~','~','~','~'},
                                {'~','~','~','-','~','~','~','~','~','~','~','~','~','~','~','-','~','~','~','~'},
                                {'~','~','-','~','~','~','~','~','~','~','~','~','~','~','~','~','-','~','~','~'},
                                {'~','-','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','-','~','-'},
                                {'-','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','~','-','~'}
                            });
                            
        }


        /// <summary>
        /// returns the Level obj of the current level
        /// </summary>
        /// <param name="currentLevel"> represents the current level, not in list index tho so it has to be reduced by 1</param>
        /// <returns></returns>
        public Level getCurrentLevel(int currentLevel)
        {
            Level level = gameLevels[currentLevel - 1];
            player.SetPlayerPosition(level.LevelStartPosition);
            return level;
        }
        /// <summary>
        /// Loads all game levels
        /// </summary>
        private void LoadLevels()
        {
            for (int i = 0; i < levelMakerGrid.Count; i++)
            {
                gameLevels.Add(new Level(levelMakerGrid[i], player));
            }
        }

        /// <summary>
        /// Resets the current level
        /// </summary>
        /// <param name="CurrentLevel">int representation of the current level</param>
        /// <returns>new level object with reset values for said level</returns>
        public Level ResetLevel(int CurrentLevel)
        {
            /*Level level = new Level(CurrentLevel, player);
            gameLevels[CurrentLevel - 1] = level;
            player.Restart(level.LevelStartPosition);
            return gameLevels[CurrentLevel - 1];*/
        }
    }
}
