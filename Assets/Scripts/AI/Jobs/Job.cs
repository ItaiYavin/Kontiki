using UnityEngine;
using System.Collections;

namespace Kontiki.AI{
    public class Job : MonoBehaviour{
        

        public enum Type{
            Fisher,
            Scavenger,
            Trader            
        }
        
        public Job.Type type;
        
        public bool isInBoat;
        public Boat boat;
        
        
        
        
        /////////////////
        //// FISHER
        //////////////
        [Header("Fishing Variables")]
        public Transform[] fishingSpots;
        
        public Transform selectedFishingSpot;
        
        public bool isFishing = false;
        
        public float initFishingRange = 5f;
        
        public float minFishingTime = 3f;
        public float maxFishingTime = 6f;
        
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
                StartCoroutine(Routine_StopFishing(Random.Range(minFishingTime,maxFishingTime)));
            }
        }
        
        
        IEnumerator Routine_StopFishing(float length){
            yield return new WaitForSeconds(length);
            isFishing = false;
            selectedFishingSpot = null;
        }
    }
}