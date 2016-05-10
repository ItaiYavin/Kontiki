using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has a boat
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsOnDeliveryRoute : ContextualScorerBase {
     

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            DeliveryMan job = (DeliveryMan)ai.job;
            
            bool b = job.isOnRoute;
            
            if(ai.debugAI_Job)
                Debug.Log(ai.self.name + " has " + (b ? "": "no") + " route"); 
            
            if(not)b = !b;
            
            return b ? score : 0f;
        }
	}
}
