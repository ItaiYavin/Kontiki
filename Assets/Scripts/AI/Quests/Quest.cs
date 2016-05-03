using UnityEngine;
using System.Collections;

namespace Kontiki
{
	public abstract class Quest : MonoBehaviour {
		public abstract void GiveQuest();

		public abstract void FinishQuest(Inventory characterInventory);
	}
}