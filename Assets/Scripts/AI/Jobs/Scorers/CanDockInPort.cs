using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for if the boat can dock at the port yet.
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class CanDockInPort : ContextualScorerBase {

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job.boat.port != null){
                float distance = Vector3.Distance( ai.job.boat.transform.position, ai.job.boat.port.position);
                bool b = distance < ai.job.boat.dockingRange;
                
                if(not)b = !b;
                return b ? score : 0f;
            }
            return 0f;
        }
	}
}
