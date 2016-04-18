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
        
        
        [Range(0, 100)]
        public float scanningRange = 1;
        
        //Stats
        [Range(0, 1)]
        public float energy = 1;
        [Range(0,100)]
        public float hunger = 0;
        
        public int memoryCapacity = 5; //how many items can this AI store in its memory?
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

        /**
        ** Navigation variables and objects
        **/
        public List<Transform> memory;
        public NavMeshAgent agent;
        public Transform target;
        
        //Stat Min & Max
        public static float hungerMax = 100f;
        public static float hungerMin = 0f;
        
        private void Awake(){
            memory = new List<Transform>();
            _context = new CharacterAIContext(this);
            agent = GetComponent<NavMeshAgent>();
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

            if(Input.GetKeyDown(KeyCode.D)){
                GoToDestination();
            }

            if(Input.GetKeyDown(KeyCode.R)){
                StopMoving();
            }

            if (selectedEdibleItem != null)
            {
				if(Input.GetKeyDown(KeyCode.Q)) {
					ConsumeEdibleItem(selectedEdibleItem);
				}
            }

            if(memory.Count > memoryCapacity){ //Remove last in memory list. Looks retarded but should work like this.
                memory.RemoveAt(0);
            }
        }
        
        public void ScanSorroundings(){
            // Find every Objects within scanningRange area
            Collider[] colliders = Physics.OverlapSphere(transform.position, scanningRange);
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
                EdibleItem closestEdibleItem = edibleItemsInRange[0];
                Vector3 v = (edibleItemsInRange[0].transform.position - transform.position);
                float currentShortestDistance =  v.sqrMagnitude;

                for(int i = 1; i < edibleItemsInRange.Count; i++) {
                    Vector3 cv = (edibleItemsInRange[i].transform.position - transform.position);
                    float distance =  cv.sqrMagnitude;

                    if(Mathf.Abs(distance) < Mathf.Abs(currentShortestDistance)) {
                        closestEdibleItem = edibleItemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }
                //memory.Add(closestEdibleItem); //possibly shouldnt happen here but somewhere else
                return closestEdibleItem;
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
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, scanningRange);

            if (selectedEdibleItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedEdibleItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedEdibleItem.transform.position, 0.25f);
            }
        }
        
        public void GoToDestination(){
            agent.destination = target.transform.position;
        }

        public void StopMoving(){
            agent.destination = transform.position;
        }

    }
}