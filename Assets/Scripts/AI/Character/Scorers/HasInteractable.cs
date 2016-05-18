using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using System;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
    public sealed class HasInteractable : ContextualScorerBase
    {
      
        [ApexSerialization, FriendlyName("in Inventory", "will check if character has edible resource in inventory")]
        public bool inInventory = false;
        
        [ApexSerialization, FriendlyName("in Hand", "will check character selected item")]
        public bool inHand = false;
        
        [ApexSerialization, FriendlyName("Type", "item type")]
        public ItemType itemType;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            Inventory inventory = ai.inventory;
            bool b = false;
            

            if(inInventory && !inventory.IsInventoryEmpty()){
                for(int i = 0; i < inventory.inventorySize; i++){
                    if(inventory.IsInventoryItemOfType(i,itemType))
                        b = true;
                }
            }
            if(inHand && character.HasSelected(itemType)){
                b = true;
            }

            if(not) b = !b;

            return b ? 1f * score : 0f * score;
        }
    }
}
