using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsFishing : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;

        public override float Score(IAIContext context){
			AIContext ai = (AIContext)context;
            if(ai.job is Fisher){
                bool b = ((Fisher)ai.job).isFishing;
                
                if(ai.debugAI_Job)
                    Debug.Log(ai.self.name + "is " + (b ? "": "not") + " fishing");
            
                if(not) b = !b;

                return b ? score : 0f;
            }
        	return 0f;
        }
	}
}
