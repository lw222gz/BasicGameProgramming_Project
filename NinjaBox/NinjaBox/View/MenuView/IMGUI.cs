using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private Button CreditsButton;
        private Button LevelSelectButton;

        private Texture2D NumberButtonNormalTexture;
        private Texture2D NumberButtonHoverTexture;

        private ButtonState oldButtonState;

        private SoundEffect buttonClickSound;

        //private ButtonType currentMenu;
        //private GameMenu gameMenu;

        public IMGUI()
        {
            oldButtonState = ButtonState.Released;
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
        public void LoadResources()
        {          
            RestartButton = new Button(content.Load<Texture2D>("ButtonImages/RestartButtonNormal.png"),
                                        content.Load<Texture2D>("ButtonImages/RestartButtonHover.png"));

            PlayGameButton = new Button(content.Load<Texture2D>("ButtonImages/PlayGameNormalButton.png"),
                                        content.Load<Texture2D>("ButtonImages/PlayGameHoverButton.png"));

            PlayTutorialButton = new Button(content.Load<Texture2D>("ButtonImages/PlayTutorialNormalButton.png"),
                                            content.Load<Texture2D>("ButtonImages/PlayTutorialHoverButton.png"));

            CreditsButton = new Button(content.Load<Texture2D>("ButtonImages/CreditButtonNormal"),
                                        content.Load<Texture2D>("ButtonImages/CreditButtonHover"));

            LevelSelectButton = new Button(content.Load<Texture2D>("ButtonImages/LevelSelectButtonNormal.png"),
                                            content.Load<Texture2D>("ButtonImages/LevelSelectButtonHover.png"));

            NumberButtonNormalTexture = content.Load<Texture2D>("ButtonImages/NumberButtonNormal.png");
            NumberButtonHoverTexture = content.Load<Texture2D>("ButtonImages/NumberButtonHover.png");


            MainMenuButton = new Button(content.Load<Texture2D>("ButtonImages/MainMenuButtonNormal.png"),
                                        content.Load<Texture2D>("ButtonImages/MainMenuButtonHover.png"));

            ResumeButton = new Button(content.Load<Texture2D>("ButtonImages/ResumeButtonNormal.png"),
                                        content.Load<Texture2D>("ButtonImages/ResumeButtonHover.png"));

            


            buttonClickSound = content.Load<SoundEffect>("MenuClick");


        }

        public bool doButton(ButtonType buttonType, Vector2 position, string mess)
        {
            MouseState mouseState = Mouse.GetState();

            if (buttonType != ButtonType.NumberButton)
            {
                button = getButtonValue(buttonType);
            }
            else
            {
                //number buttons are repetetive so I need to be abel to create multiple objects
                button = new Button(NumberButtonNormalTexture, NumberButtonHoverTexture, mess.ToString());
            }

            button.Position = camera.getVisualCords(position + camera.CameraOffSet);

            button.IsMouseOver = false;
            button.IsButtonClicked = false;
            if (mouseState.X >= button.Position.X - button.ButtonWidth / 2 &&
                mouseState.X <= button.Position.X + button.ButtonWidth / 2 &&
                mouseState.Y >= button.Position.Y - button.ButtonHeigth / 2 &&
                mouseState.Y <= button.Position.Y + button.ButtonHeigth / 2)
            {
                button.ActiveTexture = button.HoverButtonImage;
                button.IsMouseOver = true;

                if (mouseState.LeftButton == ButtonState.Pressed && oldButtonState == ButtonState.Released)
                {
                    button.IsButtonClicked = true;
                    oldButtonState = ButtonState.Pressed;
                    //Button click sound effect
                    buttonClickSound.Play();
                }
                if (mouseState.LeftButton == ButtonState.Released && oldButtonState == ButtonState.Pressed)
                {
                    oldButtonState = ButtonState.Released;
                }                
            }
            else
            {
                button.ActiveTexture = button.ButtonImage;
                //resets the old mouse state if the mouse isent over the button
                //oldButtonState = ButtonState.Released;
            }

            if (buttonType != ButtonType.NumberButton)
            {
                setButtonValue(buttonType);
            }
            

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

                case ButtonType.Credits:
                    return CreditsButton;

                case ButtonType.LevelSelect:
                    return LevelSelectButton;

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


                case ButtonType.Credits:
                    CreditsButton = button;
                    break;

                case ButtonType.LevelSelect:
                    LevelSelectButton = button;
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
                if (b.Message != null)
                {
                    messageView.DrawQuickMessage(b.Message, b.Position);
                }
            }

            //clears the list as it will be refilled with buttons in the next update
            activeButtons.Clear();            
        }
    }
}
