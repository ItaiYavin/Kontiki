using UnityEngine;
using System.Collections;

namespace Kontiki
{
			
    [RequireComponent(typeof(NavMeshAgent))]
	public class Boat : Interactable {
		
		[HideInInspector] public sui_demo_ControllerMaster CM;
		
		[HideInInspector] public Character characterInBoat;

		public Transform seat;
		
		[HideInInspector] public NavMeshAgent agent;
		
		public bool isDocked = true;
		
		public Transform target;
		
		
		bool movingForward = false;
		float endMoveForward;
		
		void Start(){
			CM = Settings.controller;
			agent = GetComponent<NavMeshAgent>();
			
		}
		
		public override bool Interact(Character character){
			if (character.isPlayer){
				if(CM != null){
					if (CM.currentControllerType == Sui_Demo_ControllerType.character){
						CM.currentControllerType = Sui_Demo_ControllerType.boat;
						
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
				
				character.transform.rotation = transform.rotation;
				character.transform.SetParent(transform,true);
				character.transform.localPosition = seat.localPosition;
				character.transform.rotation = seat.rotation;
				
				if(!character.isPlayer){
					character.GetComponent<Pathfinder>().enabled = false;
					character.GetComponent<Pathfinder>().agent.enabled = false;
				}
			}
				
			return true;
		}
		
		public void GoTo(Transform target){
			if(isDocked){
				isDocked = false;
				agent.Resume();
			}
			this.target = target;
			
			bool b = agent.SetDestination(target.position);
			
		}
		
		
		public void Dock(Transform target){
			isDocked = true;
			transform.position = target.position;
		}
	}
}
