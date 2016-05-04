using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Apex.AI.Components;
using Kontiki.AI;

namespace Kontiki {
	public class Memory : MonoBehaviour {
		private Item objectInVicinity;

		public List<Item> memory;
		private List<Item> _knownItems;
        private AIComponentContainer ai;

        // Use this for initialization
        void Start () {
			memory = new List<Item>();
            _knownItems = new List<Item>();
            ai = GetComponent<AIComponentContainer>();

            FillKnownItemList();
        }

		// Update is called once per frame
		void FixedUpdate () {
            //TODO: Put this into Courutine and check every x minute
            objectInVicinity = CheckForClosestItemInRange(); 

			if(objectInVicinity != null){
				bool b = true;
				for(int i = 0; i < memory.Count; i++){
					if(objectInVicinity == memory[i])
						b = false;
				}
				if(b) memory.Add(objectInVicinity);
			}

			if(memory.Count > Settings.memoryCapacity){ //Remove last in memory list.
				memory.RemoveAt(0);
			}
		}

        public Item CheckForClosestItemInRange()
        {
            // Find all Objects within scanningRange area
            
            Collider[] colliders = Physics.OverlapSphere(transform.position, Settings.scanningRange);

            // Look through all colliders and Look for EdibleItem and put them in a list
            List<Item> itemsInRange = new List<Item>();
            foreach (Collider c in colliders)
            {
                Item foundItem = c.GetComponent<Item>();
                if (foundItem != null)
                {
                    itemsInRange.Add(foundItem);
                }
            }

            // If we found edibleItems then find the closet one to the player
            if (itemsInRange.Count > 0)
            {
                Item closestItem = itemsInRange[0];
                float x = (itemsInRange[0].transform.position.x - transform.position.x);
                float y = (itemsInRange[0].transform.position.y - transform.position.y);
                float z = (itemsInRange[0].transform.position.z - transform.position.z);
                float currentShortestDistance = (x * x) + (y * y) + (z * z);

                for (int i = 1; i < itemsInRange.Count; i++)
                {
                    float cx = (itemsInRange[i].transform.position.x - transform.position.x);
                    float cy = (itemsInRange[i].transform.position.y - transform.position.y);
                    float cz = (itemsInRange[i].transform.position.z - transform.position.z);
                    float distance = (cx * cx) + (cy * cy) + (cz * cz);

                    if (Mathf.Abs(distance) < Mathf.Abs(currentShortestDistance))
                    {
                        closestItem = itemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }

                return closestItem;
            }

            return null;
        }

        public List<Item> GetKnownItemList(){
			return _knownItems;
		}

		public void FillKnownItemList(){
			if(_knownItems.Count > 0)
				_knownItems.Clear();
			// Find all Objects within scanningRange area
			Collider[] colliders = Physics.OverlapSphere(transform.position, Settings.knownAreaSize/2);

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
	}
}
