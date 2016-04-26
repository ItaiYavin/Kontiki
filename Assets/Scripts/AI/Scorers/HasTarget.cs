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
		enum ItemType 
		{
			anything,
			edible, 
			fuel,
			character
		};

        [ApexSerialization, FriendlyName("Debug", "Debug Log values")]
        public bool debug = false;

        [ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;

        public override float Score(IAIContext context){
        	bool b = false;
        	Character character = ((CharacterAIContext)context).character;
        	
        	switch(itemType){
	        	case ItemType.anything:
		        	if(character.target != null)
		        		b = true;
		        	else 
		        		b = false;
        		break;

        		case ItemType.edible:
		        	if(character.target is EdibleItem)
		        		b = true;
		        	else 
		        		b = false;
        		break;

        		case ItemType.fuel:
        			if(debug) Debug.Log("Fuel is not implemented!");
        		break;

        		case ItemType.character:
        			if(debug) Debug.Log("Characters as targets are not implemented");
        		break;

        		default:
        			if(debug) Debug.Log("No target type chosen");
        		break;
        	}

        	if(not)
        		b = !b;

            if(debug)
                Debug.Log("HasTarget: " + b);

        	return b ? 1f : 0f;
        }
	}
}