using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public abstract class Quest : Information {
		// areaOfInterest Variables
		public GameObject areaOfInterestPrefab;
		protected AreaOfInterest areaOfInterest;

		public bool accepted = false;

		protected List<Character> askedPeople;
        public Character player;
        public Character origin;
		public ItemType rewardType;
		
		public Color colorOrigin;

		public abstract bool CheckIfCharacterHasInfoAboutQuest(Character askedPerson);

	    public Quest(Character player, Character origin)
	    {
	        this.player = player;
	        this.origin = origin;
			askedPeople = new List<Character>();
	    }
        
        // Update is called once per frame
		
		public void FinishQuest(Inventory characterInventory){
			characterInventory.PutItemIntoInventoryRegardlessOfDistance(rewardType); // Give reward
			QuestSystem.Instance.RemoveQuest(this);
			
			Log.Quest_Completed(this);
		}

		public void CreateAreaOfInterestInWorld(Transform target){
			GameObject g = Object.Instantiate(areaOfInterestPrefab, target.position, Quaternion.identity) as GameObject; //Instantiate areaOfInterest of radius at target.position
			areaOfInterest = g.GetComponent<AreaOfInterest>();
			areaOfInterest.target = target;	
			
			areaOfInterest.ChangeColor(colorOrigin);
			areaOfInterest.ChangePosition(target.position); //Randomly move areaOfInterest so that position is still within areaOfInterest
		}
		
		public void RemoveAreaOfInterest(){
			Object.Destroy(areaOfInterest.gameObject);
		}

		public bool CheckCharacterIsInAreaOfInterest(Character character){
			return Vector3.Distance(character.transform.position, areaOfInterest.transform.position) < areaOfInterest.transform.localScale.x/2;
		}

		public void RemoveCharacterFromList(Character character, List<Character> list){
			for(int i = 0; i < list.Count; i++){
				if(list[i] == character){
					list.RemoveAt(i);
					return;
				}

				// Character was not found on list!
				return;
			}
		}

		public bool CheckListForCharacter(Character character, List<Character> list){
			for(int i = 0; i < list.Count; i++){
				if(list[i] == character)
					return true;
			}
			// Character was not found on list!
			return false;
		}
		
		public bool HasCharacterBeenAsked(Character askedCharacter){
			return CheckListForCharacter(askedCharacter, askedPeople);
		}
	}
}