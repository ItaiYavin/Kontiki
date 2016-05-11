using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Kontiki {
	public class QuestItem : Item
	{
	    public Quest quest;

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