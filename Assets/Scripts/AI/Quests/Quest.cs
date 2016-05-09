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
		}

		public void CreateAreaOfInterestInWorld(Vector3 position){
			GameObject g = Object.Instantiate(areaOfInterestPrefab, position, Quaternion.identity) as GameObject; //Instantiate areaOfInterest of radius at position
			areaOfInterest = g.GetComponent<AreaOfInterest>();
			Debug.Log(areaOfInterest + " " + colorOrigin);
			areaOfInterest.ChangeColor(colorOrigin);
			areaOfInterest.ChangePosition(position); //Randomly move areaOfInterest so that position is still within areaOfInterest
		}
		
		public void RemoveAreaOfInterest(){
			Color c = colorOrigin;
			c.a = 0;
			areaOfInterest.ChangeColor(c);
			Object.Destroy(areaOfInterest.gameObject,areaOfInterest.animationDuration);
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