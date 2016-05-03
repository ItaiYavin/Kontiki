using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{


    /// <summary>
    /// Consumes the selected edible item if an edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />

    public sealed class SelectBoat : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext) context;
            if(ai.job is JobWithBoat){
                JobWithBoat job = (JobWithBoat) ai.job;
                ai.character.SetSelected(job.boat);
            }
        }
    }
}
