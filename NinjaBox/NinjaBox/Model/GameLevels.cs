﻿using Microsoft.Xna.Framework;
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


        private List<char[,]> levelMatrix;

        public GameLevels()
        {
            player = new Player();
            gameLevels = new List<Level>(5);
            levelMatrix = new List<char[,]>(3);
            setGameLevels();
            LoadLevels();
        }
        
        /// 8 = empty square
        /// / = platform
        /// ! = enemy
        /// $ = player position
        /// * = level end
        /// base grid copy for a new level:
        /// each position represents 0.1 model coordinates. This grid is Y: 1 X:4
        /*new char[,] {
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'}
                }*/


        /// <summary>
        /// 8 = empty
        /// - = platform
        /// < = enemy facing left by default
        /// > = enemy facing right by default
        /// % = Enemy patrol path and it will allways face the same direction it spawns in. This has to be followed by an enemy sign or else it wont have any effect
        /// / = Enemy partol path and it will allways look in the direction it's moving. This has to be followed by an enemy sign or else it wont have any effect
        /// * = level end
        /// + = player start position
        /// 
        /// Idea for camera:
        /// NOT IMPLEMENTED YET IN THE LEVEL TRANSLATOR
        /// Shift + 2-0 num key = Camera
        /// Alt Gr + 2-0 num key = Camera powerbox (has to be destoryed to power down a camera)
        /// </summary>
        private void setGameLevels()
        {
            //TUTORIAL LEVEL, THIS HAS TO BE THE FIRST ITEM IN THE LIST
            levelMatrix.Add(new char[,] {
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','>','%','%','%','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','-','-','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8','8','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','<','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','<','8','8','8','8','8','8','8','8','8','8','8','*'},
                    {'8','8','8','8','8','8','-','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','-','8','8','8','8','8','8','8','8','-','-','-'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'-','-','-','-','-','-','-','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','-','-','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8'}
                });


            //level 1
            levelMatrix.Add(new char[,] {
                    {'+','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'-','-','8','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','<','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','<','%','%','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','-','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','<','8','8','8','8','8','8','<','8','8','8','<','/','/','>','/','/','/','8','8','*'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','-','8','8','8','-','-','-','-','-','-','-','-','-','-'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','-','-','-','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8'}
                });


            //level 2
            //Note: första hindret: tänk utanför lådan. huehuehue
            levelMatrix.Add(new char[,] {
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','"','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','*'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','-','-','-','-','-','-','-','-','-','-','-','-','-'},
                    {'8','8','8','8','8','-','-','-','8','8','<','/','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','%','8','8','8','8','8','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','<'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','-','-','-'},
                    {'8','8','8','8','<','%','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'-','8','8','8','-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8'},
                    {'+','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','@','8','8','c','8','8','8','8','8'},
                    {'-','-','-','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','8','-','-','-','-','-','-','-','-','-','-','-','-','-','-','-'}
                });



            //last level
            //TODO:Better solution than putting an exit in an unreachable area?
            //TODO: Add Win Message
            levelMatrix.Add(new char[,] {
                    {'8','8','8','8','*','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'-','-','-','8','8','8','8','-','-','-'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','-','-','-','-','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'8','8','8','8','8','8','8','8','8','8'},
                    {'-','-','-','-','-','-','-','-','-','-'}
                });
                            
        }


        /// <summary>
        /// returns the Level obj of the current level
        /// </summary>
        /// <param name="currentLevel"> represents the current level, not in list index tho so it has to be reduced by 1</param>
        /// <returns></returns>
        public Level getCurrentLevel(int currentLevel)
        {
            Level level = gameLevels[currentLevel];
            player.SetPlayerPosition(level.LevelStartPosition);
            if (currentLevel == gameLevels.Count - 1)
            {
                player.CompletedGame();
            }
            return level;
        }
        /// <summary>
        /// Loads all game levels
        /// </summary>
        private void LoadLevels()
        {
            for (int i = 0; i < levelMatrix.Count; i++)
            {               
                gameLevels.Add(new Level(levelMatrix[i], player, i));
            }
        }

        /// <summary>
        /// Resets the current level
        /// </summary>
        /// <param name="CurrentLevel">int representation of the current level</param>
        /// <returns>new level object with reset values for said level</returns>
        public Level ResetLevel(int CurrentLevel)
        {
            Level level = new Level(levelMatrix[CurrentLevel], player, CurrentLevel);

            gameLevels[CurrentLevel] = level;
            player.SetPlayerPosition(level.LevelStartPosition);
            player.Alive();
            return gameLevels[CurrentLevel];
        }
    }
}
