using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an empty inventory
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsInventoryEmpty : ContextualScorerBase {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            Inventory inventory = ((AIContext)context).inventory;

            bool b = inventory.IsInventoryEmpty();

            if(not) b = !b;

            if(debug) Debug.Log("Inventory is empty: " + b);

            return b ? 1f * score : 0f * score;
        }
	}
}
