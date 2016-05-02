using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsMoving : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;

        public override float Score(IAIContext context){
			AIContext ai = (AIContext)context;
        	NavMeshAgent agent = ((AIContext)context).pathfinder.agent;
        	bool b = false;

        	if(agent.remainingDistance > 0){
        		if(ai.debugAI) Debug.Log(agent.remainingDistance);
        		b = true;
        	}

    		if(ai.debugAI) Debug.Log("Is Moving: " + b);
        	if(not) b = !b;

        	return b ? 1f * score : 0f * score;
        }
	}
}
