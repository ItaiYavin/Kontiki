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
            if(ai.job.boat != null){
                ai.job.boat.Dock(ai.job.boat.port);
            }else{
                Debug.LogError("AI - Does not own a boat");
            }
                
        }
    }
}