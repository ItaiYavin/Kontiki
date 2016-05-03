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
            ai.job.boat.GoToPort(); 
        }
    }
}