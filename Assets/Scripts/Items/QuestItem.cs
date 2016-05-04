using UnityEngine;
using System.Collections;

namespace Kontiki {
	public class QuestItem : Item {

		// Use this for initialization
		void Start () {
		
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public override bool UseItem(Character character){
			return false;
		}
	}
}