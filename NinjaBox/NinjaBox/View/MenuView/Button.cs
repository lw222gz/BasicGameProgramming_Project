using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.MenuView
{
    class Button
    {
        private Vector2 position;
        private Texture2D buttonImage;
        private Texture2D hoverButtonImage;
        private float buttonWidth;
        private float buttonHeight;
        private bool isMouseOver;
        //button with no hover effect
        public Button(Vector2 position, Texture2D buttonImage) : this(position, buttonImage, buttonImage)
        { }

        //button with hover effect
        public Button(Vector2 position, Texture2D buttonImage, Texture2D hoverButtonImage)
        {
            this.position = position;
            this.buttonImage = buttonImage;   
            this.hoverButtonImage = hoverButtonImage;
            this.buttonWidth = buttonImage.Bounds.Width;
            this.buttonHeight = buttonImage.Bounds.Height;
            this.isMouseOver = false;
            ActiveTexture = buttonImage;
        }

        public Vector2 Position
        {
            get { return position; }
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
            get { return buttonWidth; }
        }
        public float ButtonHeigth
        {
            get { return buttonHeight; }
        }

        public bool IsMouseOver
        {
            get { return isMouseOver; }
            set { isMouseOver = value; }
        }

        public Texture2D ActiveTexture
        {
            get;
            set;
        }
    }
}
