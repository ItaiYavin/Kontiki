using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsBoatMoving : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;

        public override float Score(IAIContext context){
			AIContext ai = (AIContext)context;
        	NavMeshAgent agent = ai.job.boat.agent;
        	bool b = agent.remainingDistance > 0;

    		if(ai.debugAI){ 
				Debug.Log("Boat is Moving: " + b  + ", distance to target = " + agent.remainingDistance);
			}
			if(not) b = !b;

        	return b ? score : 0f;
        }
	}
}
