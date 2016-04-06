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
        private EdibleItem _selectedEdibleItem;


        void FixedUpdate()
        {
            _selectedEdibleItem = CheckForClosetEdibleInRange();

            if (_selectedEdibleItem != null)
            {
				if(Input.GetKeyDown(KeyCode.Q)) {
					ConsumeEdibleItem(_selectedEdibleItem);
				}
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
                float x = (edibleItemsInRange[0].transform.position.x - transform.position.x);
                float y = (edibleItemsInRange[0].transform.position.y - transform.position.y);
                float z = (edibleItemsInRange[0].transform.position.z - transform.position.z);
                float currentShortestDistance =  (x * x) + (y * y) + (z * z);

                for(int i = 1; i < edibleItemsInRange.Count; i++) {
                    float cx = (edibleItemsInRange[i].transform.position.x - transform.position.x);
                    float cy = (edibleItemsInRange[i].transform.position.y - transform.position.y);
                    float cz = (edibleItemsInRange[i].transform.position.z - transform.position.z);
                    float distance =  (cx * cx) + (cy * cy) + (cz * cz);

                    if(Mathf.Abs(distance) < Mathf.Abs(currentShortestDistance)) {
                        closetEdibleItem = edibleItemsInRange[i];
                        currentShortestDistance = distance;
                    }
                }

                return closetEdibleItem;
            }

            return null;
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

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, scanningRange);

            if (_selectedEdibleItem != null)
            {
                Gizmos.DrawLine(transform.position, _selectedEdibleItem.transform.position);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(_selectedEdibleItem.transform.position, 0.25f);
            }
        }
    }
}
