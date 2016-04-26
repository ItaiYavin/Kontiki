using UnityEngine;
using System.Collections;

namespace Kontiki
{
			
	public class Boat : Interactable {
		
		public sui_demo_ControllerMaster CM;
		
		public override bool Interact(Character character){
			if (CM != null){
				if (CM.currentControllerType == Sui_Demo_ControllerType.character){
					CM.currentControllerType = Sui_Demo_ControllerType.boat;
					RemoveHighlight();
					
				} else if (CM.currentControllerType == Sui_Demo_ControllerType.boat){
					CM.currentControllerType = Sui_Demo_ControllerType.character;
					CM.resetState();
				}
				return true;
			}
			
			return false;
		}
		
		
		
			
	}
}
