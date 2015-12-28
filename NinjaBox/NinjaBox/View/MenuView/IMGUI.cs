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
    
    class IMGUI : MainView
    {
        private Button button;
        private List<Button> activeButtons;
        private Button RestartButton;
        private Button PlayGameButton;
        private Button PlayTutorialButton;
        private Button MainMenuButton;
        private Button ResumeButton;

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
            //restart menu
            RestartButton = new Button(new Vector2(0.2f, 0.2f),
                                        content.Load<Texture2D>("ButtonImages/RestartButtonNormal.png"),
                                        content.Load<Texture2D>("ButtonImages/RestartButtonHover.png"));

            //main menu
            PlayGameButton = new Button(new Vector2(0.8f, 0.2f),
                                        content.Load<Texture2D>("ButtonImages/PlayGameNormalButton.png"),
                                        content.Load<Texture2D>("ButtonImages/PlayGameHoverButton.png"));

            PlayTutorialButton = new Button(new Vector2(0.8f, 0.5f),
                                            content.Load<Texture2D>("ButtonImages/PlayTutorialNormalButton.png"),
                                            content.Load<Texture2D>("ButtonImages/PlayTutorialHoverButton.png"));

            //pause menu
            MainMenuButton = new Button(new Vector2(0.8f, 0.2f),
                                    content.Load<Texture2D>("ButtonImages/MainMenuButtonNormal.png"),
                                    content.Load<Texture2D>("ButtonImages/MainMenuButtonHover.png"));

            ResumeButton = new Button(new Vector2(0.8f, 0.5f),
                                    content.Load<Texture2D>("ButtonImages/ResumeButtonNormal.png"),
                                    content.Load<Texture2D>("ButtonImages/ResumeButtonHover.png"));

        }

        public bool doButton(ButtonType buttonType)
        {
            MouseState mouseState = Mouse.GetState();

            button = getButtonValue(buttonType);

            Vector2 buttonVisualCords = camera.getVisualCords(button.Position + camera.CameraOffSet);

            button.IsMouseOver = false;
            button.IsButtonClicked = false;
            if (mouseState.X >= buttonVisualCords.X - button.ButtonWidth / 2 &&
                mouseState.X <= buttonVisualCords.X + button.ButtonWidth / 2 &&
                mouseState.Y >= buttonVisualCords.Y - button.ButtonHeigth / 2 &&
                mouseState.Y <= buttonVisualCords.Y + button.ButtonHeigth / 2)
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
                //resets the old mouse state if the mouse isent over the button
                button.OldMouseState = ButtonState.Released;
            }


            setButtonValue(buttonType);


            activeButtons.Add(button);

            return button.IsButtonClicked && button.IsMouseOver;         
        }

        /// <summary>
        /// Gets the value for a current button type
        /// </summary>
        /// <param name="buttonType">buttontype that is requested to get</param>
        /// <returns>requested button type</returns>
        private Button getButtonValue(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Restart:
                    return RestartButton;
                   
                case ButtonType.Play:
                    return PlayGameButton;

                case ButtonType.PlayTutorial:
                    return PlayTutorialButton;

                case ButtonType.Resume:
                    return ResumeButton;

                case ButtonType.MainMenu:
                    return MainMenuButton;

                default: 
                    return button;
            }
        }

        /// <summary>
        /// overwrites old values for a buttontype
        /// </summary>
        /// <param name="buttonType">buttontype that is gonna be overwritten</param>
        private void setButtonValue(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Restart:
                    RestartButton = button;
                    break;

                case ButtonType.Play:
                    PlayGameButton = button;
                    break;

                case ButtonType.PlayTutorial:
                    PlayTutorialButton = button;
                    break;


                case ButtonType.Resume:
                    ResumeButton = button;
                    break;


                case ButtonType.MainMenu:
                    MainMenuButton = button;
                    break;
            }
        }

        /// <summary>
        /// Draws all active menu buttons
        /// </summary>
        public void DrawMenu()
        {
            //TODO: Change so the position is by default x: 700 and y : 200 and then any button after that gets +300 in Y led
            foreach (Button b in activeButtons)
            {
                spriteBatch.Draw(b.ActiveTexture,
                                camera.getVisualCords(b.Position + camera.CameraOffSet),
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
