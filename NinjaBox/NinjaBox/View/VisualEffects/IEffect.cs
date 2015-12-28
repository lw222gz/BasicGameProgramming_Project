using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NinjaBox.View.VisualEffects
{
    interface IEffect
    {
        //Task: draw the visual effect
        void RunEffect(float timeElapsed);

        //Task: returns boolean that represents if the visual effect is over.
        bool IsEffectOver{ get; }
    }
}
