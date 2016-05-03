using UnityEngine;
using System.Collections;

namespace Kontiki
{
	public class Fetch : Quest {
		public Character origin;
		public Character objectiveHolder;
		public Item objective;
		public Item reward;

		public bool hasObjective;

		private Character player;

		// Use this for initialization
		void Awake () {
			player = GetComponent<Character>();
			hasObjective = false;
		}

		public override void FinishQuest(Inventory characterInventory){ //take quest item
			// Check player inventory

			// if enough space for reward
				// Give reward

				// Remove quest

			// if not
				//inform player
		}

		public override void CheckQuestState(){
			if(!hasObjective){
				if(player.inventory.CheckInventoryForSpecificItem(objective))
					hasObjective = true;
			}

			if(hasObjective && objective == null)
				FinishQuest(player.inventory);
		}
	}
}