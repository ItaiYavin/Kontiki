using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Kontiki
{
	public class Fetch : Quest {
		private List<Character> askedPeople;

		public Character origin;
		public Character objectiveHolder;
		public Item objective;
		public Item reward;
		public int chanceForKnowledge = 8; // The higher the harder, roll is possible when below 10 (always possible when <= 0)

		public bool hasObjective;
		public bool hasGivenObjective;

		private Character player;

		private float areaOfInterestSizeReductionPercent = 0.8f; // Needs to be between 0-1;

		private int numberOfAskedPeople;
		private int peakNumberOfPeople = 10;
		private int numberOfHints;

		// Use this for initialization
		public Fetch (Character player, Character origin) : base(player, origin)
		{
		   
			askedPeople = new List<Character>();
			hasObjective = false;
		}

		public void GiveObjectiveItem(){

			int i = player.inventory.CheckInventoryForSpecificItemAndReturnIndex(objective);
			if(i != -1){
				player.inventory.GetInventoryItems()[i] = null;
				objective = null;
			}

			FinishQuest(player.inventory);
		}

		public void GetObjectiveItem(){
			Item[] objectiveHolderInventoryItems = objectiveHolder.inventory.GetInventoryItems();
			
			for(int i = 0; i < objectiveHolderInventoryItems.Length; i++){
				if(objectiveHolderInventoryItems[i] == objective){
					player.inventory.PutItemIntoInventoryRegardlessOfDistance(objective);
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

					float chanceForKnowledge = Random.Range(0f, 1f);

					if(chanceForKnowledge > (chanceForKnowledge - (numberOfAskedPeople / peakNumberOfPeople)))
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
			if(!characterInventory.IsInventoryFull()){	// Check player inventory
				characterInventory.PutItemIntoInventoryRegardlessOfDistance(reward); // Give reward
			    QuestGenerator.Instance.RemoveQuest(this);
			} else {
				//TODO inform player
			}
		}

		public override void CheckQuestState(){
			if(!hasObjective){
				if(player.inventory.CheckInventoryForSpecificItem(objective))
					hasObjective = true;
			}
		}

	    public override string ToString()
	    {
	        string s = "";

	        s += "Origin: " + origin.name;
            s += "\nObjective Holder: " + objectiveHolder.name;
            s += "\nObjective: " + objective.name;

	        return s;
	    }
	}
}