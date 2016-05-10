using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has an edible resource selected
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsWithinRangeOfPartner : ContextualScorerBase {
		[ApexSerialization, FriendlyName("Range", "Range AI needs to be within to be considered being at position")]
        public float range = 2;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            bool b = false;
        	
            if(Vector3.Distance(character.transform.position, character.socialPartner.transform.position) < range){
            	b = true;
            }

            b = not ? !b : b;

            return b ? score : 0f;
        }
	}
}
