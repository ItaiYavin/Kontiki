using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class StartFishing : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job.boat != null && ai.job.type == Job.Type.Fisher ){
                ai.job.StartFishing();
            }else{
                Debug.LogError("AI - Does not own a boat");
            }
                
        }
    }
}