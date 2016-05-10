using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating hunger, linear score between 0-1, 0 being the threshold
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class IsTalking : ContextualScorerBase { 
        [ApexSerialization, FriendlyName("Not", "Inverts result")]
        public bool not = false;
        
        public override float Score(IAIContext context){
            AIContext ai = ((AIContext)context);

            bool b;

            b = ai.character.isTalking;
          
          	if(not) b = !b;

          	return b ? score : 0f;
        }
	}
}