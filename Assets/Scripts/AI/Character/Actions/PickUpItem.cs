using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    /// <summary>
    /// Picks up item into the inventory (if not already there)
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public class PickUpItem : ActionBase {
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
			/*switch(){
				Case :
		        	Item pickup;
		        	if(Vector3.Distance(item.transform.position, transform.position) < pickupRange){
		        		pickup = item;
		        		for(int i = 0; i < inv.inventorySize; i++){
		        			if(inv.GetInventoryItem(i) == null){
		        				inv.GetInventoryItems()[i] = pickup;
		        				item.gameObject.SetActive(false);
		        				return;
		        			}
		    			}
		    			pickup = null;
		    			//INVENTORY IS FULL IF CODE EVER GETS HERE
		        	}
			}*/
		}
	}
}