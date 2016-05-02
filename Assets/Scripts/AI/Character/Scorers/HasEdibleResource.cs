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
      
        [ApexSerialization, FriendlyName("In Inventory", "if false will check character selected item")]
        public bool inInventory = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            Inventory inventory = ai.inventory;
            bool b = false;

            if(inInventory){
                if(!inventory.IsInventoryEmpty()){
                    for(int i = 0; i < inventory.inventorySize; i++){
                        if(inventory.GetInventoryItem(i) is EdibleItem)
                            b = true;
                    }
                }
            } else {
                if(character.HasSelected() != null && character.selectedInteractable is EdibleItem){
                    b = true;
                }
            }

            if(not) b = !b;
            if(ai.debugAI)
                Debug.Log("HasEdibleResource: " + b);

            return b ? 1f * score : 0f * score;
        }
    }
}
