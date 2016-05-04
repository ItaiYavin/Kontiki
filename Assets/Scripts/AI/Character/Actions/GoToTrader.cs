using System.Diagnostics;
using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI
{


    /// <summary>
    /// Goes to the specified destination
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class GoToTrader : ActionBase
    {
        public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            if(ai.baseroutine.trader != null)
                ai.pathfinder.GoTo(ai.baseroutine.trader.transform);
        }
    }
}