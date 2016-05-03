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
            if(ai.job is JobWithBoat){
                
                JobWithBoat job = (JobWithBoat) ai.job;
                if(job.port != null){
                    float distance = Vector3.Distance( job.boat.transform.position, job.port.position);
                    bool b = distance < job.dockingRange;
                    
                    if(not)b = !b;
                    return b ? score : 0f;
                }else{
                    Debug.LogError("AI - Port is null");
                }
            }
            return 0f;
        }
	}
}
