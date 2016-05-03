using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Dock the boat in the port
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class DockBoat : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job is JobWithBoat){
                JobWithBoat job = (JobWithBoat) ai.job;
                job.DockAtPort();
            }else{
                Debug.LogError("AI - Does not own a boat");
            }
                
        }
    }
}