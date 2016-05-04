using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public abstract class Quest : MonoBehaviour {
		// Sphere Variables
		public GameObject spherePrefab;
		protected GameObject sphere;
		protected float sphereMaxSize;
		protected float sphereMinSize;

		public abstract void FinishQuest(Inventory characterInventory);

		public abstract void CheckQuestState();

		public abstract void UpdateQuest(Character askedPerson);

		// Update is called once per frame
		void Update () {
			CheckQuestState();
		}

		private static void RandomlyMoveGameObjectWithinRange(GameObject g, Vector3 position){
			sphere.transform.position = position;
			float ranX;
			float ranY;
			ranX = Random.Range(-g.transform.localScale.x/2, g.transform.localScale.x/2);
			ranY = Random.Range(-g.transform.localScale.y/2, g.transform.localScale.y/2);

			g.transform.position += new Vector3(ranX, ranY, 0);
		}

		public static void CreateSphereInWorld(float radius, Vector3 position){
			sphere = Instantiate(spherePrefab, position, transform.rotation) as GameObject; //Instantiate Sphere of radius at position
			sphere.transform.localScale = new Vector3(radius, radius, radius); // Set size of sphere
			
			RandomlyMoveGameObjectWithinRange(sphere, position); //Randomly move sphere so that position is still within sphere
		}

		public static void AdjustSphereInWorld(float reduction, Vector3 position){
			sphere.transform.localScale *= reduction; //Resize sphere
			if(sphere.transform.localScale.x < sphereMinSize)
				sphere.transform.localScale = new Vector3(sphereMinSize, sphereMinSize, sphereMinSize);

			RandomlyMoveGameObjectWithinRange(sphere, position); //Move sphere so that position is still wihtin sphere
		}

		public static bool CheckCharacterIsInSphere(Character character){
			if(Vector3.Distance(character.transform.position, sphere.transform.position) < sphere.transform.localScale.x/2){
				return true;
			}
			// character is not within sphere
			return false;
		}

		public static void RemoveCharacterFromList(Character character, List<Character> list){
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