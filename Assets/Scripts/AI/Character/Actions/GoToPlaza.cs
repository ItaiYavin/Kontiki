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

	public class GoToPlaza : ActionBase 
	{
        public override void Execute(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            ai.pathfinder.GoTo(ai.baseRoutine.plaza.transform);
        }
	}
}
