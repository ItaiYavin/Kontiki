using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public class QuestGenerator : MonoBehaviour {		
		public GameObject objectivePrefab;

		public GameObject areaOfInterestPrefab;

		private List<Character> charactersWithoutQuestObjects;

        // Static singleton property
        public static QuestGenerator Instance { get; private set; }

		// Use this for initialization
		void Start () {
			charactersWithoutQuestObjects = new List<Character>(); // TODO fill list
		}

		void Awake(){
			// First we check if there are any other instances conflicting
            if (Instance != null && Instance != this)
            {
                // If that is the case, we destroy other instances
                Destroy(gameObject);
            }

            // Here we save our singleton instance
            Instance = this;

            // Furthermore we make sure that we don't destroy between scenes (this is optional)
            DontDestroyOnLoad(gameObject);
		}

		public void GenerateQuest(Character questGiver, GameObject player, Item reward){
			GameObject g = Instantiate(objectivePrefab, transform.position, transform.rotation) as GameObject;
			
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

			fetch.areaOfInterestPrefab = areaOfInterestPrefab;

			charactersWithoutQuestObjects.RemoveAt(i);
		}
	}
}
