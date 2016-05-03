using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Sail to port
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class SailToPort : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job is JobWithBoat){
                JobWithBoat job = (JobWithBoat) ai.job;
                job.GoToPort(); 
            }else{
                Debug.LogError("AI - Does not own a boat");
            }
        }
    }
}