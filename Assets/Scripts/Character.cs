using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Kontiki
{
    public class Character : MonoBehaviour
    {
        /**
        ** Inspector Items and Configuration
        **/
        public Gender gender;
        [Range(0, 1)]
        public float energy = 1;
        [Range(0, 1)]
        public float hunger = 0;
        [Range(1, 10)]
        public float scanningRange = 1;

        /**
        ** Private Variables & Objects
        **/
        private Item _selectedItem;


        void FixedUpdate()
        {
            _selectedItem = CheckForClosetItemInRange();

            if (_selectedItem != null)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    _selectedItem.UseItem(this);
                }
            }
        }

        Item CheckForClosetItemInRange()
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
                Item closetItem = itemsInRange[0];
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
                        closetItem = itemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }

                return closetItem;
            }

            return null;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, scanningRange);

            if (_selectedItem != null)
            {
                Gizmos.DrawLine(transform.position, _selectedItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_selectedItem.transform.position, 0.25f);
            }
        }
    }
}
