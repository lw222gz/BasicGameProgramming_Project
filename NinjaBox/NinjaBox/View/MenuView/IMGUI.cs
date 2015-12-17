using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NinjaBox.View.MenuView;
using NinjaBox.View.MenuViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    public enum MenuType
    {
        Restart,
        Main,
        Pause,
        None
    }
    class IMGUI
    {
        private MenuType currentMenu;
        private GameMenu gameMenu;

        public IMGUI(ContentManager content)
        {
            gameMenu = new GameMenu(content);
            currentMenu = MenuType.None;   
        }

        public MenuType ActiveMenu
        {
            get { return currentMenu; }
        }


        public bool doMenu(MenuType menuType)
        {
            MouseState mouseState = Mouse.GetState();

            currentMenu = menuType;

            gameMenu.setCurrentMenu(menuType);

            foreach (Button b in gameMenu.CurrentMenu)
            {
                if(mouseState.X >= b.Position.X - b.ButtonWidth/2 &&
                    mouseState.X <= b.Position.X + b.ButtonWidth/2 &&
                    mouseState.Y >= b.Position.Y - b.ButtonHeigth/2 && 
                    mouseState.Y <= b.Position.Y + b.ButtonHeigth/2)
                {
                    b.IsMouseOver = true;
                }
                else
                {
                    b.IsMouseOver = false;
                }
            }

            return false;
        }

        public void DrawMenu()
        {
            gameMenu.DrawMenu();
        }
    }
}
