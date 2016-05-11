using Apex.AI;
using Apex.Serialization;
using UnityEngine;

namespace Kontiki.AI
{
    /// <summary>
    /// Scorer for evaluating hunger, linear score between 0-1, 0 being the threshold
    /// </summary>
    /// <seealso cref="Apex.AI.ContextualScorerBase" />
	public class Social : ContextualScorerBase {
		[ApexSerialization, FriendlyName("Threshold", "Threshold(0-1) for energy(0-max) where the score will be 0")]
        public float threshold = 0;
        
        [ApexSerialization, FriendlyName("Inverted", "Invert Energy(0-max)")]
        public bool inverted = false;

        public override float Score(IAIContext context)
        {
            AIContext ai = ((AIContext)context);
            
            float e;
            if(inverted)
                e = 1 - (ai.character.social / Settings.socialRange.max);
            else
                e = ai.character.social / Settings.socialRange.max;
                
            float v = Mathf.Max(e - threshold, 0) / (1 - threshold);

            if(ai.debugAI_Character){
                Debug.Log("social: " + v);                
            }
            
            

            if(ai.character.languageExchanger.playerWantsToSpeakWithMe)
                Debug.Log("Social: " + (v * score));
           
            //TODO Make this more flexible
            return v * score;
        }
	}
}