using UnityEngine;
using UnityEngine.Generic;
using System.Collections;

namespace Kontiki
{
	public class QuestGenerator : MonoBehaviour {
		private List<Character> characters;

		// Use this for initialization
		void Start () {
			characters = new List<Characters>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public override void GenerateQuest(Character character, GameObject player){
			if(!character.inventory.IsInventoryFull()){
				character.inventory.PutItemIntoInventory(objective);
			} else {
				character.inventory.GetInventoryItem(0);
			}

			player.AddComponent("Fetch") as Fetch; // TODO make general so it works with multiple quest types
		}
	}
}
