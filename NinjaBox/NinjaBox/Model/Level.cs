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

        public Level(int level)
        {
            levelPlatforms = new List<Platform>(10);
            GenerateLevel(level);
        }

        public List<Platform> LevelPlatforms
        {
            get { return levelPlatforms; }
        }

        private void GenerateLevel(int level)
        {
            switch (level)
            {
                case 1:
                    levelPlatforms.Add(new Platform(new Vector2(0, 0.95f), new Vector2(10, 0.5f)));
                    break;
            }
        }
    }
}
