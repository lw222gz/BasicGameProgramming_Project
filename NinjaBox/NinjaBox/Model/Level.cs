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
        private List<SecurityCamera> levelCameras;
        private List<PowerBox> levelPowerBoxes;
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
            levelPlatforms = new List<Platform>(30);
            enemies = new List<Enemy>(10);
            //7 is the max amount of cameras for a level with the current build.
            levelCameras = new List<SecurityCamera>(7);
            levelPowerBoxes = new List<PowerBox>(7);
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

        public List<SecurityCamera> LevelCameras
        {
            get { return levelCameras; }
        }

        public List<PowerBox> LevelPowerBoxes
        {
            get { return levelPowerBoxes; }
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

            for (int y = 0; y < levelDesign.GetLength(0); y++)
            {
                
                for (int x = 0; x < levelDesign.GetLength(1); x++)
                {
                    switch(levelDesign[y, x])
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
                                platformStart = x;

                                platformStartPoint = new Vector2(x / 10f, (y - modelYCordModifier) / 10f);
                            }
                            //end of a platform
                            if (x == levelDesign.GetLength(1) - 1 && levelDesign[y, x] == '-' || levelDesign[y, x + 1] != '-')
                            {
                                platformXEndPoint = (x + 1) / 10f;
                                if (x + 1 < levelDesign.GetLength(1) && levelDesign[y, x + 1] == '/')
                                {
                                    //Adds a moving platform in X-led
                                    int counter = getPatrolXLength(levelDesign[y, x + 1], levelDesign, y, x);
                                    levelPlatforms.Add(new Platform(platformStartPoint, platformXEndPoint, x - platformStart + 1, (x + counter) / 10f, Direction.Horizontal));                                    
                                }
                                else if (y + 1 < levelDesign.GetLength(0) && levelDesign[y + 1, x] == '%')
                                {
                                    //Adds a moving platform in Y-led
                                    int counter = getPatrolYLength(levelDesign[y + 1, x], levelDesign, y, x);
                                    levelPlatforms.Add(new Platform(platformStartPoint, platformXEndPoint, x - platformStart + 1, (y - modelYCordModifier + counter) / 10f, Direction.Vertical));
                                }
                                else
                                {
                                    //Adds a static platform
                                    levelPlatforms.Add(new Platform(platformStartPoint, platformXEndPoint, x - platformStart + 1));
                                }
                                isBuildingPlatform = false;
                                platformStart = 0;
                            }
                            break;


                        case '<': 
                        case '>':
                            
                            Direction enemyDirection;
                            if (levelDesign[y, x] == '>')
                            {
                                enemyDirection = Direction.Right;
                            }
                            else
                            {
                                enemyDirection = Direction.Left;
                            }
                            //checks so the enemy isent on the end on the level grid, if so it cant have a patrol path
                            if (x + 1 < levelDesign.GetLength(1) && (levelDesign[y, x + 1] == '/' || levelDesign[y, x + 1] == '%'))
                            {                               
                                    char patrolType = levelDesign[y, x + 1];

                                    int counter = getPatrolXLength(patrolType, levelDesign, y, x);     
                                    //Adds a patroling enemy
                                    enemies.Add(new Enemy(new Vector2(x / 10f, (y - modelYCordModifier) / 10f), enemyDirection, (x + counter) / 10f, patrolType == '/'));
                            }                                                        
                            else
                            {
                                //Adds a non-moving enemy
                                enemies.Add(new Enemy(new Vector2(x / 10f, (y - modelYCordModifier) / 10f), enemyDirection));
                            }
                            
                            break;



                        case '*':
                            if (!levelHasExit)
                            {
                                levelHasExit = true;
                                levelExit = new LevelExit(new Vector2(x / 10f, (y - modelYCordModifier) / 10f));
                            }
                            else
                            {
                                throw new Exception("A level can only have 1 exit.");
                            }                            
                            break; 
  

                        case '"':
                        case '#':
                        case '¤':
                            //add detection camera
                            levelCameras.Add(new SecurityCamera(new Vector2(x / 10f, (y - modelYCordModifier) / 10f), getCameraLinkId(levelDesign[y, x])));
                            break;

                        case '@':
                        case '£':
                        case '$':
                            //add camera power box
                            levelPowerBoxes.Add(new PowerBox(new Vector2(x / 10f, (y - modelYCordModifier) / 10f), getCameraLinkId(levelDesign[y, x])));
                            
                            break;

                        case '+':
                            playerStartPosition = new Vector2(x/10f + player.Size.X /2, (y - modelYCordModifier)/10f + player.Size.Y/2);
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Reads the patrol length of a moving object
        /// and returns that Y-led length as an int (1 = 0.1 model cords)
        /// </summary>
        /// <param name="patrolType">type of patrol char that the loop will look for</param>
        /// <param name="levelDesign">the 2d level matrix</param>
        /// <param name="y">y counter for the multidimensional array</param>
        /// <param name="x">x counter for the multidimensional array</param>
        /// <returns>int representing length of the patrol</returns>
        /// TODO: Try to fuse this method with getPatrolXLength somehow?
        private int getPatrolYLength(char patrolType, char[,] levelDesign, int y, int x)
        {
            int counter = 1;
            while (levelDesign[y + counter, x] == patrolType)
            {
                counter += 1;

                //if the next char read is not within the array I need to stop the loop to avoid a crash
                if (y + 1 + counter > levelDesign.GetLength(0))
                {
                    break;
                }
            }

            return counter;
        }

        /// <summary>
        /// Reads the patrol length of a moving object
        /// and returns that X-led length as an int (1 = 0.1 model cords)
        /// </summary>
        /// <param name="patrolType">Char to see if it exists</param>
        /// <param name="levelDesign">the level 2d matrix</param>
        /// <param name="y">y counter for the multidimensional array</param>
        /// <param name="x">x counter for the multidimensional array</param>
        private int getPatrolXLength(char patrolType, char [,] levelDesign, int y, int x)
        {
            int counter = 1;
            while (levelDesign[y, x + counter] == patrolType)
            {
                counter += 1;

                //if the next char read is not within the array I need to stop the loop to avoid a crash
                if (x + 1 + counter > levelDesign.GetLength(1))
                {
                    break;
                }
            }
            return counter;
        }

        private int getCameraLinkId(char p)
        {
            //each case represents a shift-key value and a Alt Gr - key value for the number buttons
            switch (p)
            {
                case '"':
                case '@':
                    return 2;

                case '#':
                case '£':
                    return 3;

                case '¤':
                case '$':
                    return 4;

                default:
                    //if a bugg occurs when a camera or powerbox has connection ID 0 the bugg lies here.
                    return 0;
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
                //level messages for the tutorial
                case 0:
                    levelMessages = new List<Message>(11);

                    levelMessages.Add(new Message("Move around with the left and right arrow keys.", new Vector2(.1f, .65f)));
                    levelMessages.Add(new Message("And jump by holding down space bar.", new Vector2(.2f, .7f)));

                    levelMessages.Add(new Message("Beware of these guys, enter their detection area and you lose.", new Vector2(.6f, .1f)));

                    levelMessages.Add(new Message("LEAP OF FAITH!", new Vector2(1.2f, -.3f)));

                    levelMessages.Add(new Message("You can attack using the 'V' key, ", new Vector2(2.6f, .1f)));
                    levelMessages.Add(new Message("you will allways attack in the direction you last moved.", new Vector2(2.6f, .15f)));

                    levelMessages.Add(new Message("Be careful not too get too close to their backs.", new Vector2(2f, -.25f)));
                    levelMessages.Add(new Message("Getting too close will cause them to sense you", new Vector2(2.0f, -.2f)));
                    levelMessages.Add(new Message("and turn around.", new Vector2(2, -.15f)));

                    levelMessages.Add(new Message("Tutorial complete! Remember that you can allways ", new Vector2(3.2f, -.3f)));
                    levelMessages.Add(new Message("pause the game by pressing 'P'", new Vector2(3.2f, -.25f)));
                    break;

               
                    //level message for level 2
                case 2:
                    levelMessages = new List<Message>(5);

                    levelMessages.Add(new Message("This security camera is unpassabled", new Vector2(3f, -.55f)));
                    levelMessages.Add(new Message("and undestructable, you need to find", new Vector2(3f, -.5f)));
                    levelMessages.Add(new Message("it's power source and destroy it to be", new Vector2(3f, -.45f)));
                    levelMessages.Add(new Message("able to pass.", new Vector2(3f, -.4f)));
                    break;
            }
        }

        


        public void RemoveEnemy(Enemy e)
        {
            enemies.Remove(e);
        }

        /// <summary>
        /// is called when a power box is destroyed
        /// </summary>
        /// <param name="powerBox">powerbox that is destroyed</param>
        public void DestroyPowerBox(PowerBox powerBox)
        {
            //updates the cameras that might have been disabled
            levelCameras = powerBox.DestroyPowerBox(levelCameras);            
        }
    }
}
