using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for the euclidean distance from character to boat. distance multiplied with the score
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class CanInteractWithBoat : ContextualScorerBase {

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            
            JobWithBoat job = (JobWithBoat) ai.job;
            float distance = Vector3.Distance(ai.transform.position, job.boat.transform.position);
            bool b = distance < Settings.pickupRange;
            
            if(not)b = !b;
            return b ? score : 0f;
        }
	}
}
