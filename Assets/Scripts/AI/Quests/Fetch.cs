using UnityEngine;
using System.Collections;

namespace Kontiki
{
	public class Fetch : Quest {
		private Character origin;
		private Item objective;

		public Item questPrefab;

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public override void GiveQuest(){

		}

		public override void FinishQuest(Inventory characterInventory){ //take quest item
			// Check player inventory

			// if enough space for reward
				// Give reward

				// Remove quest

			// if not
				//inform player
		}
	}
}