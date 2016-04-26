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

        [Range(0, 100)]
        public float pickupRange = 1;

        //Stats
        [Range(0, 1)]
        public float energy = 1;
        [Range(0,100)]
        public float hunger = 0;
        [Range(0, 200)]
        public int memoryCapacity = 200; //how many items can this AI store in its memory?
        //Stat affectors
        public float hungerIncrementPerSec = 0.1f;

        /**
        ** Private Variables & Objects
        **/
        private CharacterAIContext _context;
        private Item objectInVicinity;

		/**
        ** Inventory stats & Objects
        **/
        public Item selectedItem;
        private Inventory inv;

        /**
        ** Static Variables & Objects
        **/

        /**
        ** Navigation variables and objects
        **/
        public List<Item> memory;
        public NavMeshAgent agent;
        public Transform target;

        [Range(0, 1000)]
        public int knownAreaSize;
        private List<Item> _knownItems;

        //Stat Min & Max
        public static float hungerMax = 100f;
        public static float hungerMin = 0f;

        private void Awake(){
            _knownItems = new List<Item>();
        	inv = GetComponent<Inventory>();
            memory = new List<Item>();
            _context = new CharacterAIContext(this);
            agent = GetComponent<NavMeshAgent>();

            FillKnownItemList();
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

            if (selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    selectedItem.UseItem(this);
                }
            }

            if(objectInVicinity != null){
                bool b = true;
                for(int i = 0; i < memory.Count; i++){
                    if(objectInVicinity == memory[i])
                        b = false;
                }
                if(b) memory.Add(objectInVicinity);
            }

            if(memory.Count > memoryCapacity){ //Remove last in memory list.
                memory.RemoveAt(0);
            }
        
         
        }


        /**
        * Actions
        *
        **/
        public List<Item> GetKnownItemList(){
            return _knownItems;
        }

        public void FillKnownItemList(){
            if(_knownItems.Count > 0)
                _knownItems.Clear();
            // Find all Objects within scanningRange area
            Collider[] colliders = Physics.OverlapSphere(transform.position, knownAreaSize/2);

            // Look through all colliders and Look for EdibleItem and put them in a list
            List<Item> itemsInRange = new List<Item>();
            foreach (Collider c in colliders) {
                Item foundItem = c.GetComponent<Item>();
                if (foundItem != null)
                {
                    if(foundItem is EdibleItem){
                        _knownItems.Add(foundItem);
                    }
                }
            }
        }

        public void CleanMemory(){
            for(int i = 0; i < memory.Count; i++)
                if(memory[i] == null)
                    memory.RemoveAt(i);
        }

        public bool PickUpItem(Item item){
        	Item pickup;
        	if(Vector3.Distance(item.transform.position, transform.position) < pickupRange){
        		pickup = item;
        		for(int i = 0; i < inv.inventorySize; i++){
        			if(inv.GetInventoryItem(i) == null){
        				inv.GetInventoryItems()[i] = pickup;
        				item.gameObject.SetActive(false);
        				return true;
        			}
    			}
        	}
            return false;
        }

        public Inventory GetInventory(){
            return inv;
        }

        public bool HasSelectedResource(){
            return selectedItem != null;
        }

        public void TargetClosestItemInRange(){
            target = CheckForClosestItemInRange().transform;
        }

        Item CheckForClosestItemInRange()
        {
            // Find all Objects within scanningRange area
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

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, pickupRange);

            if (selectedItem != null)
            {
                Gizmos.DrawLine(transform.position, selectedItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(selectedItem.transform.position, 0.25f);
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
