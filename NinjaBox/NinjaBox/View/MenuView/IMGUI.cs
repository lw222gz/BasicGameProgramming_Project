using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NinjaBox.View.MenuView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View
{
    public enum ButtonType
    {
        Restart,
        Play,
        Resume,
        None
    }
    class IMGUI : MainView
    {
        private Button button;
        private List<Button> activeButtons;
        private Button RestartButton;

        //private ButtonType currentMenu;
        //private GameMenu gameMenu;

        public IMGUI()
        {
            activeButtons = new List<Button>(3);
            //gameMenu = new GameMenu(content);
            //currentMenu = ButtonType.None;   
            
        }

        public List<Button> ActiveButtons
        {
            get { return activeButtons; }
        }

        /// <summary>
        /// Loads all button objects
        /// </summary>
        public void LoadButtons()
        {
            RestartButton = new Button(new Vector2(700, 200),
                                        content.Load<Texture2D>("RestartButtonNormal.png"),
                                        content.Load<Texture2D>("RestartButtonHover.png"));
        }

        public bool doButton(ButtonType buttonType)
        {
            MouseState mouseState = Mouse.GetState();

            button = getButtonValue(buttonType);

            button.IsMouseOver = false;
            button.IsButtonClicked = false;
            if (mouseState.X >= button.Position.X - button.ButtonWidth / 2 &&
                mouseState.X <= button.Position.X + button.ButtonWidth / 2 &&
                mouseState.Y >= button.Position.Y - button.ButtonHeigth / 2 &&
                mouseState.Y <= button.Position.Y + button.ButtonHeigth / 2)
            {
                button.ActiveTexture = button.HoverButtonImage;
                button.IsMouseOver = true;

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    button.OldMouseState = ButtonState.Pressed;
                }
                if (mouseState.LeftButton == ButtonState.Released && button.OldMouseState == ButtonState.Pressed)
                {
                    button.IsButtonClicked = true;
                    button.OldMouseState = ButtonState.Released;
                }
            }
            else
            {
                button.ActiveTexture = button.ButtonImage;
            }


            setButtonValue(buttonType);


            activeButtons.Add(button);
            if (button.IsButtonClicked && button.IsMouseOver)
            {
                int a = 5;
            }

            return button.IsButtonClicked && button.IsMouseOver;         
        }

        private Button getButtonValue(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Restart:
                    return RestartButton;
                   
 
                default: 
                    return button;
            }
        }

        private void setButtonValue(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Restart:
                    RestartButton = button;
                    break;
            }
        }

        /// <summary>
        /// Draws all active menu buttons
        /// </summary>
        public void DrawMenu()
        {
            foreach (Button b in activeButtons)
            {
                spriteBatch.Draw(b.ActiveTexture,
                            b.Position,
                            null,
                            Color.White,
                            0f,
                            new Vector2(b.ActiveTexture.Bounds.Width / 2, b.ActiveTexture.Bounds.Height / 2),
                            1f,
                            SpriteEffects.None,
                            0);
            }

            //clears the list as it will be refilled with buttons in the next update
            activeButtons.Clear();            
        }
    }
}
