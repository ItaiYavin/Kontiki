using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public class Fetch : Quest {
		private List<Character> askedPeople;

		public Character origin;
		public Character objectiveHolder;
		public Item objective;
		public Item reward;
		public int threshold = -1; // The higher the harder, roll is possible when below 10 (always possible when <= 0)

		public bool hasObjective;

		private Character player;

		private float areaOfInterestSizeReductionPercent = 0.8f; // Needs to be between 0-1;

		private float numberOfAskedPeople;
		private float numberOfHints;

		// Use this for initialization
		void Awake () {
			askedPeople = new List<Character>();
			player = GetComponent<Character>();
			hasObjective = false;
		}

		public void GiveObjectiveItem(){
			int i = player.inventory.CheckInventoryForSpecificItemAndReturnIndex(objective);
			if(i != -1){
				player.inventory.GetInventoryItems()[i] = null;
				objective = null;
			}
		}

		public void GetObjectiveItem(){
			Item[] objectiveHolderInventoryItems = objectiveHolder.inventory.GetInventoryItems();
			
			for(int i = 0; i < objectiveHolderInventoryItems.Length; i++){
				if(objectiveHolderInventoryItems[i] == objective){
					transform.GetComponent<Character>().inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
					objectiveHolderInventoryItems[i] = null;
				}
			}
		}

		public override void UpdateQuest(Character askedCharacter){
			if(askedCharacter == objectiveHolder)
				GetObjectiveItem();

			if(!CheckListForCharacter(askedCharacter, askedPeople))
			{
				bool isAskedCharacterInArea = true;

				if(areaOfInterest != null) //check if person is within AreaOfInterest
					isAskedCharacterInArea = CheckCharacterIsInAreaOfInterest(askedCharacter);

				if(isAskedCharacterInArea){
					bool askedCharacterKnows = false;

					int i = Random.Range(0, 10);
					if(i > (threshold - numberOfAskedPeople)) //calculate chance to know about goal
						askedCharacterKnows = true;

					if(askedCharacterKnows)
					{
						if(areaOfInterest == null)
							CreateAreaOfInterestInWorld(areaOfInterestMaxSize, objectiveHolder.transform.position); //create AreaOfInterest
						else 
						{
							if(areaOfInterest.transform.localScale.x > areaOfInterestMinSize){
								AdjustAreaOfInterestInWorld(areaOfInterestSizeReductionPercent, objectiveHolder.transform.position);//make new smaller AreaOfInterest
								numberOfAskedPeople = 0; //reset number of asked people
							}
						}
						askedPeople.Add(askedCharacter);//Add AI to list of characters that has been asked.
						return;
					}
					else 
					{
						numberOfAskedPeople++;
						//TODO inform player that this AI does not know

						askedPeople.Add(askedCharacter);//Add AI to list of characters that has been asked.
						return;
					}
				}
				//TODO asked character is outside of areaOfInterest inform player
			}
			//TODO player has already asked character, inform player
		}

		public override void FinishQuest(Inventory characterInventory){
			if(characterInventory.IsInventoryFull()){	// Check player inventory
				characterInventory.PutItemIntoInventoryRegardlessOfDistance(reward); // Give reward
				Destroy(this); // Remove quest
			} else {
				//TODO inform player
			}
		}

		public override void CheckQuestState(){
			if(!hasObjective){
				if(player.inventory.CheckInventoryForSpecificItem(objective))
					hasObjective = true;
			}

			if(hasObjective && !player.inventory.CheckInventoryForSpecificItem(objective))
				FinishQuest(player.inventory);
		}
	}
}