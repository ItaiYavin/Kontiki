using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public class QuestGenerator : MonoBehaviour {		
		public GameObject questPrefab;

		public GameObject spherePrefab;

		private List<Character> charactersWithoutQuestObjects;

		// Use this for initialization
		void Start () {
			charactersWithoutQuestObjects = new List<Character>();
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		public void GenerateQuest(Character questGiver, GameObject player, Item reward){
			GameObject g = Instantiate(questPrefab, transform.position, transform.rotation) as GameObject;
			
			Item objective = g.GetComponent<Item>();
			int i = Random.Range(0, charactersWithoutQuestObjects.Count);

			player.AddComponent<Fetch>(); // TODO make general so it works with multiple quest types

			Fetch fetch = player.GetComponent<Fetch>();

			fetch.objective = objective;

			if(!charactersWithoutQuestObjects[i].inventory.IsInventoryFull()){
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventory(objective);
			} else {
				charactersWithoutQuestObjects[i].inventory.GetInventoryItems()[0] = null;
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventory(objective);
			}

			fetch.objectiveHolder = charactersWithoutQuestObjects[i];

			fetch.reward = reward;

			fetch.origin = questGiver;

			fetch.spherePrefab = spherePrefab;

			charactersWithoutQuestObjects.RemoveAt(i);
		}
	}
}
