using Apex.AI;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    
    
    /// <summary>
    /// Consumes the selected edible item if a edible is selected otherwise nothing.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    
    public sealed class SelectRandomFishingSpot : ActionBase{
        public override void Execute(IAIContext context){
            AIContext ai = (AIContext)context;
            if(ai.job is Fisher){
                Fisher fisher = (Fisher)ai.job;
                if(fisher.selectedFishingSpot == null)
                    fisher.SelectRandomFishingSpot();
            }else
                Debug.LogError("AI - Only a Fisher can SailToRandomFishSpot");
          
        }
    }
}