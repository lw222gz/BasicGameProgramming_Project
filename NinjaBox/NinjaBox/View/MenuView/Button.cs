using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.MenuView
{
    class Button
    {
        private Texture2D buttonImage;
        private Texture2D hoverButtonImage;
        private string message;
        private float buttonWidth;
        private float buttonHeight;

        //button with no hover effect
        public Button(Texture2D buttonImage) : this(buttonImage, buttonImage, null)
        { }
        //button with hover effect
        public Button(Texture2D buttonImage, Texture2D hoverButtonImage) : this(buttonImage, hoverButtonImage, null) 
        { }

        //button with hover effect
        public Button(Texture2D buttonImage, Texture2D hoverButtonImage, string message)
        {
            this.message = message;
            this.buttonImage = buttonImage;   
            this.hoverButtonImage = hoverButtonImage;
            //OldMouseState = ButtonState.Released;
            ActiveTexture = buttonImage;
            IsMouseOver = false;
            IsButtonClicked = false;
        }

        
        public string Message
        {
            get { return message; }
        }
        public Texture2D ButtonImage
        {
            get { return buttonImage; }
        }
        public Texture2D HoverButtonImage
        {
            get { return hoverButtonImage; }
        }
        public float ButtonWidth
        {
            get { return ActiveTexture.Bounds.Width; }
        }
        public float ButtonHeigth
        {
            get { return ActiveTexture.Bounds.Height; }
        }
        public Vector2 Position
        {
            get;
            set;
        }

        public Texture2D ActiveTexture
        {
            get;
            set;
        }

        public ButtonState OldMouseState
        {
            get;
            set;
        }
        public bool IsMouseOver
        {
            get;
            set;
        }

        public bool IsButtonClicked
        {
            get;
            set;
        }
    }
}
