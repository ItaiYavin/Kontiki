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
	public class CanStartFishing : ContextualScorerBase {

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            
            if(ai.job.type == Job.Type.Fisher && ai.job.selectedFishingSpot != null){
                float distance = Vector3.Distance(ai.job.boat.transform.position, ai.job.selectedFishingSpot.position);
                Debug.Log("distance : " + distance);    
                bool b = distance < ai.job.initFishingRange;
            
                if(not)b = !b;
                return b ? score : 0f;
                
            }
            return 0f;
        }
	}
}
