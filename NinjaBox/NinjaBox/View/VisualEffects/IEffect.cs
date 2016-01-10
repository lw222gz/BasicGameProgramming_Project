using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.VisualEffects
{
    /// <summary>
    /// interface for visual effects. Contents are required for the visual effect loop
    /// </summary>
    interface IEffect
    {
        //Task: draw the visual effect
        void RunEffect(float timeElapsed);

        //Task: returns boolean that represents if the visual effect is over.
        bool IsEffectOver{ get; }
    }
}
