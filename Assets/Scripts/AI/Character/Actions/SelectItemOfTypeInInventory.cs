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
		enum ItemType 
		{
			edible, 
			fuel
		};

		[ApexSerialization, FriendlyName("Debug", "if on, writes debug messages to console")]
		bool debug;

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;


        public override void Execute(IAIContext context){
            Character character = ((CharacterAIContext)context).character;
            Inventory inv = character.GetComponent<Inventory>();
            
            if(!inv.IsInventoryEmpty()){
	            Item bestCandidate = inv.GetInventoryItem(0);

	            switch(itemType){
	            	case ItemType.edible:
	            		for(int i = 0; i < inv.inventorySize; i++){
	            			if(inv.GetInventoryItem(i) is EdibleItem)
			            		if(!(bestCandidate is EdibleItem))
			            			bestCandidate = inv.GetInventoryItem(i);
			            		else if((bestCandidate as EdibleItem).saturation < (inv.GetInventoryItem(i) as EdibleItem).saturation)
			            			bestCandidate = inv.GetInventoryItem(i);
	            		}
	            		break;

	            	case ItemType.fuel:
	            		if(debug)
	            			Debug.Log("THIS OPTION IS NOT IMPLEMENTED YET");
	            		break;

	            	default:
	            		if(debug)
	            			Debug.Log("No type was chosen");
	            		break;
	            }

	            character.selectedItem = bestCandidate;
	        }
	        if(debug)
	        	Debug.Log("Inventory is empty");
        }
	}
}
