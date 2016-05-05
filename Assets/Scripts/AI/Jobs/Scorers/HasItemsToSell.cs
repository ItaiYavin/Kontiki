using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has a boat
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class HasItemsToSell : ContextualScorerBase {
     

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            Fisher fisher = ((Fisher)ai.job);
            
            bool b = fisher.hasItems;
            
            if(Settings.debugInteractionInfo)
                Debug.Log(ai.self.name + " has " + (b ? "": "no") + " items"); 
            
            if(not)b = !b;
            
            return b ? score : 0f;
        }
	}
}
