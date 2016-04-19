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
        public Item selectedItem;
        private CharacterAIContext _context;
        private Item objectInVicinity;

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
            objectInVicinity = CheckForClosestItemInRange();

            if(hunger < hungerMax){
                hunger += hungerIncrementPerSec * Time.deltaTime;
            }else{
                hunger = hungerMax;
            }

            //selectedEdibleItem = CheckForClosestItemInRange();

            if(Input.GetKeyDown(KeyCode.D)){
                GoToDestination();
            }

            if(Input.GetKeyDown(KeyCode.R)){
                StopMoving();
            }

            if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedItem.UseItem(this);
                }
            }

            if(objectInVicinity != null){
                memory.Add(objectInVicinity.transform);
            }

            if(memory.Count > memoryCapacity){ //Remove last in memory list.
                memory.RemoveAt(0);
            }
        }

        /**
        * Actions
        *
        **/
        public bool HasSelectedResource(){
            return selectedItem != null;
        }

        public void TargetClosestItemInRange(){
            target = CheckForClosestItemInRange().transform;
        }

        Item CheckForClosestItemInRange()
        {
            // Find every Objects within scanningRange area
            Collider[] colliders = Physics.OverlapSphere(transform.position, scanningRange);

            // Look through all colliders and Look for EdibleItem and put them in a list
            List<Item> itemsInRange = new List<Item>();
            foreach (Collider c in colliders) {
                Item foundItem = c.GetComponent<Item>();
                if (foundItem != null)
                {
                    itemsInRange.Add(foundItem);
                }
            }

            // If we found edibleItems then find the closet one to the player
            if(itemsInRange.Count > 0) {
                Item closestItem = itemsInRange[0];
                float x = (itemsInRange[0].transform.position.x - transform.position.x);
                float y = (itemsInRange[0].transform.position.y - transform.position.y);
                float z = (itemsInRange[0].transform.position.z - transform.position.z);
                float currentShortestDistance =  (x * x) + (y * y) + (z * z);

                for(int i = 1; i < itemsInRange.Count; i++) {
                    float cx = (itemsInRange[i].transform.position.x - transform.position.x);
                    float cy = (itemsInRange[i].transform.position.y - transform.position.y);
                    float cz = (itemsInRange[i].transform.position.z - transform.position.z);
                    float distance =  (cx * cx) + (cy * cy) + (cz * cz);

                    if(Mathf.Abs(distance) < Mathf.Abs(currentShortestDistance)) {
                        closestItem = itemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }

                return closestItem;
            }

            return null;
        }

        public void UseSelectedItem(){
            if (selectedItem != null)
            {
                selectedItem.UseItem(this);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, scanningRange);

            if (selectedItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedItem.transform.position, 0.25f);
            }
        }

        public void GoToDestination(){
            agent.destination = target.transform.position;
            Debug.Log("Moving to target");
        }

        public void StopMoving(){
            agent.destination = transform.position;
        }

    }
}
