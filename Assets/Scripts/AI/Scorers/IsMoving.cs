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
		[ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;

        public override float Score(IAIContext context){
        	Character character = ((CharacterAIContext)context).character;
        	NavMeshAgent agent = character.agent;
        	bool b = false;

        	if(agent.remainingDistance > 0){
        		if(debug) Debug.Log(agent.remainingDistance);
        		b = true;
        	}

    		if(debug) Debug.Log("Is Moving: " + b);
        	if(not) b = !b;

        	return b ? 1f * score : 0f * score;
        }
	}
}
