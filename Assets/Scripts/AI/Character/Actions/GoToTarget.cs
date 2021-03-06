﻿using System;
using Apex.AI;
using Kontiki;

namespace Kontiki.AI
{

    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class GoToTarget : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            ((AIContext)context).pathfinder.GoToTarget();
        }
    }
}
