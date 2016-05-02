using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class GoToDestination : ActionBase{
        
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            ai.pathfinder.GoToTarget();

            if(ai.debugAI) 
                Debug.Log("Moving to target");
        }
    }
}
