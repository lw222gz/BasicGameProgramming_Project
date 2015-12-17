using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NinjaBox.View.MenuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.MenuViews
{
    class GameMenu : MainView
    {
        private Texture2D menuBackgroundTexture;

        private Texture2D restartButtonTexture;
        private Texture2D restartButtonHoverTexture;

        private List<Button> currentMenu;

        private List<Button> restartMenu;
      
        public GameMenu(ContentManager content)
        {
            currentMenu = new List<Button>(5);
            restartMenu = new List<Button>(5);

            menuBackgroundTexture = content.Load<Texture2D>("MenuBackground.png");

            restartButtonTexture = content.Load<Texture2D>("RestartButtonNormal.png");
            restartButtonHoverTexture = content.Load<Texture2D>("RestartButtonHover.png");


            setMenuButtons();
        }

        public List<Button> CurrentMenu
        {
            get { return currentMenu; }
        }

        public void DrawMenu()
        {
            spriteBatch.Draw(menuBackgroundTexture, new Vector2(0, 0), new Color(0.7f, 0.7f, 0.7f, 0.7f));


            foreach (Button b in currentMenu)
            {
                if(b.IsMouseOver){
                    b.ActiveTexture = b.HoverButtonImage;
                }
                else {
                    b.ActiveTexture = b.ButtonImage;
                }

                spriteBatch.Draw(b.ActiveTexture, 
                                b.Position,
                                null,
                                Color.White,
                                0f,
                                new Vector2(b.ActiveTexture.Bounds.Width/2, b.ActiveTexture.Bounds.Height/2),
                                1f,
                                SpriteEffects.None,
                                0);
                
            }
        }
        public void setCurrentMenu(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Restart:
                    currentMenu = restartMenu;
                    break;
            }
        }

        private void setMenuButtons()
        {
            restartMenu.Add(new Button(new Vector2(700, 200), restartButtonTexture, restartButtonHoverTexture));
            //TODO: set buttons for the rest of the menus
        }
    }
}
