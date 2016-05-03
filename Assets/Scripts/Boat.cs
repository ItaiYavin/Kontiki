using UnityEngine;
using System.Collections;

namespace Kontiki
{
			
	public class Boat : Interactable {
		
		public sui_demo_ControllerMaster CM;
		
		public Character characterInBoat;
		
		public NavMeshAgent agent;
		
		public bool isDocked = true;
		
		void Start(){
			agent = GetComponent<NavMeshAgent>();
		}
		
		public override bool Interact(Character character){
			if (character.isPlayer){
				if(CM != null){
					if (CM.currentControllerType == Sui_Demo_ControllerType.character){
						CM.currentControllerType = Sui_Demo_ControllerType.boat;
						RemoveHighlight();
						
					} else if (CM.currentControllerType == Sui_Demo_ControllerType.boat){
						CM.currentControllerType = Sui_Demo_ControllerType.character;
						CM.resetState();
					}
				}
			}
			
			if(characterInBoat == character){
				characterInBoat = null;
				character.transform.SetParent(null,true);
				
				if(!character.isPlayer){
					character.GetComponent<Pathfinder>().enabled = true;
					character.GetComponent<Pathfinder>().agent.enabled = true;
				}
			}else if(characterInBoat == null){
				characterInBoat = character;
				
				character.transform.SetParent(transform,true);
				character.transform.localPosition = Vector3.up * transform.localScale.y/2;
				character.transform.localRotation = transform.rotation;
				
				if(!character.isPlayer){
					character.GetComponent<Pathfinder>().enabled = false;
					character.GetComponent<Pathfinder>().agent.enabled = false;
				}
			}
			
			
				
			return true;
		}
		
		public void GoTo(Transform target){
			if(isDocked)isDocked = false;
			agent.destination = target.position;
			
		}
		
		
		public void Dock(Transform target){
			isDocked = true;
		}
		
        
        
			
	}
}
