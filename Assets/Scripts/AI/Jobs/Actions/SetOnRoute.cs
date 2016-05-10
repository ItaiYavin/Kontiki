using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{


    /// <summary>
    /// Consumes the selected edible item if an edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class SetOnRoute : ActionBase{
        
        

        [ApexSerialization, FriendlyName("Value", "")]
        public bool val = false;

        public override void Execute(IAIContext context){
            AIContext ai = (AIContext) context;
            DeliveryMan job = (DeliveryMan)ai.job;
            if(!val) job.hasRoute = false;
            job.isOnRoute = val;
        }
    }
}
