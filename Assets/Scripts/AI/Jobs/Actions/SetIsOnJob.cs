using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{


    /// <summary>
    /// Consumes the selected edible item if an edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class SetIsOnJob : ActionBase{
        
        

        [ApexSerialization, FriendlyName("Value", "")]
        public bool val = false;

        public override void Execute(IAIContext context){
            AIContext ai = (AIContext) context;
            ai.isOnJob = val;
        }
    }
}
