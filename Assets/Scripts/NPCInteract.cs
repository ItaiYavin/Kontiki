using UnityEngine;
using System.Collections;

namespace Kontiki
{
		
	public sealed class NPCInteract : Interactable {
		
		bool jumping = false;
		float nextJump = 0;
		float jumpLength = 1f;
		float jumpForce = 200f;
		
		Rigidbody body;
		
		void Awake(){
			body = GetComponent<Rigidbody>();
		}
		
		void FixedUpdate(){
			if(jumping && Time.time > nextJump){
				body.AddForce(Vector3.up*jumpForce);
				nextJump = Time.time + jumpLength;
			}
		}
		
        public override bool Interact(Character character){
			int inventorySize = character.GetInventory().inventorySize;
			if(inventorySize == 0){
				Debug.Log("You have no items!");
				return false;
			}
			for(int i = 0; i < inventorySize; i++){
				if(character.GetInventory().GetInventoryItem(i) is EdibleItem){
					character.GetInventory().GetInventoryItems()[i] = null;
					Debug.Log("YAY, thanks for the apple!");
					jumping = true;
					return true;
				}
			}
			
			
			Debug.Log("You have no edible items!");
	    	return false;
		}
	}
}
