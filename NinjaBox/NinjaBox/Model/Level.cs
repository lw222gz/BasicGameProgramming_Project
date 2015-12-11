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
        private List<IGameObject> levelPlatforms;
        private Player player;

        public Level(int level, Player player)
        {
            this.player = player;
            levelPlatforms = new List<IGameObject>(10);
            GenerateLevel(level);
        }

        public List<IGameObject> Levelplatforms
        {
            get { return levelPlatforms; }
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
                    levelPlatforms.Add(new Platform(new Vector2(0f, 0.95f), new Vector2(2f, 0.95f)));
                    levelPlatforms.Add(new Platform(new Vector2(1f, 0.5f), new Vector2(2f, 0.5f)));

                    
                    break;
            }
        }
    }
}
