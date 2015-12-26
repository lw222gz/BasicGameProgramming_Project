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
        private List<Message> levelMessages;
        private Player player;
        private LevelExit levelExit;
        private Vector2 playerStartPosition;
        private int LevelDeadZone;

        private int platformStart;
        private int modelYCordModifier;
        private bool isBuildingPlatform;
        private Vector2 platformStartPoint;
        private float platformXEndPoint;

        private bool levelHasExit;

        public Level(char[,] levelDesign, Player player, int levelIdentifier)
        {
            this.player = player;
            playerStartPosition = new Vector2(0.1f, 0.85f);
            levelPlatforms = new List<Platform>(10);
            enemies = new List<Enemy>(10);
            LevelTranslator(levelDesign);
            

            SetLevelMessages(levelIdentifier);
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

        public List<Message> LevelMessages
        {
            get { return levelMessages; }
        }
        //End of properties

        /// <summary>
        /// Translates a multidimensional char array to level objects
        /// </summary>
        /// <param name="levelDesign">char[,] that is being translated</param>
        private void LevelTranslator(char[,] levelDesign)
        {
            isBuildingPlatform = false;
            levelHasExit = false;

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
                                levelPlatforms.Add(new Platform(platformStartPoint, platformXEndPoint, j - platformStart + 1)); //platformStart - j + 1
                                isBuildingPlatform = false;
                                platformStart = 0;
                            }
                            break;


                        case '<': 
                        case '>':
                            
                            Direction enemyDirection;
                            if (levelDesign[i, j] == '>')
                            {
                                enemyDirection = Direction.Right;
                            }
                            else
                            {
                                enemyDirection = Direction.Left;
                            }
                            if (levelDesign[i, j + 1] == '/' || levelDesign[i, j + 1] == '%')
                            {
                                char patrolType = levelDesign[i, j + 1];
                                int counter = 1;
                                while (levelDesign[i, j + counter] == patrolType)
                                {
                                    counter += 1;
                                }
                                
                                enemies.Add(new Enemy(new Vector2(j / 10f, (i - modelYCordModifier) / 10f), enemyDirection, (j + counter) / 10f, patrolType == '/'));
                            }
                            else
                            {
                                enemies.Add(new Enemy(new Vector2(j / 10f, (i - modelYCordModifier) / 10f), enemyDirection));
                            }
                            
                            break;



                        case '*':
                            if (!levelHasExit)
                            {
                                levelHasExit = true;
                                levelExit = new LevelExit(new Vector2(j / 10f, (i - modelYCordModifier) / 10f));
                            }
                            else
                            {
                                throw new Exception("A level can only have 1 exit.");
                            }                            
                            break;   
                    

                        case '+':
                            playerStartPosition = new Vector2(j/10f + player.Size.X /2, (i - modelYCordModifier)/10f + player.Size.Y/2);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levelIdentifier"></param>
        private void SetLevelMessages(int levelIdentifier)
        {
            //NOTE: each case has to initiate the levelMessages List, I dont want empty lists on each level just hanging around.
            switch (levelIdentifier)
            {
                case 0:
                    levelMessages = new List<Message>(5);

                    levelMessages.Add(new Message("Move around with the left and right arrow keys.", new Vector2(0.1f, 0.65f)));
                    levelMessages.Add(new Message("And jump with space bar.", new Vector2(0.2f, 0.75f)));
                    levelMessages.Add(new Message("Beware of these guys, enter their detection area and you lose.", new Vector2(0.6f, 0.1f)));
                    levelMessages.Add(new Message("LEAP OF FAITH!", new Vector2(1.2f, -0.3f)));
                    levelMessages.Add(new Message("You can attack using the 'V' key, ", new Vector2(2.6f, 0.1f)));
                    levelMessages.Add(new Message("you will allways attack in the direction you last faced", new Vector2(2.6f, 0.15f)));
                    levelMessages.Add(new Message("Tutorial complete, there will be more to learn in the real game.", new Vector2(2.8f, -0.3f)));
                    break;
            }
        }

        


        public void RemoveEnemy(Enemy e)
        {
            enemies.Remove(e);
        }
    }
}
