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
	public class HasSocialPartner : ContextualScorerBase {
		[ApexSerialization, FriendlyName("Not", "Returns the opposite")]
        public bool not = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = (AIContext)context;
            Character character = ai.character;
            bool b = false;
        	
        	if(character.socialPartner != null){
        		b = true;
        	}
            

            b = not ? !b : b;

            if(ai.character.languageExchanger.playerWantsToSpeakWithMe)
                Debug.Log("Has Social Partner: " + b);
                
            return b ? score : 0f;
        }
	}
}