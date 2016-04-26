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
        enum ItemType 
        {
            target,
            edible, 
            fuel,
            character
        };
        
        [ApexSerialization, FriendlyName("Item Types", "Type of item")]
        ItemType itemType;

        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Target", "Checks whether target is in pick up range")]
        public bool target = false;

        [ApexSerialization, FriendlyName("Not", "Inverts value")]
        public bool not = false;


        public override float Score(IAIContext context){
            Character character = ((CharacterAIContext)context).character;
            float pickUpRange = character.pickupRange;

            Vector3 targetPos = character.target.transform.position;
            bool b = false;

            switch(itemType){

                case ItemType.target:
                    if(Vector3.Distance(character.transform.position, targetPos) < pickUpRange)
                        b = true;    
                break;
                
                case ItemType.edible:
                    // Find all Objects within scanningRange area
                    Collider[] colliders = Physics.OverlapSphere(character.transform.position, pickUpRange);

                    foreach (Collider c in colliders) {
                        Item foundItem = c.GetComponent<Item>();
                        if (foundItem is EdibleItem)
                        {
                            if(debug)
                                Debug.Log("Found edible item in pickup range");
                            b = true;
                            continue;
                        }
                    }
                break;

                case ItemType.fuel:
                    if(debug) Debug.Log("Fuel is not implemented!");
                break;

                default:
                    if(debug) Debug.Log("No target type chosen");
                break;
            }

            if(not) b = !b;
            return b ? 1f : 0f;
        }
    }
}

                
