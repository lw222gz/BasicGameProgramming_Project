using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.Model.GameObjects.ObjectDefinedInterfaces
{
    /// <summary>
    /// Interface that is inherited by all destroyable game objects
    /// </summary>
    interface IisDestroyable
    {
        Vector2 Position { get; }
        Vector2 Size { get; }
    }
}
