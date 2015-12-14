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

        public Level(int level, Player player)
        {
            this.player = player;
            levelPlatforms = new List<Platform>(10);
            enemies = new List<Enemy>(10);
            GenerateLevel(level);
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
        

        private void GenerateLevel(int level)
        {
            switch (level)
            {
                case 1:
                    levelPlatforms.Add(new Platform(new Vector2(0f, 0.95f), 4)); // 0 - 2 
                    levelPlatforms.Add(new Platform(new Vector2(0.7f, 0.5f), 2)); // 0.7 - 1.7
                    levelPlatforms.Add(new Platform(new Vector2(0.3f, 0.05f), 3)); // 0.3 - 1.8


                    enemies.Add(new Enemy(new Vector2(1.25f, levelPlatforms[1].StartPosition.Y), Direction.Left));
                    
                    break;
            }
        }
    }
}
