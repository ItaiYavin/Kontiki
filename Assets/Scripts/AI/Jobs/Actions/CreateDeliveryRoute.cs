using Apex.AI;
using Kontiki;
using UnityEngine;
using System.Collections;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Dock the boat in the port
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class CreateDeliveryRoute : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            DeliveryMan job = (DeliveryMan) ai.job;
            job.CreateRoute();
        }
    }
}