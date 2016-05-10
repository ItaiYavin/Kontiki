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
		
		[HideInInspector] public bool isDocked = true;
		
		[HideInInspector] public Transform target;
		
		
		bool movingForward = false;
		float endMoveForward;
		
		void Start(){
			CM = Settings.controller;
			agent = GetComponent<NavMeshAgent>();
			
		}
		
		public override bool Interact(Character player){
			if (player.isPlayer){
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
			
			if(characterInBoat == player){
				characterInBoat = null;
				player.transform.SetParent(null,true);
				
				if(!player.isPlayer){
					player.GetComponent<Pathfinder>().enabled = true;
					player.GetComponent<Pathfinder>().agent.enabled = true;
				}
			}else if(characterInBoat == null){
				characterInBoat = player;
				
				player.transform.rotation = transform.rotation;
				player.transform.SetParent(transform,true);
				player.transform.localPosition = seat.localPosition;
				player.transform.rotation = seat.rotation;
				
				if(!player.isPlayer){
					player.GetComponent<Pathfinder>().enabled = false;
					player.GetComponent<Pathfinder>().agent.enabled = false;
				}
			}
				
			return true;
		}
		
		public void GoTo(Transform target){
			if(isDocked){
				isDocked = false;
			}
			this.target = target;
			
			agent.SetDestination(target.position);
			
		}
		
		
		public void Dock(Transform target){
			isDocked = true;
			transform.position = target.position;
		}
	}
}
