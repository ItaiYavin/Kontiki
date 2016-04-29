﻿using Apex.AI;
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

		[ApexSerialization, FriendlyName("Item Types", "Type of item")]
		ItemType itemType;

        public override float Score(IAIContext context){
        	bool b = false;
        	Pathfinder pathfinder = ((AIContext)context).pathfinder;
        	if(pathfinder.target != null){
	        	switch(itemType){
		        	case ItemType.anything:
			        	if(pathfinder.target != null)
			        		b = true;
			        		if(debug) Debug.Log("Has target");
			        	else 
			        		b = false;
			        		if(debug) Debug.Log("Has NO target");
	        		break;

	        		case ItemType.edible:
			        	if(pathfinder.target.GetComponent<EdibleItem>() != null){
			        		b = true;
			        		if(debug) Debug.Log("Has edible target");
			        	}
			        	else {
			        		b = false;
			        		if(debug) Debug.Log("Has NO edible target");
			        	}
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
	        }

        	if(not)
        		b = !b;

            if(debug)
                Debug.Log("HasTarget: " + b);

        	return b ? 1f * score : 0f * score;
        }
	}
}