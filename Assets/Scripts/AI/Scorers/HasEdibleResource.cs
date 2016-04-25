using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class HasEdibleResource : ContextualScorerBase
    {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            Character character = ((CharacterAIContext)context).character;
            Inventory inventory = character.GetInventory();
            bool b = false;;

            if(!inventory.IsInventoryEmpty()){
                for(int i = 0; i < inventory.inventorySize; i++){
                    if(inventory.GetInventoryItem(i) is EdibleItem)
                        b = true;
                }
            }

            if(not) b = !b;
            if(debug)
                Debug.Log("HasEdibleResource: " + b);

            return b ? 1f : 0f;
        }
    }
}
