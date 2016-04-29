using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;
using System.Collections.Generic;

namespace Kontiki.AI
{
    /// <summary>
    /// Goes to a random position.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
    public sealed class Explore : ActionBase{
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        public override void Execute(IAIContext context){
        	AIContext ai = ((AIContext)context);
			Vector3 point = ai.pathfinder.transform.position;
            Vector3 randomPoint = ai.pathfinder.transform.position;
            List<Item> map = ai.memory.GetKnownItemList();

            for(int i = 0; i < 30; i++){
                int r = Random.Range(0, map.Count);
                randomPoint = map[r].transform.position;

                NavMeshHit hit;
    			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
    				point = hit.position;
    			}
            }

			if(debug) Debug.Log("POSITION: " + point);

            ai.pathfinder.agent.destination = point;
		}
    }
}
