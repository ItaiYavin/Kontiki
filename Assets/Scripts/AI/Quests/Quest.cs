using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public abstract class Quest : MonoBehaviour {
		// areaOfInterest Variables
		public GameObject areaOfInterestPrefab;
		protected GameObject areaOfInterest;
		protected float areaOfInterestMaxSize;
		protected float areaOfInterestMinSize;

		public abstract void FinishQuest(Inventory characterInventory);

		public abstract void CheckQuestState();

		public abstract void UpdateQuest(Character askedPerson);

		// Update is called once per frame
		void Update () {
			CheckQuestState();
		}

		private void RandomlyMoveGameObjectWithinRange(GameObject g, Vector3 position){
			areaOfInterest.transform.position = position;
			float ranX;
			float ranY;
			ranX = Random.Range(-g.transform.localScale.x/2, g.transform.localScale.x/2);
			ranY = Random.Range(-g.transform.localScale.y/2, g.transform.localScale.y/2);

			g.transform.position += new Vector3(ranX, ranY, 0);
		}

		public void CreateAreaOfInterestInWorld(float radius, Vector3 position){
			areaOfInterest = Instantiate(areaOfInterestPrefab, position, transform.rotation) as GameObject; //Instantiate areaOfInterest of radius at position
			areaOfInterest.transform.localScale = new Vector3(radius, radius, radius); // Set size of areaOfInterest
			
			RandomlyMoveGameObjectWithinRange(areaOfInterest, position); //Randomly move areaOfInterest so that position is still within areaOfInterest
		}

		public void AdjustAreaOfInterestInWorld(float reduction, Vector3 position){
			areaOfInterest.transform.localScale *= reduction; //Resize areaOfInterest
			if(areaOfInterest.transform.localScale.x < areaOfInterestMinSize)
				areaOfInterest.transform.localScale = new Vector3(areaOfInterestMinSize, areaOfInterestMinSize, areaOfInterestMinSize);

			RandomlyMoveGameObjectWithinRange(areaOfInterest, position); //Move areaOfInterest so that position is still wihtin areaOfInterest
		}

		public bool CheckCharacterIsInAreaOfInterest(Character character){
			if(Vector3.Distance(character.transform.position, areaOfInterest.transform.position) < areaOfInterest.transform.localScale.x/2){
				return true;
			}
			// character is not within areaOfInterest
			return false;
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
	}
}