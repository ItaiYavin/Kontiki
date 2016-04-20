﻿using Apex.AI;
using Apex.Serialization;
using Kontiki;
using UnityEngine;
using System.Collections.Generic;

namespace Kontiki.AI
{
    /// <summary>
    /// Searches memory for edible fitting requirements. If edible is found, edible is set as target
    /// </summary>
    /// <seealso cref="Apex.AI.ActionBase" />
	public class SetEdibleInMemoryAsTarget : ActionBase 
	{
        [ApexSerialization, FriendlyName("Debug", "if on, writes debug messages to console")]
        bool debug;

        [ApexSerialization, FriendlyName("Find Nearest", "if on, the nearest edible in memory will be chosen as target (this is prioritised)")]
        bool findNearest;

        [ApexSerialization, FriendlyName("Find Most Saturating", "if on, the most saturating edible in memory will be chosen as target")]
        [Range(0, 1)]
        bool findMostSaturating;

        [ApexSerialization, FriendlyName("Threshold for priority", "if both nearest and most saturating is on, if percentage difference in distance is higher, saturation will be ignored")]  
        float priorityThreshold;

        public override void Execute(IAIContext context)
        {
            Character character = ((CharacterAIContext)context).character;
            Transform mostFitting;
        	
        	List<Transform> edibleList = new List<Transform>();

        	for(int i = 0; i < character.memory.Count; i++){
        		if(character.memory[i].GetComponent<EdibleItem>() != null)
        			edibleList.Add(character.memory[i]);
        	}

        	if(edibleList.Count != 0){
        		mostFitting = edibleList[0];

	            for(int i = 1; i < character.memory.Count; i++)
	            {
            		if(findNearest && !findMostSaturating)
            			if(Vector3.Distance(edibleList[i].position, character.transform.position) < 
            					Vector3.Distance(mostFitting.position, character.transform.position))
            				mostFitting = edibleList[i];

            		if(!findNearest && findMostSaturating)
            			if(edibleList[i].GetComponent<EdibleItem>().saturation > mostFitting.GetComponent<EdibleItem>().saturation)
            				mostFitting = edibleList[i];

            		if(findNearest && findMostSaturating){
            			float dif = Vector3.Distance(edibleList[i].position, character.transform.position) - Vector3.Distance(mostFitting.position, character.transform.position);
            			if(dif/Vector3.Distance(mostFitting.position, character.transform.position) < priorityThreshold)
            				if(mostFitting.GetComponent<EdibleItem>().saturation < edibleList[i].GetComponent<EdibleItem>().saturation)
            					mostFitting = edibleList[i];
            		}
	            }
        		character.target = mostFitting;
        		if(debug)
        			Debug.Log("Edible found and added to target!");
        	}
        }
	}
}