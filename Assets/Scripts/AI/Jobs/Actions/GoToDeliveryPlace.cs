using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Dock the boat in the port
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class GoToDeliveryPlace : ActionBase{
        

        [ApexSerialization, FriendlyName("Move Towards Origin otherwise Route Destination", "")]
        public bool towardsOrigin = false;
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            DeliveryMan job = (DeliveryMan) ai.job;
            ai.pathfinder.GoTo((towardsOrigin ? job.origin.transform : job.destination.transform));
        }
    }
}