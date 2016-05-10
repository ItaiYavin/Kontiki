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
            float minSqrDist = (ai.transform.position - Settings.plazas[0].position).sqrMagnitude;
            int index = 0;
            for (int i = 1; i < Settings.plazas.Count; i++)
            {
                float d = (ai.transform.position - Settings.plazas[i].position).sqrMagnitude;
                if(minSqrDist > d){
                    minSqrDist = d;
                    index = i;
                }
            }
            ai.baseRoutine.selectedPlaza = Settings.plazas[index];
            ai.pathfinder.GoTo(ai.baseRoutine.selectedPlaza);
        }
	}
}
