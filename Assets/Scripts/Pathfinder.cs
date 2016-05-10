using UnityEngine;
using System.Collections;
using Apex.AI;
using Apex.AI.Components;

namespace Kontiki {
    
    [RequireComponent(typeof(NavMeshAgent))]
	public class Pathfinder : MonoBehaviour {
        /**
		** Navigation variables and objects
		**/
        [HideInInspector]
        public NavMeshAgent agent;
		public Transform target;

        private AIComponentContainer ai;

		// Use this for initialization
		void Awake () {
			agent = GetComponent<NavMeshAgent>();
            ai = GetComponent<AIComponentContainer>();
		}

		// Update is called once per frame
		void Update () {
        }

        public void TargetClosestItemInRange(){
            target = ai.memory.CheckForClosestItemInRange().transform;
        }

		public void GoToTarget(){
			agent.destination = target.transform.position;
		}

        public void GoTo(Transform destination)
        {
            agent.destination = destination.position;
        }

		public void StopMoving(){
			agent.destination = transform.position;
		}

        public bool IsAtPosition(Vector3 pos, float range)
        {
            float distance = Vector3.Distance(transform.position, pos);
            if(ai.debugAI_Character)
                Debug.Log("Distance to " + pos + " is " + distance  + " range: " + range + " remaining distance: " + agent.remainingDistance);
            return distance < range;
        }

        private void OnDrawGizmosSelected()
        {
            /*
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, Settings.Instance.scanningRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, Settings.Instance.pickupRange);
            */
        }
    }
}
