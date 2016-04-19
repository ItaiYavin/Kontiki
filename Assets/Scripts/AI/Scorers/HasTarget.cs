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
	public class HasTarget : ContextualScorerBase {
        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context){
        	bool b;
        	Character character = ((CharacterAIContext)context).character;
        	if(character.target != null)
        		b = true;
        	else 
        		b = false;

        	if(not)
        		b = !b;

            if(debug)
                Debug.Log("HasTarget: " + b);

        	return b ? 0f : 1f;
        }
	}
}