using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Kontiki
{
	public class Fetch : Quest {

		public Character objectiveHolder;
		public Item objective;
		public int chanceForKnowledge = 8; // The higher the harder, roll is possible when below 10 (always possible when <= 0)

		public bool hasObjective;
		public bool hasGivenObjective;

		private float areaOfInterestSizeReductionPercent = 0.8f; // Needs to be between 0-1;

		private int numberOfAskedPeople;
		private int peakNumberOfPeople = 3;
		private int numberOfHints;
		
		
		
		public Color colorObjectiveHolder;
		public Color colorObjective;

		// Use this for initialization
		public Fetch (Character player, Character origin) : base(player, origin)
		{
		    this.origin = origin;
            this.player = player;

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

		public override bool CheckIfCharacterHasInfoAboutQuest(Character askedCharacter){
			if(!HasCharacterBeenAsked(askedCharacter))
			{
				bool isAskedCharacterInArea = true;

				if(areaOfInterest != null) //check if person is within AreaOfInterest
					isAskedCharacterInArea = CheckCharacterIsInAreaOfInterest(askedCharacter);
				
				float chanceForKnowledge;
				if(isAskedCharacterInArea){
					chanceForKnowledge = Random.Range(0f, 1f);
				}else{
					chanceForKnowledge = Random.Range(0f, 0.5f);
				}

				if(chanceForKnowledge > (chanceForKnowledge - ((float)numberOfAskedPeople / peakNumberOfPeople)))
				{
					if(areaOfInterest == null)
						CreateAreaOfInterestInWorld(objectiveHolder.transform); //create AreaOfInterest
					else{
						areaOfInterest.AdjustAreaOfInterestInWorld(areaOfInterestSizeReductionPercent, objectiveHolder.transform.position);	
					}
					numberOfAskedPeople = 0; //reset number of asked people
					askedPeople.Add(askedCharacter); //Add AI to list of characters that has been asked.
					return true;
				}else{
					numberOfAskedPeople++;
					Debug.Log("n People: " + numberOfAskedPeople);
					askedPeople.Add(askedCharacter); //Add AI to list of characters that has been asked.
					return false;
				}
			}
			return false;
		}
		
		public bool CheckIfCharacterHasObjective(Character askedCharacter){
			if(askedCharacter == objectiveHolder){
				GetObjectiveItem();
				return true;
			}
			return false;

		}

	}
}