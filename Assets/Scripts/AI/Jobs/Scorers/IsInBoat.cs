using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character is in the boat
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsInBoat : ContextualScorerBase {
     

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            
            JobWithBoat job = (JobWithBoat) ai.job;
            bool b = job.boat.characterInBoat == ai.character; 
            
            if(ai.debugAI_Job)
                Debug.Log("Is " + (b ? "": "not") + " in boat");
            
            if(not)b = !b;
            
            return b ? score : 0f;
        }
	}
}
