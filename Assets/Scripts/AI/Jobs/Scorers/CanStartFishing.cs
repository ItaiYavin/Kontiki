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
            
            if(ai.job is Fisher){
                Fisher fisher = (Fisher)ai.job;
                if(fisher.selectedFishingSpot != null){
                    float distance = Vector3.Distance(fisher.boat.transform.position, fisher.selectedFishingSpot.position);
                    if(ai.debugAI_Job)
                        Debug.Log(ai.self.name + " is " + distance + " units away from " + fisher.selectedFishingSpot.name);    
                    bool b = distance < fisher.initFishingRange;
                
                    if(not)b = !b;
                    return b ? score : 0f;
                }
                
            }
            return 0f;
        }
	}
}
