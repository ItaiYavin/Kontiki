using UnityEngine;
using System.Collections;

namespace Kontiki
{
			
	public class Boat : Interactable {
		
		public sui_demo_ControllerMaster CM;
		
		public Character characterInBoat;
		
		public NavMeshAgent agent;
		
		public bool isDocked = true;
		public bool isReturningToPort = false;
		
        public Transform port;
        public float dockingRange;
		
		void Start(){
			agent = GetComponent<NavMeshAgent>();
		}
		
		public override bool Interact(Character character){
			if (character.isPlayer && CM != null){
				if (CM.currentControllerType == Sui_Demo_ControllerType.character){
					CM.currentControllerType = Sui_Demo_ControllerType.boat;
					RemoveHighlight();
					
				} else if (CM.currentControllerType == Sui_Demo_ControllerType.boat){
					CM.currentControllerType = Sui_Demo_ControllerType.character;
					CM.resetState();
				}
			}
			
			if(characterInBoat == character){
				characterInBoat = null;
			}else if(characterInBoat == null){
				characterInBoat = character;
				
				character.transform.position = transform.position + Vector3.up;
			}
				
			return true;
		}
		
		public void GoTo(Transform target){
			if(isDocked)isDocked = false;
			agent.destination = target.position;
			
		}
		
		
		public void Dock(Transform target){
			isDocked = true;
			isReturningToPort = false;
		}
		
        
        
        
        public void GoToPort(){
			isReturningToPort = true;
			GoTo(port);
        }
			
	}
}
