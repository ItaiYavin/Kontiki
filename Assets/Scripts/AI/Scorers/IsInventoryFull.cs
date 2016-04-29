using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has a full inventory
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsInventoryFull : ContextualScorerBase {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;
        
        public override float Score(IAIContext context){
            Inventory inventory = ((AIContext)context).inventory;

            bool b = inventory.IsInventoryFull();

            if(not) b = !b;

            return b ? 1f * score : 0f * score;
        }
	}
}
