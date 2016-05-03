using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether the boat is docked
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsBoatDocked : ContextualScorerBase {
     

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            
            bool b = ai.job.boat.isDocked;
            
            if(not) b = !b;
            
            return b ? score : 0f;
        }
	}
}
