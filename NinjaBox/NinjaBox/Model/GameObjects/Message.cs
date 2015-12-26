using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class Message
    {
        private string message;
        private Vector2 position;

        public Message(string message, Vector2 position)
        {
            this.message = message;
            this.position = position;
        }

        /// <summary>
        /// Properties for private fields
        /// </summary>
        public string getMessage
        {
            get { return message; }
        }

        public Vector2 Position
        {
            get { return position; }
        }
    }
}
