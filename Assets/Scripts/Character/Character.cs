using Apex.AI;
using UnityEngine;
using System;
using System.Collections.Generic;
using Apex.AI.Components;

using Kontiki.AI;

namespace Kontiki
{
    public sealed class Character : MonoBehaviour, IContextProvider{

  
        /**
        ** Inspector Items and Configuration
        **/
        public Gender gender;
        [Range(0, 1)]
        
        
        public float scanningRange = 1;
        
        //Stats
        public float energy = 1;
        [Range(0, 1)]
        public float hunger = 0;
        [Range(0,100)]
        
        
        //Stat affectors
        public float hungerIncrementPerSec = 0.001f;

        /**
        ** Private Variables & Objects
        **/
        public EdibleItem selectedEdibleItem;
        private CharacterAIContext _context;

        /**
        ** Static Variables & Objects
        **/
        
        //Stat Min & Max
        public static float hungerMax = 100f;
        public static float hungerMin = 0f;
        
        private void Awake(){
            _context = new CharacterAIContext(this);
        }
        

        public IAIContext GetContext(Guid aiId)
        {
            return _context;
        }


        void FixedUpdate()
        {
            if(hunger < hungerMax){
                hunger += hungerIncrementPerSec * Time.deltaTime;
            }else{
                hunger = hungerMax;
            }
            
            selectedEdibleItem = CheckForClosetEdibleInRange();

            if (selectedEdibleItem != null)
            {
				if(Input.GetKeyDown(KeyCode.Q)) {
					ConsumeEdibleItem(selectedEdibleItem);
				}
            }
        }
        
        /**
        * Actions
        * 
        **/
        
        public bool HasSelectedEdibleResource(){
            return selectedEdibleItem != null;
        }
        
        public void SelectClosestEdibleInRange(){
            if(selectedEdibleItem != null){
                selectedEdibleItem = CheckForClosetEdibleInRange();
            }
        }
        EdibleItem CheckForClosetEdibleInRange()
        {
            // Find every Objects within scanningRange area
            Collider[] colliders = Physics.OverlapSphere(transform.position, scanningRange);

            // Look through all colliders and Look for EdibleItem and put them in a list
            List<EdibleItem> edibleItemsInRange = new List<EdibleItem>();
            foreach (Collider c in colliders) {
                EdibleItem foundEdibleItem = c.GetComponent<EdibleItem>();
                if (foundEdibleItem != null)
                {
                    edibleItemsInRange.Add(foundEdibleItem);
                }
            }

            // If we found edibleItems then find the closet one to the player
            if(edibleItemsInRange.Count > 0) {
                EdibleItem closetEdibleItem = edibleItemsInRange[0];
                Vector3 v = (edibleItemsInRange[0].transform.position - transform.position);
                float currentShortestDistance =  v.sqrMagnitude;

                for(int i = 1; i < edibleItemsInRange.Count; i++) {
                    Vector3 cv = (edibleItemsInRange[i].transform.position - transform.position);
                    float distance =  cv.sqrMagnitude;

                    if(Mathf.Abs(distance) < Mathf.Abs(currentShortestDistance)) {
                        closetEdibleItem = edibleItemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }

                return closetEdibleItem;
            }

            return null;
        }


        public void ConsumeSelectedEdibleItem(){
            if(selectedEdibleItem != null){
                ConsumeEdibleItem(selectedEdibleItem);
            }
        }
        void ConsumeEdibleItem(EdibleItem edibleItem)
        {
            if (hunger > 0)
            {
                Debug.Log(name + " have consumed " + edibleItem.name + " and lost " + edibleItem.saturation + " in hunger");

                hunger = Mathf.Max(0f, hunger - edibleItem.saturation);

                Destroy(edibleItem.gameObject);
            }
            else
            {
                Debug.Log(name + " is not hungry, " + edibleItem.name + " is not consumed");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, scanningRange);

            if (selectedEdibleItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedEdibleItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedEdibleItem.transform.position, 0.25f);
            }
        }
        
    }
}