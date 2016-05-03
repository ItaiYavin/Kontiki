using UnityEngine;
using System.Collections;

namespace Kontiki.AI{
    public class Fisher : JobWithBoat{
        
        [Header("Fishing Variables")]
        public Transform[] fishingSpots;
        
        public Transform selectedFishingSpot;
        
        public bool isFishing = false;
        
        public float initFishingRange = 5f; // Move Into Settings
        
        public float minFishingTime = 3f; //Move into Settings
        public float maxFishingTime = 6f; //Move into Settings
		
        
        public void GoToRandomFishingSpot(){
            if(fishingSpots.Length != 0){
                int index = Random.Range(0,fishingSpots.Length);
                Debug.Log("Sailing to " + index);
                selectedFishingSpot = fishingSpots[index];
                boat.GoTo(selectedFishingSpot);
            }
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
            selectedFishingSpot = null;
        }
    }
}
        
