using UnityEngine;
using System.Collections;

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
            _selectedEdibleItem = CheckForEdibleInRange();

            if (_selectedEdibleItem != null)
            {
				if(Input.GetKeyDown(KeyCode.Q)) {
					ConsumeEdibleItem(_selectedEdibleItem);
				}
            }
        }

        EdibleItem CheckForEdibleInRange()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, scanningRange);

            foreach (Collider c in colliders) {
                EdibleItem n = c.GetComponent<EdibleItem>();
				if(n != null) {
                    return n;
                }
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
