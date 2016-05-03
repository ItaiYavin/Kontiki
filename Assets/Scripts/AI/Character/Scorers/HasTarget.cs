using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class HasTarget : ContextualScorerBase {

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
        	bool b = false;
			AIContext ai = (AIContext) context;
        	Pathfinder pathfinder = ((AIContext)context).pathfinder;
        	b = pathfinder.target != null;

        	if(not)
        		b = !b;

            if(ai.debugAI)
                Debug.Log("HasTarget: " + b);

        	return b ? 1f * score : 0f * score;
        }
	}
}