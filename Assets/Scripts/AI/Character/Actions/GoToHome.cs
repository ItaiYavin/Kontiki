using System.Diagnostics;
using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Goes to the specified destination
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class GoToHome : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = ((AIContext)context);
            ((AIContext)context).pathfinder.GoTo(ai.baseroutine.home);
        }
    }
}