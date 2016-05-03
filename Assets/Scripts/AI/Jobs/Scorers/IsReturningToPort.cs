using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI{
    
    /// <summary>
    /// Scorer for whether boat is returning to port
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsReturningToPort : ContextualScorerBase {

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;
        
        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            bool b = ai.job.boat.isReturningToPort;
            
            if(not) b = !b;
            
            return b ? score : 0f;
                
        }
    }
}