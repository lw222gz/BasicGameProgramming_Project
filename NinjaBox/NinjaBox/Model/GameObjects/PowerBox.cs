using Microsoft.Xna.Framework;
using NinjaBox.Model.GameObjects.ObjectDefinedInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects
{
    class PowerBox : IDestroyable
    {
        private Vector2 position;
        private int connectionID;
        private bool isActive;

        public PowerBox(Vector2 position, int connectionID)
        {
            this.position = position;
            this.position.Y -= 0.1f;
            this.position += Size / 2;
            this.connectionID = connectionID;
            isActive = true;
        }

        public Vector2 Position
        {
            get { return position; }
        }
        public Vector2 Size
        {
            get { return new Vector2(0.07f, 0.2f); }
        }
        public int ConnectionID
        {
            get { return connectionID; }
        }
        public bool IsActive
        {
            get { return isActive; }
        }

        /// <summary>
        /// Disables the power box and all the cameras linked to it.
        /// </summary>
        public List<SecurityCamera> DestroyPowerBox(List<SecurityCamera> securityCameras)
        {
            isActive = false;
            for (int i = 0; i < securityCameras.Count; i++)
            {
                if (ConnectionID == securityCameras[i].ConnectionID)
                {
                    securityCameras[i].DisableCamera();
                }
            }
            return securityCameras;
        }
    }
}
