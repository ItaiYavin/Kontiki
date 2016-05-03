using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class GoToBoat : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job is JobWithBoat){
                JobWithBoat job = (JobWithBoat) ai.job;
                ai.pathfinder.target = job.boat.transform;
                ai.pathfinder.GoToTarget();
            }else{
                Debug.LogError("AI - Does not own a boat");
            }
                
        }
    }
}