using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public class QuestSystem : MonoBehaviour
	{
	    public ItemType rewardType = ItemType.Edible;       		
		public GameObject objectivePrefab;
		public GameObject areaOfInterestPrefab;
		private List<Character> charactersWithoutQuestObjects;
	    public List<Quest> quests;
		public List<Quest> acceptedQuests;
		
        public List<int> usedPersonColors = new List<int>();

        // Static singleton property
        public static QuestSystem Instance { get; private set; }

		// Use this for initialization
		void Start () {
			charactersWithoutQuestObjects = new List<Character>();
			Character[] tempChar = FindObjectsOfType(typeof (Character)) as Character[];
			foreach(Character character in tempChar){
				charactersWithoutQuestObjects.Add(character);
			}

			tempChar = null;
            quests = new List<Quest>(5);
			acceptedQuests = new List<Quest>(5);
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
 
		public Quest GenerateQuest(Character questGetter, Character questOrigin){
			//@TODO(KasperHdL) make generic, fetch quest specific currently..
			GameObject g = Instantiate(objectivePrefab, transform.position, transform.rotation) as GameObject;
			
			QuestItem objective = g.GetComponent<QuestItem>();
			charactersWithoutQuestObjects.Remove(questOrigin);
			int i = Random.Range(0, charactersWithoutQuestObjects.Count);

            Fetch fetch = new Fetch(questGetter, questOrigin);
			fetch.objective = objective;

			if(!charactersWithoutQuestObjects[i].inventory.IsInventoryFull()){
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
			} else {
				charactersWithoutQuestObjects[i].inventory.GetInventoryItems()[0] = null;
				charactersWithoutQuestObjects[i].inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
			}

			fetch.objectiveHolder = charactersWithoutQuestObjects[i];
			fetch.rewardType = rewardType;
			fetch.origin = questOrigin;
			fetch.areaOfInterestPrefab = areaOfInterestPrefab;
			
			fetch.colorOrigin = GetUnusedPersonColor();
			fetch.colorObjective = fetch.colorOrigin;

			charactersWithoutQuestObjects.RemoveAt(i);
            quests.Add(fetch);
		    return fetch;
		}

        public void RemoveQuest(Quest quest)
	    {
			acceptedQuests.Remove(quest);
	        quests.Remove(quest);
	    }
		
	    public void AcceptQuest(Quest quest)
        {
            if (Settings.debugQuestInfo)
                Debug.Log("Player accepted quest from " + quest.origin);

            acceptedQuests.Add(quest);
			
			Log.Quest_Accepted(quest);
        }
		
	
	
		public Quest[] GetAcceptedQuests(){
//			Debug.Log("acceptedQuests " + acceptedQuests.Count);
			return acceptedQuests.ToArray();
		}
		
	
		/**
		 * Colors 
		 **/
		 
		public Color GetUnusedPersonColor(){
			if(usedPersonColors.Count == Settings.languageColors.Count){
				Debug.LogError("All Language Colors has been Used - defaulting to white");
				return new Color(1,1,1);
			}
			
			int index = Random.Range(0,Settings.languageColors.Count);
			while(usedPersonColors.IndexOf(index) != -1)
				index = Random.Range(0,Settings.languageColors.Count);
			usedPersonColors.Add(index);
			return Settings.languageColors[index];
		}
		
		public void FreeUsedPersonColor(Color color){
			FreeUsedPersonColor(Settings.languageColors.IndexOf(color));
		}
		
		public void FreeUsedPersonColor(int index){
			if(index < 0 || index >= usedPersonColors.Count){
				return;
			}
			usedPersonColors.Remove(index);
		}
		
	}
}
