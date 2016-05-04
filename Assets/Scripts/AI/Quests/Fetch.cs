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
		public int threshold = 8;

		public bool hasObjective;

		private Character player;

		private float sphereSizeReductionPercent = 0.8f; // Needs to be between 0-1;

		private float numberOfAskedPeople;
		private float numberOfHints;

		// Use this for initialization
		void Awake () {
			askedPeople = new List<Character>();
			player = GetComponent<Character>();
			hasObjective = false;
		}

		public void GetObjectiveItem(){
			
		}

		public override void UpdateQuest(Character askedCharacter){
			if(!CheckListForCharacter(askedCharacter, askedPeople))
			{
				bool isAskedCharacterInArea = true;

				if(sphere != null) //check if person is within sphere
					isAskedCharacterInArea = CheckCharacterIsInSphere(askedCharacter);

				if(isAskedCharacterInArea){
					bool askedCharacterKnows = false;
										
					int i = Random.Range(0, 10);
					if(i > (threshold - numberOfAskedPeople)) //calculate chance to know about goal
						askedCharacterKnows = true;

					if(askedCharacterKnows)
					{
						if(sphere == null)
							CreateSphereInWorld(sphereMaxSize, objectiveHolder.transform.position); //create sphere
							// WHAT KIND OF SPHERE? AREA OF INTEREST?
						else {
							if(sphere.transform.localScale.x > sphereMinSize){
								AdjustSphereInWorld(sphereSizeReductionPercent, objectiveHolder.transform.position);//make new smaller sphere
								numberOfAskedPeople = 0; //reset number of asked people
							}
						}
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
				//TODO asked character is outside of sphere inform player
			}
			//TODO player has already asked character, inform player
		}

		public override void FinishQuest(Inventory characterInventory){
			if(characterInventory.IsInventoryFull()){	// Check player inventory
				characterInventory.PutItemIntoInventory(reward); // Give reward
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

			if(hasObjective && objective == null)
				FinishQuest(player.inventory);
		}
	}
}