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

		[ApexSerialization, FriendlyName("Debug", "if on, writes debug messages to console")]
		bool debug;

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;

		public override void Execute(IAIContext context){
			Character character = ((CharacterAIContext)context).character;
            Inventory inventory = character.GetInventory();
            float pickUpRange = character.pickupRange;
            Vector3 target = character.target.position;
            Vector3 itemPos;

            Item closestItem;

			if(!inventory.IsInventoryFull()){
				// Find every Objects within scanningRange area
	            Collider[] colliders = Physics.OverlapSphere(character.transform.position, pickUpRange);

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
            	
            	float x = closestItem.transform.position.x - character.transform.position.x;
            	float y = closestItem.transform.position.y - character.transform.position.y;
            	float z = closestItem.transform.position.z - character.transform.position.z;
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
				            	
				            	x = itemsInRange[i].transform.position.x - character.transform.position.x;
				            	y = itemsInRange[i].transform.position.y - character.transform.position.y;
				            	z = itemsInRange[i].transform.position.z - character.transform.position.z;
				            	itemDistance = (x * x) + (y * y) + (z * z);

			            		if(itemDistance < closestDistance){
			            			closestItem = itemsInRange[i];
			            			closestDistance = itemDistance;
			            		}
			            	}
			            }
			            if(!(closestItem is EdibleItem)){
			            	if(debug) Debug.Log("No Edible Item found");
			            	return;
			            }

		        	break;
					
					case ItemType.fuel:
						if(debug) Debug.Log("FUEL NOT IMPLEMENTED YET");
					break;

					default:
						if(debug) Debug.Log("No pickup type chosen");
					break;
				}

				character.PickUpItem(closestItem);
			}
			else 
				if(debug) Debug.Log("Inventory is full");
		}
	}
}