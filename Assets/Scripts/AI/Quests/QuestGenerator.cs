using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public class QuestGenerator : MonoBehaviour
	{
	    public EdibleItem reward;       		
		public GameObject objectivePrefab;
		public GameObject areaOfInterestPrefab;

	    public Quest proposedQuest;

		private List<Character> charactersWithoutQuestObjects;
	    private List<Quest> quests;

        // Static singleton property
        public static QuestGenerator Instance { get; private set; }

		// Use this for initialization
		void Start () {
			charactersWithoutQuestObjects = new List<Character>(); // TODO fill list
			Character[] tempChar = FindObjectsOfType(typeof (Character)) as Character[];
			foreach(Character character in tempChar){
				charactersWithoutQuestObjects.Add(character);
			}

			tempChar = null;
            quests = new List<Quest>(5);
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

		public Quest GenerateQuest(Character player, Character questGiver){
			GameObject g = Instantiate(objectivePrefab, transform.position, transform.rotation) as GameObject;
			
			Item objective = g.GetComponent<Item>();
			int i = Random.Range(0, charactersWithoutQuestObjects.Count);

            Fetch fetch = new Fetch(player, questGiver);
			fetch.objective = objective;

			if(!charactersWithoutQuestObjects[i].inventory.IsInventoryFull()){
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
			} else {
				charactersWithoutQuestObjects[i].inventory.GetInventoryItems()[0] = null;
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
			}

			fetch.objectiveHolder = charactersWithoutQuestObjects[i];
			fetch.reward = reward;
			fetch.origin = questGiver;
			fetch.areaOfInterestPrefab = areaOfInterestPrefab;

			charactersWithoutQuestObjects.RemoveAt(i);
            quests.Add(fetch);
		    return fetch;
		}

        public void RemoveQuest(Quest quest)
	    {
	        quests.Remove(quest);
	    }

	    public void ProposeQuest(Quest q)
	    {
	        proposedQuest = q;
	    }

	    public void AcceptProposedQuest()
        {
            if (Settings.debugQuestInfo)
                Debug.Log("Player accepted quest from " + proposedQuest.origin);

            quests.Add(proposedQuest);
	        proposedQuest = null;
        }

	    public void DeclineProposedQuest()
	    {
	        proposedQuest = null;
        }
	}
}
