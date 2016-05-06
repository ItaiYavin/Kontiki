using System;
using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{


    /// <summary>
    /// Sets characters selected item to the specified type from inventory
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public sealed class SelectItemOfTypeInInventory : ActionBase {
		

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;


        public override void Execute(IAIContext context){
			AIContext ai = (AIContext)context;
            Character character = ai.character;
            Inventory inv = ai.inventory;
            
            if(!inv.IsInventoryEmpty()){
	            Item bestCandidate = inv.GetInventoryItem(0);

	            switch(itemType){
	            	case ItemType.Edible:
	            		for(int i = 0; i < inv.inventorySize; i++){
	            			if(inv.GetInventoryItem(i) is EdibleItem)
			            		if(!(bestCandidate is EdibleItem))
			            			bestCandidate = inv.GetInventoryItem(i);
			            		else if((bestCandidate as EdibleItem).saturation < (inv.GetInventoryItem(i) as EdibleItem).saturation)
			            			bestCandidate = inv.GetInventoryItem(i);
	            		}
	            		break;
	            }

	            character.SetSelected(bestCandidate);
	        }
			if(ai.debugAI_Character){
					Debug.Log("Inventory is empty");
			}
        }
	}
}
