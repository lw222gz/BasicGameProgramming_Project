using Microsoft.Xna.Framework;
using NinjaBox.Model.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model
{
    class Level
    {
        private List<Platform> levelPlatforms;
        private List<Enemy> enemies;
        private Player player;
        private LevelExit levelExit;
        private Vector2 playerStartPosition;
        private int LevelDeadZone;

        private int platformStart;
        private int modelYCordModifier;
        private bool isBuildingPlatform;
        private Vector2 platformStartPoint;
        private float platformXEndPoint;


        public Level(char[,] levelDesign, Player player)
        {
            this.player = player;
            levelPlatforms = new List<Platform>(10);
            enemies = new List<Enemy>(10); 
            LevelTranslator(levelDesign);
            playerStartPosition = new Vector2(0.1f, 0.85f);
        }

        private void LevelTranslator(char[,] levelDesign)
        {
            isBuildingPlatform = false;

            LevelDeadZone = levelDesign.GetLength(0);
            

            //this modifier takes action if there are more than 10 rows in Y led and makes sure that the bottom of the screen is allways value 1 in Y cords
            //that makes it easier for the calculation of the camera off set (since the player cant go below 1 in Y cords) and I can have a static value of 1 in cords as a "deadzone"
            modelYCordModifier = 0;
            if (levelDesign.GetLength(0) > 10)
            {
                modelYCordModifier = (levelDesign.GetLength(0) - 10);
            }

            for (int i = 0; i < levelDesign.GetLength(0); i++)
            {
                
                for (int j = 0; j < levelDesign.GetLength(1); j++)
                {
                    switch(levelDesign[i, j])
                    {
                        case '-':
                            //sets start values for a platform
                            if (!isBuildingPlatform)
                            {
                                isBuildingPlatform = true;
                                //due to a bugg when transforming float to int I keep a value that represents the startpoint in the grid to later use to get an integer of the number of platforms in the grid.  
                                //bugg: at some values ex. 0.7 when transforming to an int * 10 it would be rounded to 6 instead of 7 and at a case like this draw an extra platform that the player couldent
                                //land on. And at some values no platfroms would be drawn when there should be. These values were CONSISTENT therefore it's probably something I just don't know yet about
                                //transforming floats to intergers.
                                //This private field is a workaround that problem.
                                platformStart = j;

                                platformStartPoint = new Vector2(j / 10f, (i - modelYCordModifier) / 10f);
                            }
                            //end of a platform
                            if (j == levelDesign.GetLength(1) - 1 && levelDesign[i, j] == '-' || levelDesign[i, j + 1] != '-')
                            {
                                platformXEndPoint = (j + 1) / 10f;
                                levelPlatforms.Add(new Platform(platformStartPoint, platformXEndPoint, platformStart - j + 1));
                                isBuildingPlatform = false;
                                platformStart = 0;
                            }
                            break;


                        case '<':
                            enemies.Add(new Enemy(new Vector2(j / 10f, (i - modelYCordModifier) / 10f), Direction.Left));
                            break;

                        case '>':
                            enemies.Add(new Enemy(new Vector2(j / 10f, (i - modelYCordModifier) / 10f), Direction.Right));
                            break;



                        case '*':
                            levelExit = new LevelExit(new Vector2(j / 10f, (i - modelYCordModifier) / 10f));
                            break;

                        
                    }
                }
            }     
        }

        /// <summary>
        /// Properties for private fields
        /// </summary>
        public List<Platform> Levelplatforms
        {
            get { return levelPlatforms; }
        }
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        public Player Player
        {
            get { return player; }
        }
        public LevelExit LevelExit
        {
            get { return levelExit; }
        }
        public Vector2 LevelStartPosition
        {
            get { return playerStartPosition; }
        }
       
    }
}
