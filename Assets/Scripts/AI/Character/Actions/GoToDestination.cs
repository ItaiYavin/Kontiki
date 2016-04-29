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
    	[ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        public override void Execute(IAIContext context){
            ((AIContext)context).pathfinder.GoToTarget();

            if(debug) 
            Debug.Log("Moving to target");
        }
    }
}
