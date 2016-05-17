using UnityEngine;
using System.Collections;

namespace Kontiki.AI{
    public class Fisher : JobWithBoat{
        
        [Header("Fishing Variables")]
        
        public bool isScavenger = false;
        public Transform[] fishingSpots;
        
        public Transform selectedFishingSpot;
        
        public bool isFishing = false;
        
        public bool hasItems = false;
        public ItemType itemType = ItemType.Edible;
        
        public float initFishingRange = 5f; // Move Into Settings
        
        public float minFishingTime = 3f; //Move into Settings
        public float maxFishingTime = 6f; //Move into Settings
		
        
        void Start(){
            if(isScavenger){
                fishingSpots = Settings.scavengingSpots.ToArray();
            }else{
                fishingSpots = Settings.fishingSpots.ToArray();
            }
            
            port = Settings.GetPort();
            port.name = gameObject.name + "'s Port";
            GameObject g = Instantiate(boatPrefab, port.transform.position, port.transform.rotation) as GameObject;
            boat = g.GetComponent<Boat>();
            boat.name = gameObject.name + "'s Boat";
        }
        
        public void SelectRandomFishingSpot(){
            if(fishingSpots.Length != 0){
                int index = Random.Range(0,fishingSpots.Length);
                selectedFishingSpot = fishingSpots[index];
            }
        }
        
        public void GoToSelectedFishingSpot(){
            if(selectedFishingSpot != null)
                boat.GoTo(selectedFishingSpot);
            else
                Debug.Log(gameObject.name + " is trying to go to selectedFishingSpot but is null");
        }
        
        public void StartFishing(){
            if(selectedFishingSpot != null){
                isFishing = true;
                StartCoroutine(Routine_StopFishing(Random.Range(
                    minFishingTime,
                    maxFishingTime
                )));
            }
        }
        
        
        IEnumerator Routine_StopFishing(float length){
            yield return new WaitForSeconds(length);
            isFishing = false;
            hasItems = true;
            selectedFishingSpot = null;
        }
    }
}
        
