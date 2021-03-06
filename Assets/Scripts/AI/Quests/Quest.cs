﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
	public abstract class Quest : Information {
		// areaOfInterest Variables
		public GameObject areaOfInterestPrefab;
		public AreaOfInterest areaOfInterest;

		public bool accepted = false;

		public List<Character> askedPeople;
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
            Object.Destroy(areaOfInterest.gameObject);
        }

		public void CreateAreaOfInterestInWorld(Vector3 from, Transform target){
			GameObject g = Object.Instantiate(areaOfInterestPrefab, from, Quaternion.identity) as GameObject; //Instantiate areaOfInterest of radius at target.position
			areaOfInterest = g.GetComponent<AreaOfInterest>();
			areaOfInterest.target = target;

            areaOfInterest.transform.localScale = Vector3.zero;
			Color c = colorOrigin;
			
			float size =areaOfInterest.areaOfInterestMaxSize;
			float lerpTime = (from - target.transform.position).magnitude;
			Debug.Log(lerpTime);
			if(lerpTime < 50f) {
				lerpTime = 6f;
				if(lerpTime < 25f)
					size = 15f;
			}else{
				lerpTime /= 5f;
			}
			
			c.a = areaOfInterest.maxAlpha + 0.25f;
			areaOfInterest.startUpdateTime = Time.time + lerpTime;
			areaOfInterest.ChangeColor(c, 2f);
			areaOfInterest.ChangeScale(size, 10f);
			areaOfInterest.ChangePosition(target, lerpTime);
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
			}
            // Character was not found on list!
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