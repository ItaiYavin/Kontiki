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
            Character character = ((CharacterAIContext)context).character;
            float pickUpRange = character.pickupRange;

            // Find every Objects within scanningRange area
            Collider[] colliders = Physics.OverlapSphere(character.transform.position, character.scanningRange);

            foreach (Collider c in colliders) {
                Item foundItem = c.GetComponent<Item>();
                if (foundItem != null)
                {
                	if(debug)
                		Debug.Log("Found item in pickup range");
                    return not ? 0f : 1f;
                }
            }

        	if(debug)
        		Debug.Log("Found NO item in pickup range");
            return not ? 1f : 0f;
	    }
	}
}
