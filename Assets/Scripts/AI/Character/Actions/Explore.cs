using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;

namespace Kontiki.AI{
    /// <summary>
    /// Goes to a random position.
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />   
    public sealed class Explore : ActionBase{
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;
        
        [ApexSerialization, FriendlyName("Range", "Range within which point will be randomly chosen")]
        public float range = 2;

        public override void Execute(IAIContext context){
        	Character character = ((CharacterAIContext)context).character;
			Vector3 point = character.transform.position;
    		
    		for (int i = 0; i < 30; i++) {
				Vector3 randomPoint = character.transform.position + Random.insideUnitSphere * range;
				NavMeshHit hit;
				if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
					point = hit.position;
				}
			}

			if(debug) Debug.Log("POSITION: " + point);

			character.agent.destination = point;
		}
    }
}