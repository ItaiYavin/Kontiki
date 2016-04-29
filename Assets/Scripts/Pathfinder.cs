using UnityEngine;
using System.Collections;
using Apex.AI;
using Apex.AI.Components;

namespace Kontiki {
	public class Pathfinder : MonoBehaviour {
        /**
		** Navigation variables and objects
		**/
        public NavMeshAgent agent;
		public Transform target;

        private AIComponentContainer ai;

		// Use this for initialization
		void Start () {
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

		public void StopMoving(){
			agent.destination = transform.position;
		}

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, SettingsSingleton.Instance.scanningRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, SettingsSingleton.Instance.pickupRange);
        }
    }
}
