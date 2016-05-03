using UnityEngine;
using System.Collections;

namespace Kontiki
{
	public abstract class Quest : MonoBehaviour {
		public abstract void FinishQuest(Inventory characterInventory);

		public abstract void CheckQuestState();

		// Update is called once per frame
		void Update () {
			CheckQuestState();
		}
	}


}