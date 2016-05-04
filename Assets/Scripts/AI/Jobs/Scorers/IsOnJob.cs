using Apex.AI;
using Apex.Serialization;
using UnityEngine;
using Kontiki;
using System.Collections;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for whether character has edible in memory list
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsOnJob : ContextualScorerBase {
		

		[ApexSerialization, FriendlyName("Not", "Invert return value")]
        public bool not = false;
        
        public override float Score(IAIContext context){
           AIContext ai = (AIContext)context;
            bool b = ai.isOnJob;
           
                
            if(ai.debugAI)
                Debug.Log("Is " + (b ? "on": "off") + " job");
            if(not) b = !b;
            
            

            return b ? score : 0f;
        
        }
	}
}
