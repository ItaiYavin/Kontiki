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
        	NavMeshAgent agent = ai.pathfinder.agent;
        	bool b = false;
			
			if(ai.job is Fisher || ai.job is Trader){
					b = false;
				
			}else if(agent.remainingDistance > 0){
					b = true;
				}
			
			if(ai.debugAI_Character)
				Debug.Log("Is Moving: " + b  + ", distance to target = " + agent.remainingDistance);
			
			if(not) b = !b;
			

            if(ai.debugAI_Character)
                Debug.Log("Is Moving: " + b + " score: " + score);
                
        	return b ? score : 0f;
        }
	}
}
