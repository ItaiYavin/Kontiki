using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating whether an item is within pick up range
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public sealed class ItemInPickupRangeScore : ContextualScorerBase {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Inverts value")]
        public bool not = false;

	    public override float Score(IAIContext context){

	    }
	}
}
