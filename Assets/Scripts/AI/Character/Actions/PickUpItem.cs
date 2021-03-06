﻿using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;
using System.Collections.Generic;

namespace Kontiki.AI{
    /// <summary>
    /// Picks up item into the inventory (if not already there)
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public class PickUpItem : ActionBase {
		enum ItemType 
		{
			target,
			edible, 
			fuel
		};

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;

		public override void Execute(IAIContext context){
			AIContext ai = (AIContext)context;
            float pickUpRange = Settings.pickupRange;
            Vector3 target = ai.pathfinder.target.position;
            Vector3 itemPos;

            Item closestItem;

			if(!ai.inventory.IsInventoryFull()){
				// Find every Objects within scanningRange area
	            Collider[] colliders = Physics.OverlapSphere(ai.transform.position, pickUpRange);

	            // Look through all colliders and Look for EdibleItem and put them in a list
	            List<Item> itemsInRange = new List<Item>();
	            foreach (Collider c in colliders){
	                Item foundItem = c.GetComponent<Item>();
	                if (foundItem != null)
	                {
	                    itemsInRange.Add(foundItem);
	                }
            	}

            	closestItem = itemsInRange[0];
            	float closestDistance;
            	
            	float x = closestItem.transform.position.x - ai.transform.position.x;
            	float y = closestItem.transform.position.y - ai.transform.position.y;
            	float z = closestItem.transform.position.z - ai.transform.position.z;
            	closestDistance = (x * x) + (y * y) + (z * z);
            	
				switch(itemType){
					case ItemType.target:
						itemPos = target;
					break;
					
					case ItemType.edible:
			            for(int i = 0; i < itemsInRange.Count; i++){
			            	if(itemsInRange[i] is EdibleItem){
			            		if(!(closestItem is EdibleItem)){
			            			closestItem = itemsInRange[i];
			            			continue;
			            		}

				            	float itemDistance;
				            	
				            	x = itemsInRange[i].transform.position.x - ai.transform.position.x;
				            	y = itemsInRange[i].transform.position.y - ai.transform.position.y;
				            	z = itemsInRange[i].transform.position.z - ai.transform.position.z;
				            	itemDistance = (x * x) + (y * y) + (z * z);

			            		if(itemDistance < closestDistance){
			            			closestItem = itemsInRange[i];
			            			closestDistance = itemDistance;
			            		}
			            	}
			            }
			            if(!(closestItem is EdibleItem)){
			            	if(ai.debugAI_Interaction) 
								Debug.Log("No Edible Item found");
			            	return;
			            }

		        	break;
				}

				ai.inventory.PutItemIntoInventory(closestItem);
			}
			else 
				if(ai.debugAI_Interaction) Debug.Log("Inventory is full");
		}
	}
}