using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class HasSelectedFishingSpot : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;

        public override float Score(IAIContext context){
			AIContext ai = (AIContext)context;
            if(ai.job is Fisher){
                bool b = ((Fisher)ai.job).selectedFishingSpot != null;
                
                if(ai.debugAI_Job)
                    Debug.Log(ai.self.name + " has " + (b ? "": "not") + " selected fishing spot");
            
                if(not) b = !b;

                return b ? score : 0f;
            }
        	return 0f;
        }
	}
}
