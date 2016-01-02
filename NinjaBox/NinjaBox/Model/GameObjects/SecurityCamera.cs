using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class SecurityCamera
    {
        private Vector2 position;
        
        private int connectionID;
        private bool isTurnedOn;
        
        public SecurityCamera(Vector2 position, int connectionID)
        {
            this.position = position;
            this.position.Y += CameraSize.Y / 2;
            this.connectionID = connectionID;
            this.isTurnedOn = true;
        }

        public Vector2 Position
        {
            get { return position; }
        }
        public int ConnectionID
        {
            get { return connectionID; }
        }
        public bool IsTurnedOn
        {
            get { return isTurnedOn; }
        }
        public Vector2 CameraSize
        {
            get { return new Vector2(0.1f, 0.1f); }
        }
        public Vector2 DetectionAreaSize
        {
            get { return new Vector2(0.22f, 0.4f); }
        }
        public Vector2 DetectionAreaPosition
        {
            get { return new Vector2(position.X - CameraSize.X/2, position.Y + CameraSize.Y / 2); }
        }

        /// <summary>
        /// disables the cameras detection area.
        /// </summary>
        public void DisableCamera()
        {
            isTurnedOn = false;
        }
    }
}
